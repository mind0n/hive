/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				Permission.cs	                                           			*
 *      Description:																*
 *				 权限类              											    *
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

namespace PmSys.Components
{
    /// <summary>
    /// 权限类
    /// </summary>
    public class Permission
    {
        #region "Private Variables"
        private string _ApplicationName;
        private int _ApplicationID;
        private string _PageCode;
        private string _PageCodeName;
        private List<PermissionItem> _ItemList = new List<PermissionItem>();
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 应用名称
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int ApplicationID
        {
            get
            {
                return _ApplicationID;
            }
            set
            {
                _ApplicationID = value;
            }
        }
        /// <summary>
        /// 模块代码
        /// </summary>
        public string PageCode
        {
            get
            {
                return _PageCode;
            }
            set
            {
                _PageCode = value;
            }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string PageCodeName
        {
            get
            {
                return _PageCodeName;
            }
            set
            {
                _PageCodeName = value;
            }
        }

        /// <summary>
        /// 权限文件列表
        /// </summary>
        public List<PermissionItem> ItemList
        {
            get
            {
                return _ItemList;
            }
            set
            {
                _ItemList = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// 权限详细类
    /// </summary>
    public class PermissionItem
    {
        #region "Private Variables"
        private string _Item_Name;
        private int _Item_Value;
        private string _Item_FileList;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Item_Name
        {
            get
            {
                return _Item_Name;
            }
            set
            {
                _Item_Name = value;
            }
        }
        /// <summary>
        /// 权限值
        /// </summary>
        public int Item_Value
        {
            get
            {
                return _Item_Value;
            }
            set
            {
                _Item_Value = value;
            }
        }

        /// <summary>
        /// 权限所属文件列表
        /// </summary>
        public string Item_FileList
        {
            get
            {
                return _Item_FileList;
            }
            set
            {
                _Item_FileList = value;
            }
        }
        #endregion
    }

    #region "权限类型"
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PopedomType
    {
        /// <summary>
        /// 列表/查看
        /// </summary>
        List = 2,
        /// <summary>
        /// 新增
        /// </summary> 
        New = 4,
        /// <summary>
        /// 修改
        /// </summary>
        Edit = 8,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 16,
        /// <summary>
        /// 排序
        /// </summary>
        Orderby = 32,
        /// <summary>
        /// 打印
        /// </summary>
        Print = 64,
        /// <summary>
        /// 备用A
        /// </summary>
        A = 128,
        /// <summary>
        /// 备用B
        /// </summary>
        B = 256,
        /// <summary>
        /// 自定义权限512
        /// </summary>
        Custom512 = 512,
        /// <summary>
        /// 自定义权限1024
        /// </summary>
        Custom1024 = 1024,
        /// <summary>
        /// 自定义权限2048
        /// </summary>
        Custom2048 = 2048,
        /// <summary>
        /// 自定义权限4096 
        /// </summary>
        Custom4096 = 4096,
        /// <summary>
        /// 自定义权限8192
        /// </summary>
        Custom8192 = 8192,
        /// <summary>
        /// 自定义权限16384
        /// </summary>
        Custom16384 = 16384,
        /// <summary>
        /// 自定义权限32768
        /// </summary>
        Custom32768 = 32768,
        /// <summary>
        /// 自定义权限65536
        /// </summary>
        Custom65536 = 65536,
        /// <summary>
        /// 自定义权限131072
        /// </summary>
        Custom131072 = 131072,
        /// <summary>
        /// 自定义权限262144
        /// </summary>
        Custom262144 = 262144,
        /// <summary>
        /// 自定义权限524288
        /// </summary>
        Custom524288 = 524288,
        /// <summary>
        /// 自定义权限1048576
        /// </summary>
        Custom1048576 = 1048576,
        /// <summary>
        /// 自定义权限2097152
        /// </summary>
        Custom2097152 = 2097152,
        /// <summary>
        /// 自定义权限4194304
        /// </summary>
        Custom4194304 = 4194304,
        /// <summary>
        /// 自定义权限8388608
        /// </summary>
        Custom8388608 = 8388608,
        /// <summary>
        /// 自定义权限16777216
        /// </summary>
        Custom16777216 = 16777216,
        /// <summary>
        /// 自定义权限33554432
        /// </summary>
        Custom33554432 = 33554432,
        /// <summary>
        /// 自定义权限67108864
        /// </summary>
        Custom67108864 = 67108864,
        /// <summary>
        /// 自定义权限134217728
        /// </summary>
        Custom134217728 = 134217728,
        /// <summary>
        /// 自定义权限268435456
        /// </summary>
        Custom268435456 = 268435456,
        /// <summary>
        /// 自定义权限536870912
        /// </summary>
        Custom536870912 = 536870912
    }
    #endregion
}
