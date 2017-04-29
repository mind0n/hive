using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ULib.DataSchema;
using System.Data;
using System.IO;
using ULib.Exceptions;

namespace ULib.Executing.Commands.Data
{
	public class ParseDatasetCommand : CommandNode
	{
		[ParameterAttribute(IsRequired = true)]
		public string TargetId;
		public bool IsText;
	    public int ColumnIndex;
	    public string ColumnName;
	    public string Filename;
	    public bool IsReadMode;
		public override string Name
		{
			get
			{
				return string.Concat("Parse sql saved in ", TargetId, " with ", IsText ? "text" : "unknown", " format");
			}
			set
			{
				base.Name = value;
			}
		}
		public override void Run(CommandResult rlt, bool isPreview=false)
		{
		    string targetId = null, outputId = null;
            if (!string.IsNullOrEmpty(TargetId))
            {
                targetId = Executor.Instance.ParseIdString(TargetId);
                outputId = targetId;
            }
            if (!string.IsNullOrEmpty(OutputId))
            {
                outputId = Executor.Instance.ParseIdString(OutputId);
            }

			if (!string.IsNullOrEmpty(targetId))
			{
				DataSet ds = Executor.Instance.Get<DataSet>(targetId);
                if (IsReadMode && ds == null && !string.IsNullOrEmpty(Filename))
                {
                    string filename = Filename.PathMakeAbsolute();
                    string content = File.ReadAllText(filename);
                    ds = content.FromXml<DataSet>();
                }
				if (ds != null && IsText && ds.Tables.Count > 0)
				{
                    BackupDataset(ds);
				    DataTable table = ds.Tables[0];
					List<string> sqls = new List<string>();
					foreach (DataRow row in table.Rows)
					{
						object data = GetCellData(row);
						if (data != null)
						{
							sqls.Add(data.ToString());
						}
					}
					//Executor.Instance.SetVar(outputId, false, string.Join("\r\n", sqls.ToArray()));
				    rlt.IsCondition = false;
				    rlt.ResultValue = string.Join("\r\n", sqls.ToArray());
				}
				else
				{
					rlt.LastError = new Exception(string.Format("Cannot parse null DataSet of id {0}.  Please check whether the ExecuteSql task (which has the same ResultId) has generated correct output.", targetId));
				}
			}
			else
			{
				rlt.LastError = new ArgumentNullException("TargetId cannot be null");
			}
		}

	    private void BackupDataset(DataSet ds)
	    {
	        try
	        {
	            string filename = Executor.Instance.ParseIdString(Filename);
	            if (!string.IsNullOrEmpty(filename) && ds != null)
	            {
	                filename = AppDomain.CurrentDomain.BaseDirectory + filename;
	                if (File.Exists(filename))
	                {
	                    try
	                    {
	                        DateTime d = DateTime.Now;
	                        File.Move(filename,
	                                  string.Format("{0}_{1}-{2}-{3}_{4}-{5}-{6}-{7}.xml", filename.PathLastName(false),
	                                                d.Year, d.Month,
	                                                d.Day, d.Hour, d.Minute, d.Second, d.Millisecond));
	                    }
	                    catch (Exception ex)
	                    {
	                        ExceptionHandler.Handle(ex);
	                        try
	                        {
	                            File.Delete(filename);
	                        }
	                        catch (Exception e)
	                        {
	                            ExceptionHandler.Handle(e);
	                        }
	                    }
	                }
	                File.WriteAllText(filename, ds.ToXml());
	            }
	        }
	        catch (Exception ex)
	        {
	            ExceptionHandler.Handle(ex);
	        }
	    }

	    private object GetCellData(DataRow row)
	    {
	        if (!string.IsNullOrEmpty(ColumnName))
	        {
	            string columnName = Executor.Instance.ParseIdString(ColumnName);
	            return row[columnName];
	        }
	        else
	        {
	            return row[ColumnIndex];
	        }
	    }
	}
}
