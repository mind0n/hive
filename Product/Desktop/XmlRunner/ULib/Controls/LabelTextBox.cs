using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ULib.Controls
{
    public partial class LabelTextBox : UserControl
    {
        private Control contentFormatControl;

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

        public TextBox ContentControl
        {
            get
            {
                return tContent;
            }
        }

        public Control ContentFormatControl
        {
            get
            {
                return contentFormatControl;
            }
            set
            {
                if (value != null)
                {
                    contentFormatControl = value;
                    SplitDistance = Splitter.Width - contentFormatControl.Width - Splitter.SplitterWidth;
                }
            }
        }
        public int SplitDistance
        {
            get
            {
                return Splitter.SplitterDistance;
            }
            set
            {
                Splitter.SplitterDistance = value;
            }
        }
        public string Text
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
        public char PasswordChar
        {
            get
            {
                return tContent.PasswordChar;
            }
            set
            {
                tContent.PasswordChar = value;
            }
        }
        public string LabelText
        {
            get
            {
                return tLabel.Text;
            }
            set
            {
                tLabel.Text = value;
            }
        }
        public LabelTextBox()
        {
            InitializeComponent();
        }
    }
}
