using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ULib.DataSchema;
using ULib.Executing;
using ULib.Executing.Commands.Common;
using ULib.Output;
using ULib.Executing.Commands.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace ULib.Controls
{
	public class AutoSettingPanel : Panel
	{
		public int ItemLeft { get; set; }
		public int ItemHeight { get; set; }
		public int ItemMarginLeft { get; set; }
		public int ItemMarginTop { get; set; }
		public int ItemMarginRight { get; set; }
		public int LabelWidth { get; set; }
		private int CurtTop = 10;

		Dict<string, int> editors = new Dict<string, int>();
		public AutoSettingPanel()
		{
			AutoScroll = true;
			AutoScrollMinSize = new System.Drawing.Size(0, 24);
			AllowDrop = true;
			Font = new Font("Consolas", 8);
		}

		public void Initialize()
		{
			Controls.Clear();
			CurtTop = 0;
			ItemLeft = 10;
			ItemHeight = 24;
			LabelWidth = 150;
			ItemMarginLeft = 4;
			ItemMarginTop = 4;
			ItemMarginRight = 20;
			editors["TextBox"] = 0;
			editors["CheckBox"] = 1;
			editors["ComboBox"] = 2;
			editors["PathBrowser"] = 3;
			editors["MultilineBox"] = 4;
		}

		public bool Validate()
		{
			bool rlt = true;
			foreach (Control i in Controls)
			{
				SettingTag tag = i.Tag as SettingTag;
				if (tag != null && !(i is Label))
				{
					Label lb = tag.LabelControl;
					if (tag.Attribute != null)
					{
						//i.Enabled = !tag.Attribute.Validate(tag);
						//if (i.Enabled)
						//{
						//    rlt = false;
						//}
						bool r = tag.Attribute.Validate(tag);
						lb.ForeColor = r ? Color.Black : Color.Red;
						if (!r)
						{
							rlt = false;
						}
					}
					else
					{
						lb.ForeColor = Color.Black;
					}
				}
			}
			return rlt;
		}
		public void Generate(Node settings, string txtfile)
		{
			// (?<=\")(.*?)(?=\"])
			if (File.Exists(txtfile))
			{
				Node setting = new Node();
				string key = txtfile.PathLastName();
				AddGroup(key);
				string content = File.ReadAllText(txtfile);
				Regex regex = new Regex("(?<=%)(.*?)(?=%)", RegexOptions.IgnoreCase);
				MatchCollection ms = regex.Matches(content);
				setting.Name = key;
				foreach (Match m in ms)
				{
					Node item = new Node { Name = m.Groups[0].ToString() };
					//setting.Add(item);
					SetCommand cmd = new SetCommand { Id = item.Name, Name = item.Name, Argument = string.Concat("%", item.Name, "%"), Visible = false };
					ExecuteParameter par = Executor.Instance.GetVar(cmd.Id);
					if (par != null)
					{
						if (par.Value is SetCommand)
						{
							cmd = (SetCommand)par.Value;
						}
						else
						{
							throw new Exception(string.Format("Duplicate task id '{0}' detected in the plan.", item.Name));
						}
					}
					else
					{
						Executor.Instance.SetVar(item.Name, new ExecuteParameter { Name = item.Name, IsCondition = false, Value = cmd });
						Executor.Instance.ExecEntry.Add(cmd, 0);
					}
					//item.Value = cmd;
					setting.Add(cmd);
					if (cmd != null)
					{
						AddItem(item.Name, cmd, "Value");
					}
				}
				settings.Add(setting);
			}
		}
		public void Generate(CommandNode cmd, bool isReadonly = false)
		{
			Controls.Clear();
			CurtTop = ItemMarginTop;
			if (cmd != null)
			{
				FieldInfo[] list = cmd.GetType().GetFields();
				foreach (FieldInfo i in list)
				{
					AddCmdItem(i.Name, cmd, i.Name, i.FieldType.Name.IndexOf("bool", StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0, isReadonly);
				}
			}
		}
		public void Generate(ExecuteParameters parameters)
		{
			Controls.Clear();
			CurtTop = ItemMarginTop;
			if (parameters != null)
			{
				foreach (KeyValuePair<string, ExecuteParameter> i in parameters)
				{
					string key = i.Key;
					ExecuteParameter p = i.Value;
					if (p.Value != null && p.Value is CommandNode)
					{
						UICommandNode ucmd = null;
						CommandNode cmd = (CommandNode)p.Value;
						if (!cmd.Visible)
						{
							continue;
						}
						if (cmd is UICommandNode)
						{
							ucmd = (UICommandNode)cmd;
						}
						if (cmd is SetCommand)
						{
							SetCommand set = (SetCommand)cmd;
							FieldInfo f = cmd.GetType().GetField("Content");
							SettingTag tag = AddCmdItem(p.Name, cmd, f.Name, set.IsCondition ? 1 : 0);
							tag.Cmd = cmd;
							tag.Id = f.Name;
						}
						else if (cmd is ArrayCommand)
						{
							ArrayCommand ac = (ArrayCommand)cmd;
							AddCmdItem(ac.Id, ac, "SelectedIndex", 2);
						}
						else if (cmd is RunJobCommand)
						{
							RunJobCommand rc = (RunJobCommand)cmd;
							AddCmdItem(rc.Id, rc, "JobName");
						}
						else
						{
							FieldInfo[] list = cmd.GetType().GetFields();
							AddGroup(string.Concat(p.Name, " - ", cmd.GetType().Name));
							foreach (FieldInfo f in list)
							{
								ParameterAttribute a = null;
								object[] attr = f.GetCustomAttributes(typeof(ULib.Executing.ParameterAttribute), true);
								if (attr != null && attr.Length > 0)
								{
									a = attr[0] as ParameterAttribute;
								}
								if (!string.Equals(f.Name, "id", StringComparison.OrdinalIgnoreCase) && !string.Equals(f.Name, "iscondition", StringComparison.OrdinalIgnoreCase))
								{
									if (a == null || !a.IsHidden)
									{
										SettingTag tag = AddCmdItem(f.Name, cmd, f.Name, f.FieldType.Name.IndexOf("bool", StringComparison.OrdinalIgnoreCase) >= 0 ? 1 : 0);
										tag.Cmd = cmd;
										tag.Id = f.Name;
										if (a != null)
										{
											tag.Attribute = a;
										}
									}
								}
							}
							if (ucmd != null)
							{
								ucmd.OnGenerateCompleted(this);
							}
							AddGroup(string.Empty);
						}
					}
				}
			}
		}
		public void AddButton(string name, EventHandler callback)
		{
			Button b = new Button();
			b.Text = name;
			b.Click += callback;
			b.AutoSize = true;
			UpdatePos(null, b, 0, false);
		}

		public void AddGroup(string name)
		{
			Label lb = new Label();
			lb.Font = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
			lb.AutoSize = true;
			lb.Text = name;
			Panel p = new Panel();
			p.BackColor = Color.Silver;
			p.Height = 4;
			UpdatePos(lb, p, 8);
		}

		public SettingTag AddItem(string name, object target, string field)
		{
			Label lb = new Label { Text = name, AutoSize = true };
			TextBox editor = new TextBox();
			SettingTag rlt = null;
			FieldInfo f = target.GetType().GetField(field);
			if (f != null)
			{
				rlt = new SettingTag { FieldInfo = f, Target = target, Attribute = new ParameterAttribute { IsRequired = true } };
				rlt.LabelControl = lb;
				lb.Tag = rlt;
				object value = f.GetValue(target);
				editor.Tag = rlt;
				editor.Text = value != null ? value.ToString() : string.Empty;
				editor.TextChanged += new EventHandler(editor_TextChanged);
				UpdatePos(lb, editor);
			}
			return rlt;
		}

		public SettingTag AddCmdItem(string name, CommandNode target, string field, int editorType = 0, bool isReadonly = false)
		{
			if (target == null)
			{
				return null;
			}
			UICommandNode ucmd = null;
			if (target is UICommandNode)
			{
				ucmd = (UICommandNode)target;
			}
			PathBrowser pb = new PathBrowser();
			ComboBox rd = new ComboBox();
			TextBox editor = new TextBox();
			CheckBox ck = new CheckBox();
			Label lb = new Label { Text = name, AutoSize = true };
			if (!string.IsNullOrEmpty(target.Editor) && editors.ContainsKey(target.Editor))
			{
				editorType = editors[target.Editor];
			}
			editor.AllowDrop = true;
            editor.MouseDoubleClick += new MouseEventHandler(editor_MouseDoubleClick);
			ck.Enabled = !isReadonly;
			editor.Enabled = !isReadonly;
			rd.Enabled = !isReadonly;
			lb.MouseDown += new MouseEventHandler(lb_MouseDown);
			SettingTag rlt = null;
			FieldInfo f = target.GetType().GetField(field);
			if (f != null)
			{
				rlt = new SettingTag { FieldInfo = f, Target = target };
				lb.Tag = rlt;
				object value = f.GetValue(target);
				rlt.EditorType = editorType;
				rlt.LabelControl = lb;
				if (editorType == 1)
				{
					rlt.EditControl = ck;
					ck.Tag = rlt;
					ck.Checked = value != null ? bool.Parse(value.ToString()) : false;
					ck.CheckedChanged += new EventHandler(ck_CheckedChanged);
					UpdatePos(lb, ck);
					NotifyUINode(ucmd, ck, lb);
				}
				else if (editorType == 2)
				{
					ArrayCommand arr = target as ArrayCommand;
					if (arr != null)
					{
						//rd.DataSource = null;
						rd.Items.Clear();	 
						rlt.EditControl = rd;
						rd.Tag = rlt;
						//rd.DataSource = arr.GetList();
						rd.Items.AddRange(arr.GetList());
						rd.SelectedIndex = arr.SelectedIndex;
						rd.SelectedIndexChanged += new EventHandler(rd_SelectedIndexChanged);						
						UpdatePos(lb, rd);
						NotifyUINode(ucmd, rd, lb);
					}
				}
				else if (editorType == 3)
				{
					rlt.EditControl = pb;
					pb.Tag = rlt;
					if (value != null)
					{
						pb.StartupPath = value.ToString();
					}
					pb.OnTextChanged+=new Action<PathBrowser>(pb_OnTextChanged);
					UpdatePos(lb, pb);
					NotifyUINode(ucmd, pb, lb);
				}
				else
				{
					if (lb.Text.IndexOf("pwd", StringComparison.OrdinalIgnoreCase) >= 0 || lb.Text.IndexOf("password", StringComparison.OrdinalIgnoreCase) >= 0)
					{
						editor.PasswordChar = '*';
					}
					editor.DragEnter += new DragEventHandler(editor_DragEnter);
					editor.DragDrop += new DragEventHandler(editor_DragDrop);
					if (editorType == 4)
					{
						editor.WordWrap = false;
						editor.ScrollBars = ScrollBars.Both;
						editor.Multiline = true;
						editor.Height = 100;
					}
					rlt.EditControl = editor;
					rlt.Id = lb.Text;
					rlt.EditorType = 0;
					editor.Tag = rlt;
					editor.Text = value != null ? value.ToString() : string.Empty;
					editor.TextChanged += new EventHandler(editor_TextChanged);
					UpdatePos(lb, editor);
					NotifyUINode(ucmd, editor, lb);
				}
			}
			return rlt;
		}

        private void editor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Multiline && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                object data = Clipboard.GetData(DataFormats.Text);
                if (data != null)
                {
                    string s = null;
                    string row = data.ToString();
                    if (row.IndexOf('\t') > 0)
                    {
                        string[] t = row.Split('\t');
                        if (t.Length == 3)
                        {
                            s = string.Concat(t[2], "\\", t[0]);
                        }
                        else
                        {
                            s = t[t.Length - 1];
                        }
                    }
                    else
                    {
                        s = row;
                    }
                    if (s.StartsWith("$/Root"))
                    {
                        s = s.Replace("\r\n", "");
                        s = s.Replace("/", "\\");
                        s = s.Replace("$\\Root", "###");
                        BoxAddPath(s, box);
                    }
                }
            }
        }

		private static void NotifyUINode(UICommandNode ucmd, Control ck, Label lb)
		{
			if (ucmd != null)
			{
				ucmd.OnGenerate(lb, ck);
			}
		}

		void editor_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		void editor_DragDrop(object sender, DragEventArgs e)
		{
			TextBox box = sender as TextBox;
			if (box != null)
			{
				if (box.Multiline)
				{
					BoxAddPath(e.GetDragString(), box);
				}
				else
				{
					box.Text = e.GetDragString();
				}
			}
		}

		private static void BoxAddPath(string path, TextBox box)
		{
			box.Text = box.Text.Trim();
			if (!string.IsNullOrEmpty(box.Text) && !box.Text.EndsWith("\r\n"))
			{
				box.Text += "\r\n";
			}
			box.Text += path;
		}

		void pb_OnTextChanged(PathBrowser sender)
		{
			if (sender.Tag != null && sender.Tag is SettingTag)
			{
				SettingTag tag = (SettingTag)sender.Tag;
				tag.SetValue(sender.SelectedPath);
			}
		}

		void rd_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox box = sender as ComboBox;
			if (box != null && box.Tag != null && box.Tag is SettingTag)
			{
				SettingTag tag = (SettingTag)box.Tag;
				tag.SetValue(box.SelectedIndex);
			}
		}

		void editor_TextChanged(object sender, EventArgs e)
		{
			TextBox box = sender as TextBox;
			if (box != null && box.Tag != null && box.Tag is SettingTag)
			{
				SettingTag tag = (SettingTag)box.Tag;
				tag.SetValue(box.Text);
				if (tag.LabelControl != null)
				{
					tag.LabelControl.ForeColor = Color.Black;
				}
			}
		}

		void ck_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = sender as CheckBox;
			if (box != null && box.Tag != null && box.Tag is SettingTag)
			{
				SettingTag tag = (SettingTag)box.Tag;
				tag.SetValue(box.Checked);
			}
		}
		void lb_MouseDown(object sender, MouseEventArgs e)
		{
			Label s = sender as Label;
			if (s != null)
			{
				SettingTag t = s.Tag as SettingTag;
				if (t != null && t.EditControl != null && !string.Equals("id", s.Text, StringComparison.OrdinalIgnoreCase))
				{
					if (t.Attribute == null || !t.Attribute.IsReadonly)
					{
						t.EditControl.Enabled = true;
					}
				}
			}
		}

		private void UpdatePos(Control label, Control target = null, int offsetTop = 0, bool setWidth = true)
		{
			int itemHeight = ItemHeight, itemLeft = ItemLeft;
			if (label != null)
			{
				Controls.Add(label);
				label.Left = itemLeft;
				label.Top = CurtTop + 4;
				itemHeight = label.Height;
				itemLeft += label.Width + ItemMarginLeft;
			}
			if (target != null)
			{
				Controls.Add(target);
				if (label != null && string.IsNullOrEmpty(label.Text))
				{
					itemLeft = ItemLeft;
				}
				else if (LabelWidth > itemLeft)
				{
					itemLeft = LabelWidth;
				}
				target.Left = itemLeft;
				target.Top = CurtTop + offsetTop;
				if (setWidth)
				{
					target.Width = this.Width - itemLeft - ItemMarginRight;
				}
				if (target.Height > itemHeight)
				{
					itemHeight = target.Height;
				}
			}
			if (ItemHeight > itemHeight)
			{
				itemHeight = ItemHeight;
			}
			CurtTop += itemHeight + ItemMarginTop;
			
		}

	}

	public class SettingTag
	{
		public object Target;
		public FieldInfo FieldInfo;
		public int EditorType;
		public ParameterAttribute Attribute;
		public CommandNode Cmd;
		public Control EditControl;
		public Label LabelControl;
		public string Id;
		public object Value
		{
			get
			{
				return FieldInfo.GetValue(Target);
			}
		}
		public void SetValue(object v)
		{
			if (Target != null && FieldInfo != null)
			{
				try
				{
					object value = Convert.ChangeType(v, FieldInfo.FieldType);
					FieldInfo.SetValue(Target, value);
				}
				catch { }
			}
		}
	}
}
