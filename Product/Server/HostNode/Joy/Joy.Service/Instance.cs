using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using Joy.Core;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Joy.Core;

namespace Joy.Service
{
    public class Instance
    {
        internal WebOperationContext cx;
        public virtual InstanceSettings Settings { get { return null; } }

        //protected virtual string LoadFileContent(string path)
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return string.Empty;
        //    }
        //    if (path.IndexOf("..") >= 0 || path.IndexOf(":") >= 0)
        //    {
        //        return string.Empty;
        //    }
            
        //    var vf = path.PathMap(settings.BaseDir);
        //    if (File.Exists(vf))
        //    {
        //        var cnt = File.ReadAllText(vf);
        //        return cnt;
        //    }
        //    return string.Empty;
        //}

        //protected virtual Stream LoadFileStream(string path)
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return Stream.Null;
        //    }
        //    if (path.IndexOf("..") >= 0 || path.IndexOf(":") >= 0)
        //    {
        //        return Stream.Null;
        //    }

        //    var vf = path.PathMap(settings.BaseDir);
        //    var cnt = File.OpenRead(vf);
        //    return cnt;
        //}   

        //protected virtual Stream LoadResStream(string path, string type = null)
        //{
        //    if (string.IsNullOrEmpty(path))
        //    {
        //        return string.Empty.Stream();
        //    }
        //    var asm = Assembly.GetExecutingAssembly();
        //    if (!string.IsNullOrEmpty(type))
        //    {
        //        var t = type.RetrieveType();
        //        if (t != null)
        //        {
        //            asm = Assembly.GetAssembly(t);
        //        }
        //    }
        //    return asm.GetManifestResourceStream(path);
        //}
    }
    public class InstanceSettings
    {
        private string baseDir;
        public string BaseDir
        {
            get
            {
                string rlt = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(baseDir))
                {
                    rlt = string.Format(baseDir, AppDomain.CurrentDomain.BaseDirectory);
                }
                return rlt;
            }
            set
            {
                var s = value;
                baseDir = s.PathMap(AppDomain.CurrentDomain.BaseDirectory, '\\');
            }
        }
        public bool IsRes { get; set; }

        public string ProviderName { get; set; }

        private ResourceProvider provider;
        public ResourceProvider Provider
        {
            get
            {
                if (provider == null)
                {
                    if (string.IsNullOrEmpty(ProviderName))
                    {
                        ProviderName = typeof(FileContentProvider).AssemblyQualifiedName;
                    }
                    var type = Type.GetType(ProviderName);
                    if (type == null)
                    {
                        provider = new FileContentProvider(this);
                    }
                    else
                    {
                        provider = Activator.CreateInstance(type, this) as ResourceProvider;
                    }
                }
                return provider;
            }
        }
    }
    public class EmailSettings
    {
        public string To;
        public string CC;
        public string Title;
        public string Uname;
        public string Upwd;
        public string Host;
        public int Port;
        public bool EnableSSL;
        public SmtpDeliveryMethod Method;
        public string Body;
        public bool IsHtml;
        public string From;

        public bool Send(bool isAsync = false, bool isCatch = false)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(string.IsNullOrEmpty(From) ? Uname : From);
                foreach (var i in this.To.Split(';'))
                {
                    if (!string.IsNullOrEmpty(i))
                    {
                        mail.To.Add(i);
                    }
                }
                mail.Subject = this.Title;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.Body = this.Body;
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                if (isAsync)
                {
                    Thread th = null;
                    th.Launch(() =>
                    {
                        Send(mail);
                    });
                }
                else
                {
                    Send(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (isCatch)
                {
                    Error.Handle(ex);
                }
                else
                {
                    throw;
                }
                return false;
            }
        }

        private void Send(MailMessage mail)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = Host;
                smtp.Port = Port;
                smtp.Credentials = new NetworkCredential(Uname, Upwd);
                smtp.EnableSsl = EnableSSL;
                smtp.DeliveryMethod = Method;
                smtp.Send(mail);
            }
        }
    }

    public class GmailSettings : EmailSettings
    {
        public GmailSettings()
        {
            To = "weerspecial@126.com";
            CC = "weerspecial@gmail.com";
            Title = "Test email";
            Uname = string.Empty;
            Upwd = string.Empty;
            Host = "smtp.gmail.com";
            Port = 587;
            EnableSSL = true;
            Method = SmtpDeliveryMethod.Network;
            Body = "This is a test email<a href='http://www.google.com'>Google</a>";
            IsHtml = true;

        }
    }

    public class ActionAttribute : Joy.Core.MethodAttribute
    {
        //public string Joiner = "/";
        public override object[] OnInvoke(object instance, System.Reflection.ParameterInfo[] pars, object[] args)
        {
            var joiner = "/";
            var opars = base.OnInvoke(instance, pars, args);
            var ins = instance as Instance;
            if (ins != null)
            {
                joiner = ins.Settings.Provider.Joiner;
            }
            if (opars.Length < args.Length)
            {
                var rlt = new List<string>();
                for (int i = opars.Length - 1; i < args.Length; i++)
                {
                    var o = args[i];
                    if (o != null)
                    {
                        rlt.Add(o.ToString());
                    }
                    else
                    {
                        rlt.Add(string.Empty);
                    }
                }
                opars[opars.Length - 1] = string.Join(joiner, rlt.ToArray());
            }
            return opars;
        }
    }

}
