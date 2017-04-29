using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using ULib.DataSchema;
using ULib.DataSchema.Alerting;
using ULib.Output;
using ULib.Results;

namespace ULib.Executing.Commands.Special
{
    public class IpAt : CommandNode
    {
        public const string TemplateImport = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1""";
        public const string TemplateExport = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";
        public const string SelectScriptTemplate = "SELECT * FROM [{0}$]";
        public const string InsertScriptTemplate = "INSERT INTO [{0}$]({1}) VALUES({2})";

        public delegate void ImportItemHandler(Entity entity, OleDbDataReader reader);

        public string Folder;
        public string OutFolder;
        public string Connection;
        public string OutputFile;
        public string Ext;
        public string IsCheckRel;
        public string IsSplitSqlFiles;

        private bool isCheckRel
        {
            get
            {
                string v = Executor.Instance.ParseIdString(IsCheckRel);
                if (!string.IsNullOrEmpty(v))
                {
                    return Convert.ToBoolean(v);
                }
                return true;
            }
        }

        private bool isSplitSqlFiles
        {
            get
            {
                string v = Executor.Instance.ParseIdString(IsSplitSqlFiles);
                if (!string.IsNullOrEmpty(v))
                {
                    return Convert.ToBoolean(v);
                }
                return true;
            }
        }

        public override string Name
        {
            get
            {
                return string.Format("Import records from {0}", Executor.Instance.ParseIdString(Folder));
            }
        }
        public override void Run(CommandResult rlt, bool isPreview = false)
        {
            string folder = Executor.Instance.ParseIdString(Folder);
            outFile = outFolder + Executor.Instance.ParseIdString(OutputFile);
            List<string> paths = new List<string>();
            string[] files = Directory.GetFiles(folder, Executor.Instance.ParseIdString(Ext));
            paths.AddRange(files);
            paths.Sort();
            if (File.Exists(outFile))
            {
                File.Delete(outFile);
            }
            if (File.Exists(eFile))
            {
                File.Delete(eFile);
            }
            counter.Clear();
            if (!isSplitSqlFiles)
            {
                BeginWriteData(outFile);
            }
            else
            {
                string[] sqlfiles = Directory.GetFiles(outFolder, "*.sql");
                foreach (string i in sqlfiles)
                {
                    File.Delete(i);
                }
            }
            foreach (string i in paths)
            {
                if (isCancelling || isStopRequested)
                {
                    break;
                }
                if (isSplitSqlFiles)
                {
                    GenerateSingleSql(i);
                }
                else
                {
                    GenerateSingleSql(i, true);
                }
            }
            if(!isSplitSqlFiles)
                EndWriteData(outFile);
        }
        private string outFile;

        private string outFolder
        {
            get
            {
                string r = Executor.Instance.ParseIdString(OutFolder);
                if (!r.EndsWith("\\"))
                {
                    r += "\\";
                }
                if (!Directory.Exists(r))
                {
                    Directory.CreateDirectory(r);
                }
                return r;
            }
        }

        private string eFile
        {
            get { return outFile + ".err.txt"; }
        }

        private void GenerateSingleSql(string file, bool isSingleFile = false)
        {
            if (File.Exists(file))
            {
                string outfile = outFile;
                if (!isSingleFile)
                {
                    outfile = outFolder + file.PathLastName(false) + ".sql";
                }
                SqlConnection conn = ULib.Executing.Commands.Data.ExecuteSqlCommand.GetConnection(Executor.Instance.ParseIdString(Connection), "DBA");

                OutputHandler.Handle("Loading data from xls file {0}...", 0, file);
                EntitySet r1 = LoadSheetData(file, "R1", typeof(TableRecordEntity));
                //EntitySet ItemItems = LoadSheetData(file, "R2", typeof(ARI));
                OutputHandler.Handle("Generating SQL file ...");
                try
                {
                    conn.Open();
                    if (!isSingleFile)
                    {
                        BeginWriteData(outfile);
                    }
                    WriteData(outfile, conn, r1, "T1");
                    //WriteData(outfile, conn, ItemItems, "T2");
                    if (!isSingleFile)
                    {
                        EndWriteData(outfile);
                    }
                    if (isCheckRel)
                    {
                        //ValidateRelationship(ItemItems, r1);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private  Dict<string, int> counter = new Dict<string, int>();

        private void BeginWriteData(string ofile)
        {
            File.WriteAllText(ofile, "use DBA\r\n");
            File.AppendAllText(ofile, "BEGIN try\r\n");
            File.AppendAllText(ofile, "BEGIN TRAN\r\n");
            File.AppendAllText(ofile, @"
                SET NOCOUNT ON
                SET IDENTITY_INSERT T1 OFF
                SET IDENTITY_INSERT T1_Item OFF
            ");
        }

        private void EndWriteData(string ofile)
        {
            File.AppendAllText(ofile, @"
                        COMMIT TRAN
                        PRINT 'COMMITTED SUCCESSFULLY'
                    End try
                    BEGIN catch
                        PRINT @@ERROR
                        print ERROR_MESSAGE()
                        PRINT 'ERROR IN SCRIPT, ROLLING BACK'
                        ROLLBACK TRAN
                    END catch
                GO
                ");
            File.AppendAllText(ofile, @"
                SET IDENTITY_INSERT T1 OFF
                SET IDENTITY_INSERT T1_Item OFF
            ");

        }

        private void WriteData(string ofile, SqlConnection conn, List<Entity> r1, string table)
        {
            File.AppendAllText(ofile, string.Format("SET IDENTITY_INSERT {0} ON\r\n",table));
            
            for (int ii = 0; ii < r1.Count; ii++)
            {
                if (isCancelling || isStopRequested)
                {
                    break;
                }
                Entity i = r1[ii];
                //string ins = i.MakeInsertSQL(conn);
                MakeSqlResult mr = i.MakeInsertSQL(conn);
                if (mr.IsNoException)
                {
                    string ins = mr.Sql;
                    File.AppendAllText(ofile, ins);
                }
                else
                {
                    File.AppendAllText(eFile, mr.MiscInfo + "\t" + mr.LastError.Message + "\tOriginal Data:" + mr.OriginalData + "\r\n");
                }
                if (ii % 500 == 0)
                {
                    OutputHandler.Handle(ii + "/" + r1.Count);
                }
            }
            File.AppendAllText(ofile, string.Format("SET IDENTITY_INSERT {0} OFF\r\n", table));
        }

        private static EntitySet LoadSheetData(string file, string sheetname, Type type)
        {
            string connstr = string.Format(TemplateImport, file);
            EntityMappings mapping = type.GetMappedFields();
            EntitySet rlt = new EntitySet();
            using (OleDbConnection cn = new OleDbConnection(connstr))
            {
                try
                {
                    using (OleDbCommand cmd = cn.CreateCommand())
                    {
                        cn.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = string.Format(SelectScriptTemplate, sheetname);
                        using (OleDbDataReader r = cmd.ExecuteReader())
                        {
                            int row = 0;
                            List<string> columns = new List<string>();
                            while (r.Read())
                            {
                                row++;
                                Entity o = Activator.CreateInstance(type) as Entity;
                                if (o != null)
                                {
                                    for (int i = 0; i < r.VisibleFieldCount; i++)
                                    {
                                        //string n = r.GetName(i);
                                        if (row == 1)
                                        {
                                            object colobj = r.GetValue(i);
                                            columns.Add(colobj.ToString());
                                        }
                                        else
                                        {
                                            string n = columns[i];
                                            if (!string.IsNullOrEmpty(n))
                                            {
                                                n = n.Replace("/", "");
                                                n = n.Replace("\\", "");
                                                if (mapping.ContainsKey(n))
                                                {
                                                    EntityMappingsItem item = mapping[n];
                                                    FieldInfo info = item.FieldInfo;
                                                    object value = r.GetValue(i);
                                                    o.OriginalValues[n] = value;
                                                    //info.SetValue(o, value);
                                                }
                                            }
                                        }
                                    }
                                    if (row > 1)
                                    {
                                        if (!o.IsPFieldEmpty)
                                        {
                                            o._MiscInfo = string.Format("File: {0}, Sheet:{1}, Line:{2}", file, sheetname, row);
                                            rlt.AddEntity(o);
                                        }
                                        else
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    cn.Close();
                }

            }
            return rlt;
        }

    }
}
