/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				sys_UserTable.cs                                   		        	*
 *      Description:																*
 *				 用户实体类                 	           						    *
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
    /// 用户实体类
    /// </summary>
    [Serializable]
    public class sys_UserTable
    {
        #region "Private Variables"
        private string _DB_Option_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除 
        private int _UserID = 0;  // 用户ID号
        private string _U_LoginName="";  // 登陆名
        private string _U_Password; //密码md5加密字符
        private string _U_CName;  // 中文姓名
        private string _U_EName; //英文名
        private int _U_GroupID = 0;  // 部门ID号与sys_Group表中GroupID关联
        private string _U_Email; //电子邮件
        private int _U_Type = 1;  // 用户类型0:超级用户1:普通用户
        private int _U_Status = 0;  // 当前状态0:正常 1:禁止
        private string _U_Licence; //用户序列号
        private string _U_Mac; //锁定机器硬件地址
        private string _U_Remark;  // 备注说明
        private string _U_IDCard; //身份证号码
        private int _U_Sex = 0;  // 性别1:男0:女
        private DateTime? _U_BirthDay; // 出生日期
        private string _U_MobileNo; //手机号
        private string _U_UserNO; //员工编号
        private DateTime? _U_WorkStartDate; // 到职日期
        private DateTime? _U_WorkEndDate; // 离职日期
        private string _U_CompanyMail; //公司邮件地址
        private int _U_Title = 0;  // 职称与应用字段关联
        private string _U_Extension; //分机号
        private string _U_HomeTel; //家中电话
        private string _U_PhotoUrl;  // 用户照片网址
        private DateTime _U_DateTime = DateTime.MaxValue; // 操作时间
        private string _U_LastIP; //最后访问IP
        private DateTime _U_LastDateTime = DateTime.MaxValue; // 最后访问时间
        private string _U_ExtendField; //扩展字段
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除 
        /// </summary>
        public string DB_Option_Action_
        {
            set { this._DB_Option_Action_ = value; }
            get { return this._DB_Option_Action_; }
        }

        /// <summary>
        /// 用户ID号
        /// </summary>
        public int UserID
        {
            set { this._UserID = value; }
            get { return this._UserID; }
        }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string U_LoginName
        {
            set { this._U_LoginName = value; }
            get { return this._U_LoginName; }
        }

        /// <summary>
        /// 密码md5加密字符
        /// </summary>
        public string U_Password
        {
            set { this._U_Password = value; }
            get { return this._U_Password; }
        }

        /// <summary>
        /// 中文姓名
        /// </summary>
        public string U_CName
        {
            set { this._U_CName = value; }
            get { return this._U_CName; }
        }

        /// <summary>
        /// 英文名
        /// </summary>
        public string U_EName
        {
            set { this._U_EName = value; }
            get { return this._U_EName; }
        }

        /// <summary>
        /// 部门ID号与sys_Group表中GroupID关联
        /// </summary>
        public int U_GroupID
        {
            set { this._U_GroupID = value; }
            get { return this._U_GroupID; }
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string U_Email
        {
            set { this._U_Email = value; }
            get { return this._U_Email; }
        }

        /// <summary>
        /// 用户类型0:超级用户1:普通用户2:管理员
        /// </summary>
        public int U_Type
        {
            set { this._U_Type = value; }
            get { return this._U_Type; }
        }

        /// <summary>
        /// 当前状态0:正常 1:禁止
        /// </summary>
        public int U_Status
        {
            set { this._U_Status = value; }
            get { return this._U_Status; }
        }

        /// <summary>
        /// 用户序列号
        /// </summary>
        public string U_Licence
        {
            set { this._U_Licence = value; }
            get { return this._U_Licence; }
        }

        /// <summary>
        /// 锁定机器硬件地址
        /// </summary>
        public string U_Mac
        {
            set { this._U_Mac = value; }
            get { return this._U_Mac; }
        }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string U_Remark
        {
            set { this._U_Remark = value; }
            get { return this._U_Remark; }
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string U_IDCard
        {
            set { this._U_IDCard = value; }
            get { return this._U_IDCard; }
        }

        /// <summary>
        /// 性别1:男0:女
        /// </summary>
        public int U_Sex
        {
            set { this._U_Sex = value; }
            get { return this._U_Sex; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? U_BirthDay
        {
            set { this._U_BirthDay = value; }
            get { return this._U_BirthDay; }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        public string U_MobileNo
        {
            set { this._U_MobileNo = value; }
            get { return this._U_MobileNo; }
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string U_UserNO
        {
            set { this._U_UserNO = value; }
            get { return this._U_UserNO; }
        }

        /// <summary>
        /// 到职日期
        /// </summary>
        public DateTime? U_WorkStartDate
        {
            set { this._U_WorkStartDate = value; }
            get { return this._U_WorkStartDate; }
        }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? U_WorkEndDate
        {
            set { this._U_WorkEndDate = value; }
            get { return this._U_WorkEndDate; }
        }

        /// <summary>
        /// 公司邮件地址
        /// </summary>
        public string U_CompanyMail
        {
            set { this._U_CompanyMail = value; }
            get { return this._U_CompanyMail; }
        }

        /// <summary>
        /// 职称与应用字段关联
        /// </summary>
        public int U_Title
        {
            set { this._U_Title = value; }
            get { return this._U_Title; }
        }

        /// <summary>
        /// 分机号
        /// </summary>
        public string U_Extension
        {
            set { this._U_Extension = value; }
            get { return this._U_Extension; }
        }

        /// <summary>
        /// 家中电话
        /// </summary>
        public string U_HomeTel
        {
            set { this._U_HomeTel = value; }
            get { return this._U_HomeTel; }
        }

        /// <summary>
        /// 用户照片网址
        /// </summary>
        public string U_PhotoUrl
        {
            set { this._U_PhotoUrl = value; }
            get { return this._U_PhotoUrl; }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime U_DateTime
        {
            set { this._U_DateTime = value; }
            get { return this._U_DateTime; }
        }

        /// <summary>
        /// 最后访问IP
        /// </summary>
        public string U_LastIP
        {
            set { this._U_LastIP = value; }
            get { return this._U_LastIP; }
        }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime U_LastDateTime
        {
            set { this._U_LastDateTime = value; }
            get { return this._U_LastDateTime; }
        }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string U_ExtendField
        {
            set { this._U_ExtendField = value; }
            get { return this._U_ExtendField; }
        }

        #endregion
    }
}
