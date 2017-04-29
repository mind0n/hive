namespace Portal.Winform
{
    partial class ManageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Awesomium.Core.WebPreferences webPreferences1 = new Awesomium.Core.WebPreferences(true);
            this.web = new Awesomium.Windows.Forms.WebControl(this.components);
            this.ws = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.Dock = System.Windows.Forms.DockStyle.Fill;
            this.web.Location = new System.Drawing.Point(0, 0);
            this.web.Size = new System.Drawing.Size(777, 559);
            this.web.Source = new System.Uri("http://localhost:1104/Testing/3js/Test3JS.aspx", System.UriKind.Absolute);
            this.web.TabIndex = 0;
            // 
            // ws
            // 
            this.ws.DataPath = "E:\\$Main\\Session";
            webPreferences1.CanScriptsAccessClipboard = true;
            webPreferences1.Databases = true;
            webPreferences1.EnableGPUAcceleration = true;
            webPreferences1.FileAccessFromFileURL = true;
            webPreferences1.SmoothScrolling = true;
            webPreferences1.UniversalAccessFromFileURL = true;
            webPreferences1.WebGL = true;
            webPreferences1.WebSecurity = false;
            this.ws.Preferences = webPreferences1;
            this.ws.Views.Add(this.web);
            // 
            // ManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 559);
            this.Controls.Add(this.web);
            this.Name = "ManageForm";
            this.Text = "Storage Management";
            this.ResumeLayout(false);

        }

        #endregion

        private Awesomium.Windows.Forms.WebControl web;
        public Awesomium.Windows.Forms.WebSessionProvider ws;
    }
}