/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_FrameWorkInfoTable.cs                         		        	*
 *      Description:																*
 *				 基础模组版本信息 		            							    *
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
using System.Management;

namespace PmSys.Components
{
    /// <summary>
    /// 基础模组版本信息
    /// </summary>
    [Serializable]
    public class sys_FrameWorkInfoTable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public sys_FrameWorkInfoTable()
        {
            
            ManagementClass mc = new ManagementClass("Win32_Processor");
            try
            {
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    _S_RegsionGUID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                _S_RegsionGUID = Common.EnBase64(AppDomain.CurrentDomain.BaseDirectory);
                FileTxtLogs.WriteServiceLog(ex.ToString(), "LoadFrameWorkInfoTable");
            }
            finally {
                mc.Dispose();
            }
        }

        #region "Private Variables"
        private string _S_Name = "签证管理系统";
        private string _S_Version = "1.0.7";
        private S_VersionType _S_Type = S_VersionType.Release;
        private string _S_Licensed = "";
        private string _S_RegsionSystem = Common.GetServerOS;
        private string _S_RegsionIp = Common.GetServerIp;
        private string _S_RegsionDomain = Common.GetServerHost;
        private string _S_RegsionGUID;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 系统名称
        /// </summary>
        public string S_Name
        {
            get
            {
                return _S_Name;
            }
            set
            {
                _S_Name = value;
            }
        }
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string S_Version
        {
            get
            {
                return _S_Version;
            }
            set
            {
                _S_Version = value;
            }
        }
        /// <summary>
        /// 版本类型
        /// </summary>
        public S_VersionType S_Type
        {
            get
            {
                return _S_Type;
            }
            set
            {
                _S_Type = value;
            }
        }
        /// <summary>
        /// 产品序列号
        /// </summary>
        public string S_Licensed
        {
            get
            {
                return _S_Licensed;
            }
            set
            {
                _S_Licensed = value;
            }
        }
        /// <summary>
        /// 注册操作系统名称
        /// </summary>
        public string S_RegsionSystem
        {
            get
            {
                return _S_RegsionSystem;
            }
            set
            {
                _S_RegsionSystem = value;
            }
        }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string S_RegsionIp
        {
            get
            {
                return _S_RegsionIp;
            }
            set
            {
                _S_RegsionIp = value;
            }
        }
        /// <summary>
        /// 注册域名
        /// </summary>
        public string S_RegsionDomain
        {
            get
            {
                return _S_RegsionDomain;
            }
            set
            {
                _S_RegsionDomain = value;
            }
        }
        /// <summary>
        /// 注册GUID
        /// </summary>
        public string S_RegsionGUID
        {
            get
            {
                return _S_RegsionGUID;
            }
        }


        #endregion
    }


    /// <summary>
    /// 版本信息
    /// </summary>
    [Serializable]
    public enum S_VersionType
    {
        /// <summary>
        /// 测试版
        /// </summary>
        Beta = 0,
        /// <summary>
        /// 试用版
        /// </summary>
        Trail = 1,
        /// <summary>
        /// 正式版
        /// </summary>
        Release = 2
    }
}
