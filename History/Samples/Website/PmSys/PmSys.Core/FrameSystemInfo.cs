/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				FrameSystemInfo.cs                                        			*
 *      Description:																*
 *				 系统信息操作类    												    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using PmSys.Components;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Collections;
using System.Reflection;

namespace PmSys
{
    /// <summary>
    /// 系统信息操作类
    /// </summary>
    public class FrameSystemInfo
    {
        /// <summary>
        /// 系统信息类
        /// </summary>
        public static sys_SystemInfoTable GetSystemInfoTable = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        static FrameSystemInfo()
        {
            if (GetSystemInfoTable == null)
            {
                sys_SystemInfoTable si = new sys_SystemInfoTable();
                QueryParam qp = new QueryParam();
                qp.PageIndex = 1;
                qp.PageSize = 1;
                qp.OrderType = 0;
                int Recount = 0;

                ArrayList lst = BusinessFacade.sys_SystemInfoList(qp, out Recount);
                if (Recount == 0)
                {
                    si.DB_Option_Action_ = "Insert";
                    si.SystemID = BusinessFacade.sys_SystemInfoInsertUpdate(si);
                }
                else
                {
                    si = lst[0] as sys_SystemInfoTable;
                    //更新新加配置
                    sys_ConfigDataTable sys_ConfigDB = si.S_SystemConfigData;
                    sys_ConfigDataTable sys_Config = new sys_ConfigDataTable();
                    //foreach (PropertyInfo pi in new sys_ConfigDataTable().GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    foreach (PropertyInfo pi in sys_ConfigDB.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (pi.GetValue(sys_ConfigDB, null) == null)
                        {
                            pi.SetValue(sys_ConfigDB, pi.GetValue(sys_Config, null), null);
                        }
                    }

                    //从Dll中更新版本
                    //sys_FrameWorkInfoTable sys_Db = si.S_FrameWorkInfo;
                    //sys_FrameWorkInfoTable sys_Dll = new sys_FrameWorkInfoTable();
                    //foreach (PropertyInfo pi in sys_Db.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    //{
                    //    if (pi.Name != "S_Licensed")
                    //    {
                    //        pi.SetValue(sys_Db, pi.GetValue(sys_Dll, null), null);
                    //    }
                    //}
                }
                GetSystemInfoTable = si;
            }
            FrameWorkPermission.InitStat = true;
        }

        /// <summary>
        /// 序列化sys_FrameWorkInfoTable类
        /// </summary>
        /// <param name="it">sys_FrameWorkInfoTable类</param>
        /// <returns>byte[]字节流</returns>
        public static byte[] Serializable_sys_FrameWorkInfoTable(sys_FrameWorkInfoTable it)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;
            formatter.Serialize(ms, it);
            ms.Position = 0;
            b = new byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }

        /// <summary>
        /// 反序列化字节为sys_FrameWorkInfoTable类
        /// </summary>
        /// <param name="BytArray">字节流</param>
        /// <returns>sys_FrameWorkInfoTable类</returns>
        public static sys_FrameWorkInfoTable Deserialize_sys_FrameWorkInfoTable(byte[] BytArray)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(BytArray, 0, BytArray.Length);
            ms.Position = 0;
            sys_FrameWorkInfoTable it = formatter.Deserialize(ms) as sys_FrameWorkInfoTable;
            return it;
        }

        /// <summary>
        /// 序列化sys_ConfigDataTable类
        /// </summary>
        /// <param name="it">sys_ConfigDataTable类</param>
        /// <returns>byte[]字节流</returns>
        public static byte[] Serializable_sys_ConfigDataTable(sys_ConfigDataTable it)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;
            formatter.Serialize(ms, it);
            ms.Position = 0;
            b = new byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }

        /// <summary>
        /// 反序列化字节为sys_ConfigDataTable类
        /// </summary>
        /// <param name="BytArray">字节流</param>
        /// <returns>sys_ConfigDataTable类</returns>
        public static sys_ConfigDataTable Deserialize_sys_ConfigDataTable(byte[] BytArray)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            ms.Write(BytArray, 0, BytArray.Length);
            ms.Position = 0;
			try
			{
				object o = formatter.Deserialize(ms);
				sys_ConfigDataTable it = o as sys_ConfigDataTable;
				return it;
			}
			catch
			{
				return sys_ConfigDataTable.DefaultConfigDataTable;
			}
        }

        /// <summary>
        /// 获取PmSys名称
        /// </summary>
        public static string FrameWorkVerName
        {
            get
            {
                string FrameName = "";
                if (GetSystemInfoTable.S_FrameWorkInfo.S_Licensed != "" && GetSystemInfoTable.S_FrameWorkInfo.S_Type != S_VersionType.Beta)
                    FrameName = GetSystemInfoTable.S_FrameWorkInfo.S_Name + " " + GetSystemInfoTable.S_FrameWorkInfo.S_Version + " " + S_VersionType.Release.ToString();
                else
                    FrameName = GetSystemInfoTable.S_FrameWorkInfo.S_Name + " " + GetSystemInfoTable.S_FrameWorkInfo.S_Version + " " + GetSystemInfoTable.S_FrameWorkInfo.S_Type.ToString();
                return string.Format(" {0}<font size='2'>({1})</font>", FrameName, Common.GetDBType);
            }

        }
    }
}
