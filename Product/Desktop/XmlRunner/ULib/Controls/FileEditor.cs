using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace ULib.Controls
{
    public partial class FileEditor : UserControl
    {
        public bool ReadOnly
        {
            get
            {
                return tContent.ReadOnly;
            }
            set
            {
                tContent.ReadOnly = value;
            }
        }
        private bool hideSaveButton;

        public bool HideSaveButton
        {
            get { return hideSaveButton; }
            set { hideSaveButton = value; }
        }

        private bool hideCloseButton;

        public bool HideCloseButton
        {
            get { return hideCloseButton; }
            set { hideCloseButton = value; }
        }
        public TextBox ContentBox
        {
            get
            {
                return this.tContent;
            }
        }
        public Button SaveButton
        {
            get
            {
                return bSave;
            }
        }
        public Button ResetButton
        {
            get
            {
                return bReset;
            }
        }
        public Button CloseButton
        {
            get
            {
                return bClose;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Content
        {
            get
            {
                return tContent.Text;
            }
            set
            {
                tContent.Text = value;
            }
        }
        public FileEditor()
        {
            InitializeComponent();
            textboxes = new ObservableCollection<TextBoxItem>();
            textboxes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(textboxList_CollectionChanged);
            Load += new EventHandler(FileEditor_Load);
        }

        public void WriteLine(string msg, params string[] args)
        {
            tContent.Text += string.Format(msg, args) + "\r\n";
        }

        void FileEditor_Load(object sender, EventArgs e)
        {
            bClose.Visible = !hideCloseButton;
            bSave.Visible = !hideSaveButton;
            InitTxtBoxList();
        }

        void textboxList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            InitTxtBoxList();
        }

        private void InitTxtBoxList()
        {
            if (textboxes.Count > 0)
            {
                sp.SplitterDistance = 100;
                sp.Panel1.AutoScroll = true;
                sp.Panel1.AutoScrollMinSize = new Size();
                container.Controls.Clear();
                foreach (TextBoxItem i in textboxes)
                {
                    LabelTextBox b = new LabelTextBox();
                    b.LabelText = i.Label;
                    b.Text = i.Text;
                    b.Dock = DockStyle.Bottom;
                    container.Controls.Add(b);
                }
            }
            else
            {
                sp.SplitterDistance = 0;
            }
        }


        private ObservableCollection <TextBoxItem> textboxes;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<TextBoxItem> TextboxList
        {
            get { return textboxes; }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            Content = string.Empty;
        }
    }
    public class TextBoxItem
    {
        public string Label { get; set; }
        public string Text { get; set; }
    }
}
