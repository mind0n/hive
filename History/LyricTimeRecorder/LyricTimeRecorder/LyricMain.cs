using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LyricTimeRecorder
{
	public partial class LyricMain : Form
	{
		string[] lines;
		int index = 0;
		DateTime start;
		bool started = false;
		public LyricMain()
		{
			InitializeComponent();
            Reset();
			FormClosing += new FormClosingEventHandler(LyricMain_FormClosing);
		}

        private void Reset()
        {
            started = false;
            tResult.Text = string.Empty;
            lines = File.ReadAllLines("Input.txt");
			lyrics.Items.Clear();
            foreach (var l in lines)
            {
                lyrics.Items.Add(l);
            }
            lyrics.Items.Add("--------------End----------------");
            lyrics.SelectedIndex = index;
        }
		protected override void OnShown(EventArgs e)
		{
			TopMost = true;
		}

		void LyricMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (File.Exists("Output.txt"))
				{
					File.Delete("Output.txt");
				}
				File.WriteAllText("Output.txt", tResult.Text);
			}
			catch (Exception error)
			{
				MessageBox.Show(error.ToString());
			}
		}

		private void bRecord_Click(object sender, EventArgs e)
		{
			if (started)
			{
				try
				{
					TimeSpan t = DateTime.Now - start;
					tResult.Text += string.Concat(
                        "Data+=\"[", 
                        N(t.Minutes), 
                        ':', 
                        N(t.Seconds), 
                        '.', 
                        N((int)t.Milliseconds / 10), 
                        ']', 
                        lines[index], 
                        "\\n\";\r\n");
					index++;
					lyrics.SelectedIndex = index;
				}
				catch { }
			}
			else
			{
				start = DateTime.Now;
				Text += " - Recording";
				started = true;
			}
		}
		private string N(int num)
		{
			if (num < 10)
			{
				return "0" + num;
			}
			else
			{
				return num.ToString();
			}
		}

		private void lyrics_SelectedIndexChanged(object sender, EventArgs e)
		{
			index = lyrics.SelectedIndex;
		}

        private void bReset_Click(object sender, EventArgs e)
        {
            Reset();
        }
	}
}
