using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DeskViewer
{
    /// <summary>
    /// 0|http://192.168.42.128:8000/DesktopService/{0}|
    /// 1000|http://cleansys.vicp.cc:8000/DesktopService/{0}
    /// </summary>
    public class ViewerForm : Form
    {
        public static string hostTmp;

        private SecDelayedEvents evts;

        private PictureBox pic;

        private System.ComponentModel.IContainer components = null;

        public static Cfg cfg;
        private int cinterval;
        private int interval;

        private bool altDown;

        private string cfgfile = AppDomain.CurrentDomain.BaseDirectory + "cfg.txt";

        public ViewerForm()
        {
            InitializeComponent();
            Load += ViewerForm_Load;
            Shown += ViewerForm_Shown;
            ReadCfg();
            evts = new SecDelayedEvents();
        }

        private void ReadCfg()
        {
            if (File.Exists(cfgfile))
            {
                var cnt = File.ReadAllText(cfgfile);
                cfg = Cfg.Parse(cnt);
                interval = cfg.Interval;
                cinterval = interval;
                hostTmp = cfg.UrlScr;
            }
        }

        void ViewerForm_Load(object sender, EventArgs e)
        {
            evts.OnSendRequest += evts_OnSendRequest;
            MouseWheel += pic_MouseWheel;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }

        void evts_OnSendRequest(bool isSuccess, string error = null)
        {
            if (!isSuccess)
            {
                MessageBox.Show(error);
            }
            else if (interval < 0)
            {
                var th = new Thread(new ThreadStart(delegate
                {
                    RetrieveScr();
                }));
                th.IsBackground = true;
                th.Start();
            }
        }

        void ViewerForm_Shown(object sender, System.EventArgs e)
        {
            LaunchViewerThread();
        }

        private void LaunchViewerThread()
        {
            var th = new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    if (cfg.Mode == 0)
                    {
                        if (!RetrieveScr())
                        {
                            break;
                        }
                    }
                    else if (cfg.Mode == 1)
                    {
                        var req = WebRequest.Create(cfg.UrlScr);
                        using (var res = req.GetResponse())
                        {
                            using (var sm = res.GetResponseStream())
                            {
                                var ss = new StreamReader(sm).ReadToEnd();
                                ss = HttpUtility.UrlDecode(ss);
                                var jss = new JavaScriptSerializer();
                                //var rlt = jss.Deserialize<ResultBase>(s);
                                //var ss = rlt.Result.ToString();
                                ss = OpenSSLDecrypt(ss, SecDelayedEvents.Pwd);
                                var bs = Convert.FromBase64String(ss);
                                using (var ms = new MemoryStream(bs))
                                {
                                    var bmp = Image.FromStream(ms);
                                    Invoke((MethodInvoker)delegate
                                    {
                                        if (pic.Image != null)
                                        {
                                            pic.Image.Dispose();
                                        }
                                        pic.Image = bmp;
                                    });
                                }
                            }
                        }
                        if (interval < 0)
                        {
                            
                        }
                        if (interval > 0)
                        {
                            Thread.Sleep(interval);
                        }

                    }
                }
            }));
            th.IsBackground = true;
            th.Start();
        }

        public static string OpenSSLDecrypt(string encrypted, string passphrase)
        {
            encrypted = encrypted.Replace(' ', '+');
            // base 64 decode
            byte[] encryptedBytesWithSalt = Convert.FromBase64String(encrypted);
            // extract salt (first 8 bytes of encrypted)
            byte[] salt = new byte[8];
            byte[] encryptedBytes = new byte[encryptedBytesWithSalt.Length - salt.Length - 8];
            Buffer.BlockCopy(encryptedBytesWithSalt, 8, salt, 0, salt.Length);
            Buffer.BlockCopy(encryptedBytesWithSalt, salt.Length + 8, encryptedBytes, 0, encryptedBytes.Length);
            // get key and iv
            byte[] key, iv;
            DeriveKeyAndIV(passphrase, salt, out key, out iv);
            return DecryptStringFromBytesAes(encryptedBytes, key, iv);
        }

        static string DecryptStringFromBytesAes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                            srDecrypt.Close();
                        }
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }


        private static void DeriveKeyAndIV(string passphrase, byte[] salt, out byte[] key, out byte[] iv)
        {
            // generate key and iv
            List<byte> concatenatedHashes = new List<byte>(48);

            byte[] password = Encoding.UTF8.GetBytes(passphrase);
            byte[] currentHash = new byte[0];
            MD5 md5 = MD5.Create();
            bool enoughBytesForKey = false;
            // See http://www.openssl.org/docs/crypto/EVP_BytesToKey.html#KEY_DERIVATION_ALGORITHM
            while (!enoughBytesForKey)
            {
                int preHashLength = currentHash.Length + password.Length + salt.Length;
                byte[] preHash = new byte[preHashLength];

                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(password, 0, preHash, currentHash.Length, password.Length);
                Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + password.Length, salt.Length);

                currentHash = md5.ComputeHash(preHash);
                concatenatedHashes.AddRange(currentHash);

                if (concatenatedHashes.Count >= 48)
                    enoughBytesForKey = true;
            }

            key = new byte[32];
            iv = new byte[16];
            concatenatedHashes.CopyTo(0, key, 0, 32);
            concatenatedHashes.CopyTo(32, iv, 0, 16);

            md5.Clear();
            md5 = null;
        }

        public static string OpenSSLEncrypt(string plainText, string passphrase)
        {
            // generate salt
            byte[] key, iv;
            byte[] salt = new byte[8];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);
            DeriveKeyAndIV(passphrase, salt, out key, out iv);
            // encrypt bytes
            byte[] encryptedBytes = EncryptStringToBytesAes(plainText, key, iv);
            // add salt as first 8 bytes
            byte[] encryptedBytesWithSalt = new byte[salt.Length + encryptedBytes.Length + 8];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("Salted__"), 0, encryptedBytesWithSalt, 0, 8);
            Buffer.BlockCopy(salt, 0, encryptedBytesWithSalt, 8, salt.Length);
            Buffer.BlockCopy(encryptedBytes, 0, encryptedBytesWithSalt, salt.Length + 8, encryptedBytes.Length);
            // base64 encode
            return Convert.ToBase64String(encryptedBytesWithSalt);
        }

        static byte[] EncryptStringToBytesAes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                        swEncrypt.Flush();
                        swEncrypt.Close();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();
        }
        private bool RetrieveScr()
        {
            try
            {
                var ssss = OpenSSLEncrypt(Environment.MachineName, SecDelayedEvents.Pwd);
                //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "clt.txt", ssss);
                var sss = ssss.Replace("/", "\""); //HttpUtility.UrlEncode(ssss);
                var req = WebRequest.Create(string.Format(hostTmp, "c/" + sss));
                using (var res = req.GetResponse())
                {
                    using (var sm = res.GetResponseStream())
                    {
                        var s = new StreamReader(sm).ReadToEnd();
                        s = HttpUtility.UrlDecode(s);
                        var jss = new JavaScriptSerializer();
                        var rlt = jss.Deserialize<ResultBase>(s);
                        var ss = rlt.Result.ToString();
                        ss = OpenSSLDecrypt(ss, SecDelayedEvents.Pwd);
                        var bs = Convert.FromBase64String(ss);
                        using (var ms = new MemoryStream(bs))
                        {
                            var bmp = Image.FromStream(ms);
                            Invoke((MethodInvoker)delegate
                            {
                                if (pic.Image != null)
                                {
                                    pic.Image.Dispose();
                                }
                                pic.Image = bmp;
                            });
                        }
                    }
                }
                if (interval < 0)
                {
                    return false;
                }
                if (interval > 0)
                {
                    Thread.Sleep(interval);
                }
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate { MessageBox.Show(ex.ToString()); });
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(747, 476);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 476);
            this.Controls.Add(this.pic);
            this.KeyPreview = true;
            this.Name = "ViewerForm";
            this.Text = "Viewer Window";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewerForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ViewerForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        private static int DetectButton(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    return 0;
                case MouseButtons.Middle:
                    return 1;
                case MouseButtons.Right:
                    return 2;
                default:
                    return 0;
            }
        }

        void pic_MouseWheel(object sender, MouseEventArgs e)
        {
            UpdateStatus("Mouse Wheel");
            evts.Mouse("mousewheel", e.X, e.Y, 0, pic.Width, pic.Height, e.Delta);
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateStatus("Mouse Down");
            if (e.Button == MouseButtons.Right && altDown)
            {
                interval = interval >= 0 ? -1 : cinterval;
                return;
            }
            evts.Mouse("mousedown", e.X, e.Y, DetectButton(e), pic.Width, pic.Height);
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateStatus("Mouse Up");
            evts.Mouse("mouseup", e.X, e.Y, DetectButton(e), pic.Width, pic.Height);
        }

        private void ViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            UpdateStatus("Key Down");
            if (e.KeyValue == 18)
            {
                altDown = true;
            }
            evts.Keyboard("keydown", e.KeyValue);
        }

        private void UpdateStatus(string status)
        {
            Text = string.Format("{0} {1}", (interval < 0 ? "Paused" : interval.ToString()), status);
        }

        private void ViewerForm_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateStatus("Key Up");
            if (e.KeyValue == 18)
            {
                altDown = false;
            }
            evts.Keyboard("keyup", e.KeyValue);
        }
    }

    [Serializable]
    public class ResultBase
    {
        public List<object> ResultSet = new List<object>();

        private bool isResultSetted;

        private Exception lastError;

        public bool IsResultSetted
        {
            get { return isResultSetted; }
        }

        [XmlIgnore]
        public Exception LastError
        {
            get { return lastError; }
            set
            {
                isResultSetted = true;
                lastError = value;
            }
        }

        public string LastErrorMsg
        {
            get
            {
                return lastError == null ? null : lastError.Message;
            }
        }

        public bool IsNoException
        {
            get
            {
                return LastError == null;
            }
        }

        public bool IsSuccessful
        {
            get
            {
                return IsNoException && isResultSetted;
            }
        }

        public object Result
        {
            get
            {
                if (ResultSet != null && ResultSet.Count > 0)
                {
                    return ResultSet[0];
                }
                return null;
            }
            set
            {
                isResultSetted = true;
                if (ResultSet == null)
                {
                    ResultSet = new List<object>();
                }
                if (value != null)
                {
                    ResultSet.Add(value);
                }
                else
                {
                    ResultSet.Clear();
                }
            }
        }

        public void Reset()
        {
            isResultSetted = false;
            lastError = null;
        }

        public override string ToString()
        {
            return string.Format("Result {0} returned {1} exception{2}.", (Result == null ? "NULL" : Result.ToString()),
                (IsNoException ? "without" : "contains"), (IsNoException ? string.Empty : LastError.ToString()));
        }
    }

    public class Events
    {
        public delegate void SendRequestDelegate(bool isSuccess, string error = null);

        public event SendRequestDelegate OnSendRequest;

        protected string evttmp = "{{ name:\"{0}\",btn:{1},x:{2},y:{3},cw:{4},ch:{5},key:\"{6}\",delta:{7} }}";

        protected List<string> events = new List<string>();

        public string u;

        public Events()
        {
            if (ViewerForm.cfg.Mode == 0)
            {
                 u = string.Format(ViewerForm.hostTmp, "Tl");
            }
            else if (ViewerForm.cfg.Mode == 1)
            {
                u = ViewerForm.cfg.UrlEvt;
            }
        }

        public virtual void Mouse(string act, int x, int y, int b, int cw, int ch, int delta = 0)
        {
            var s = string.Format(evttmp, act, b, x, y, cw, ch, " ", delta);
            ProcessEvt(s);
        }

        public virtual void Keyboard(string act, int code)
        {
            var s = string.Format(evttmp, act, 0, 0, 0, 0, 0, code, 0);
            ProcessEvt(s);
        }

        protected virtual void ProcessEvt(string s)
        {
            SendRequest(s);
        }

        protected virtual void SendRequest(string s)
        {
            var encoding = new ASCIIEncoding();
            string postData = s;
            byte[] data = encoding.GetBytes(postData);
            var req = WebRequest.Create(new Uri(u, UriKind.Absolute));
            req.Method = "POST";
            req.ContentLength = data.Length;
            var sreq = req.GetRequestStream();
            sreq.Write(data, 0, data.Length);


            try
            {
                using (var res = req.GetResponse())
                {
                }
                if (OnSendRequest != null)
                {
                    OnSendRequest(true);
                }
            }
            catch (WebException wex)
            {
                var sx = wex.Response.GetResponseStream();
                string ss = "";
                int lastNum = 0;
                do
                {
                    lastNum = sx.ReadByte();
                    ss += (char)lastNum;
                } while (lastNum != -1);
                sx.Close();
                OnSendRequest(false, ss);
                s = null;
            }
        }
    }

    public class SecDelayedEvents : DelayedEvents
    {
        static SecDelayedEvents()
        {
            var f = AppDomain.CurrentDomain.BaseDirectory + "cfg.txt";
            Pwd = Cfg.Parse(File.ReadAllText(f)).Pwd;
            //if (File.Exists(f))
            //{
            //    var s = File.ReadAllText(f);
            //    var l = s.Split('|');
            //    if (l.Length > 2)
            //    {
            //        Pwd = l[2];
            //    }
            //}
        }
        public static string Pwd;
        public static string Encrypt(string plainText, string passphrase)
        {
            // generate salt
            byte[] key, iv;
            byte[] salt = new byte[8];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);
            DeriveKeyAndIV(passphrase, salt, out key, out iv);
            // encrypt bytes
            byte[] encryptedBytes = EncryptStringToBytesAes(plainText, key, iv);
            // add salt as first 8 bytes
            byte[] encryptedBytesWithSalt = new byte[salt.Length + encryptedBytes.Length + 8];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("Salted__"), 0, encryptedBytesWithSalt, 0, 8);
            Buffer.BlockCopy(salt, 0, encryptedBytesWithSalt, 8, salt.Length);
            Buffer.BlockCopy(encryptedBytes, 0, encryptedBytesWithSalt, salt.Length + 8, encryptedBytes.Length);
            // base64 encode
            return Convert.ToBase64String(encryptedBytesWithSalt);
        }
        static byte[] EncryptStringToBytesAes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("iv");

            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged { Mode = CipherMode.CBC, KeySize = 256, BlockSize = 128, Key = key, IV = iv };

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                        swEncrypt.Flush();
                        swEncrypt.Close();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();
        }

        private static void DeriveKeyAndIV(string passphrase, byte[] salt, out byte[] key, out byte[] iv)
        {
            // generate key and iv
            List<byte> concatenatedHashes = new List<byte>(48);

            byte[] password = Encoding.UTF8.GetBytes(passphrase);
            byte[] currentHash = new byte[0];
            MD5 md5 = MD5.Create();
            bool enoughBytesForKey = false;
            // See http://www.openssl.org/docs/crypto/EVP_BytesToKey.html#KEY_DERIVATION_ALGORITHM
            while (!enoughBytesForKey)
            {
                int preHashLength = currentHash.Length + password.Length + salt.Length;
                byte[] preHash = new byte[preHashLength];

                Buffer.BlockCopy(currentHash, 0, preHash, 0, currentHash.Length);
                Buffer.BlockCopy(password, 0, preHash, currentHash.Length, password.Length);
                Buffer.BlockCopy(salt, 0, preHash, currentHash.Length + password.Length, salt.Length);

                currentHash = md5.ComputeHash(preHash);
                concatenatedHashes.AddRange(currentHash);

                if (concatenatedHashes.Count >= 48)
                    enoughBytesForKey = true;
            }

            key = new byte[32];
            iv = new byte[16];
            concatenatedHashes.CopyTo(0, key, 0, 32);
            concatenatedHashes.CopyTo(32, iv, 0, 16);

            md5.Clear();
            md5 = null;
        }

        protected override void SendRequest(string s)
        {
            s = Encrypt(s, Pwd);
            base.SendRequest(s);
        }
    }

    public class DelayedEvents : Events
    {
        private BackgroundWorker w;

        private object lck = new object();

        public DelayedEvents()
        {
            w = new BackgroundWorker();
            w.DoWork += w_DoWork;
            //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", "");
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(300);
            lock (lck)
            {
                var queue = events;
                events = new List<string>();
                var jss = new JavaScriptSerializer();
                var ss = jss.Serialize(queue);
                ss = ss.Replace("\"{", "{");
                ss = ss.Replace("}\"", "}");
                ss = ss.Replace("\\\"", "\"");
                SendRequest(ss);
            }
        }

        protected override void ProcessEvt(string s)
        {
            lock (lck)
            {
                events.Add(s);
            }
            if (!w.IsBusy)
            {
                w.RunWorkerAsync();
            }
        }
    }
    public class Cfg
    {
        public int Mode;
        public string UrlScr;
        public string UrlEvt;
        public string Pwd;
        public int Interval;
        public static Cfg Parse(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("Config Content");
            }
            var list = content.Split('|');
            if (list == null || list.Length < 1)
            {
                throw new ArgumentNullException("Config Content");
            }
            
            var cfg = new Cfg();
            cfg.Mode = int.Parse(list[0]);
            if (cfg.Mode == 0)
            {
                cfg.Interval = int.Parse(list[1]);
                cfg.UrlEvt = list[2];
                cfg.UrlScr = list[2];
                cfg.Pwd = list[3];
            }
            else if (cfg.Mode == 1)
            {
                cfg.Interval = int.Parse(list[1]);
                cfg.UrlScr = list[2];
                cfg.UrlEvt = list[3];
                cfg.Pwd = list[4];
            }
            return cfg;
        }
    }
}
