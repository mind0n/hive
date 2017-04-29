/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				DataProvider.cs                         	                		*
 *      Description:																*
 *				 获取记录值    	   	        						        	    *
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
using System.Data;
using System.Collections;
using PmSys.Components;

namespace PmSys.Data
{
    /// <summary>
    /// 获取记录值
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    public delegate object PopulateDelegate(IDataReader dr);

    /// <summary>
    /// 数据抽象类
    /// </summary>
    public abstract class DataProvider
    {
        
        #region "DataProvider Instance"

        private static DataProvider _defaultInstance = null;
        static DataProvider()
        {
            DataProvider dataProvider;
            if (Common.GetDBType == "MsSql")
                dataProvider = new SqlDataProvider();
            else if (Common.GetDBType == "Access")
            {
                //dataProvider = new AccessDataProvider();
            }
            else if (Common.GetDBType == "Oracle")
            {
               // dataProvider = new OracleDataProvider();
            }
            else
                throw new ApplicationException("数据库配置不对！");

            _defaultInstance = new SqlDataProvider(); ;
        }

        /// <summary>
        /// 数据访问抽象层实例
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {

            return _defaultInstance;
        }

        /// <summary>
        /// 数据访问抽象层实例
        /// </summary>
        /// <param name="strConn">数据库连接字符串</param>
        /// <returns></returns>
        public static DataProvider Instance(string strConn)
        {
            return new SqlDataProvider(strConn);
        }

        #endregion
                
        #region "sys_Roles - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Roles
        /// </summary>
        /// <param name="fam">sys_RolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_RolesInsertUpdate(sys_RolesTable fam);

        /// <summary>
        /// 返回sys_RolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolesTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_RolesList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_RolesTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_RolesTable</returns>
        protected object Populatesys_Roles(IDataReader dr)
        {
            sys_RolesTable nc = new sys_RolesTable();

            if (!Convert.IsDBNull(dr["RoleID"])) nc.RoleID = Convert.ToInt32(dr["RoleID"]); // 角色ID自动ID
            if (!Convert.IsDBNull(dr["R_UserID"])) nc.R_UserID = Convert.ToInt32(dr["R_UserID"]); // 角角所属用户ID
            if (!Convert.IsDBNull(dr["R_RoleName"])) nc.R_RoleName = Convert.ToString(dr["R_RoleName"]).Trim(); // 角色名称
            if (!Convert.IsDBNull(dr["R_Description"])) nc.R_Description = Convert.ToString(dr["R_Description"]).Trim(); // 角色介绍
            return nc;
        }
        #endregion
        
        #region "sys_User - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_User
        /// </summary>
        /// <param name="fam">sys_UserTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_UserInsertUpdate(sys_UserTable fam);

        /// <summary>
        /// 返回sys_UserTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_UserList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_UserTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_UserTable</returns>
        protected object Populatesys_User(IDataReader dr)
        {
            sys_UserTable nc = new sys_UserTable();

            if (!Convert.IsDBNull(dr["UserID"])) nc.UserID = Convert.ToInt32(dr["UserID"]); // 用户ID号
            if (!Convert.IsDBNull(dr["U_LoginName"])) nc.U_LoginName = Convert.ToString(dr["U_LoginName"]).Trim(); // 登陆名
            if (!Convert.IsDBNull(dr["U_Password"])) nc.U_Password = Convert.ToString(dr["U_Password"]).Trim(); // 密码md5加密字符
            if (!Convert.IsDBNull(dr["U_CName"])) nc.U_CName = Convert.ToString(dr["U_CName"]).Trim(); // 中文姓名
            if (!Convert.IsDBNull(dr["U_EName"])) nc.U_EName = Convert.ToString(dr["U_EName"]).Trim(); // 英文名
            if (!Convert.IsDBNull(dr["U_GroupID"])) nc.U_GroupID = Convert.ToInt32(dr["U_GroupID"]); // 部门ID号与sys_Group表中GroupID关联
            if (!Convert.IsDBNull(dr["U_Email"])) nc.U_Email = Convert.ToString(dr["U_Email"]).Trim(); // 电子邮件
            if (!Convert.IsDBNull(dr["U_Type"])) nc.U_Type = Convert.ToInt32(dr["U_Type"]); // 用户类型0:超级用户1:普通用户
            if (!Convert.IsDBNull(dr["U_Status"])) nc.U_Status = Convert.ToInt32(dr["U_Status"]); // 当前状态0:正常 1:禁止
            if (!Convert.IsDBNull(dr["U_Licence"])) nc.U_Licence = Convert.ToString(dr["U_Licence"]).Trim(); // 用户序列号
            if (!Convert.IsDBNull(dr["U_Mac"])) nc.U_Mac = Convert.ToString(dr["U_Mac"]).Trim(); // 锁定机器硬件地址
            if (!Convert.IsDBNull(dr["U_Remark"])) nc.U_Remark = Convert.ToString(dr["U_Remark"]).Trim(); // 备注说明
            if (!Convert.IsDBNull(dr["U_IDCard"])) nc.U_IDCard = Convert.ToString(dr["U_IDCard"]).Trim(); // 身份证号码
            if (!Convert.IsDBNull(dr["U_Sex"])) nc.U_Sex = Convert.ToInt32(dr["U_Sex"]); // 性别1:男0:女
            if (!Convert.IsDBNull(dr["U_BirthDay"])) nc.U_BirthDay = Convert.ToDateTime(dr["U_BirthDay"]); // 出生日期
            if (!Convert.IsDBNull(dr["U_MobileNo"])) nc.U_MobileNo = Convert.ToString(dr["U_MobileNo"]).Trim(); // 手机号
            if (!Convert.IsDBNull(dr["U_UserNO"])) nc.U_UserNO = Convert.ToString(dr["U_UserNO"]).Trim(); // 员工编号
            if (!Convert.IsDBNull(dr["U_WorkStartDate"])) nc.U_WorkStartDate = Convert.ToDateTime(dr["U_WorkStartDate"]); // 到职日期
            if (!Convert.IsDBNull(dr["U_WorkEndDate"])) nc.U_WorkEndDate = Convert.ToDateTime(dr["U_WorkEndDate"]); // 离职日期
            if (!Convert.IsDBNull(dr["U_CompanyMail"])) nc.U_CompanyMail = Convert.ToString(dr["U_CompanyMail"]).Trim(); // 公司邮件地址
            if (!Convert.IsDBNull(dr["U_Title"])) nc.U_Title = Convert.ToInt32(dr["U_Title"]); // 职称与应用字段关联
            if (!Convert.IsDBNull(dr["U_Extension"])) nc.U_Extension = Convert.ToString(dr["U_Extension"]).Trim(); // 分机号
            if (!Convert.IsDBNull(dr["U_HomeTel"])) nc.U_HomeTel = Convert.ToString(dr["U_HomeTel"]).Trim(); // 家中电话
            if (!Convert.IsDBNull(dr["U_PhotoUrl"])) nc.U_PhotoUrl = Convert.ToString(dr["U_PhotoUrl"]).Trim(); // 用户照片网址
            if (!Convert.IsDBNull(dr["U_DateTime"])) nc.U_DateTime = Convert.ToDateTime(dr["U_DateTime"]); // 操作时间
            if (!Convert.IsDBNull(dr["U_LastIP"])) nc.U_LastIP = Convert.ToString(dr["U_LastIP"]).Trim(); // 最后访问IP
            if (!Convert.IsDBNull(dr["U_LastDateTime"])) nc.U_LastDateTime = Convert.ToDateTime(dr["U_LastDateTime"]); // 最后访问时间
            if (!Convert.IsDBNull(dr["U_ExtendField"])) nc.U_ExtendField = Convert.ToString(dr["U_ExtendField"]).Trim(); // 扩展字段
            return nc;
        }
        #endregion

        #region "sys_UserRoles - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_UserRoles
        /// </summary>
        /// <param name="fam">sys_UserRolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_UserRolesInsertUpdate(sys_UserRolesTable fam);

        /// <summary>
        /// 返回sys_UserRolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserRolesTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_UserRolesList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_UserRolesTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_UserRolesTable</returns>
        protected object Populatesys_UserRoles(IDataReader dr)
        {
            sys_UserRolesTable nc = new sys_UserRolesTable();

            if (!Convert.IsDBNull(dr["R_UserID"])) nc.R_UserID = Convert.ToInt32(dr["R_UserID"]); // 用户ID与sys_User表中UserID相关
            if (!Convert.IsDBNull(dr["R_RoleID"])) nc.R_RoleID = Convert.ToInt32(dr["R_RoleID"]); // 用户所属角色ID与Sys_Roles关联
            return nc;
        }
        #endregion

        #region "sys_Applications - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Applications
        /// </summary>
        /// <param name="fam">sys_ApplicationsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_ApplicationsInsertUpdate(sys_ApplicationsTable fam);

        /// <summary>
        /// 返回sys_ApplicationsTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ApplicationsTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_ApplicationsList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_ApplicationsTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_ApplicationsTable</returns>
        protected object Populatesys_Applications(IDataReader dr)
        {
            sys_ApplicationsTable nc = new sys_ApplicationsTable();

            if (!Convert.IsDBNull(dr["ApplicationID"])) nc.ApplicationID = Convert.ToInt32(dr["ApplicationID"]); // 自动ID 1:为系统管理应用
            if (!Convert.IsDBNull(dr["A_AppName"])) nc.A_AppName = Convert.ToString(dr["A_AppName"]).Trim(); // 应用名称
            if (!Convert.IsDBNull(dr["A_AppDescription"])) nc.A_AppDescription = Convert.ToString(dr["A_AppDescription"]).Trim(); // 应用介绍
            if (!Convert.IsDBNull(dr["A_AppUrl"])) nc.A_AppUrl = Convert.ToString(dr["A_AppUrl"]).Trim(); // 应用Url地址
            return nc;
        }
        #endregion

        #region "sys_Event - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Event
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_EventInsertUpdate(sys_EventTable fam);

        /// <summary>
        /// 返回sys_EventTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_EventTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_EventList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_EventTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_EventTable</returns>
        protected object Populatesys_Event(IDataReader dr)
        {
            sys_EventTable nc = new sys_EventTable();

            if (!Convert.IsDBNull(dr["EventID"])) nc.EventID = Convert.ToInt32(dr["EventID"]); // 事件ID号
            if (!Convert.IsDBNull(dr["E_U_LoginName"])) nc.E_U_LoginName = Convert.ToString(dr["E_U_LoginName"]).Trim(); // 用户名
            if (!Convert.IsDBNull(dr["E_UserID"])) nc.E_UserID = Convert.ToInt32(dr["E_UserID"]); // 操作时用户ID与sys_Users中UserID
            if (!Convert.IsDBNull(dr["E_DateTime"])) nc.E_DateTime = Convert.ToDateTime(dr["E_DateTime"]); // 事件发生的日期及时间
            if (!Convert.IsDBNull(dr["E_ApplicationID"])) nc.E_ApplicationID = Convert.ToInt32(dr["E_ApplicationID"]); // 所属应用程序ID与sys_Applicatio
            if (!Convert.IsDBNull(dr["E_A_AppName"])) nc.E_A_AppName = Convert.ToString(dr["E_A_AppName"]).Trim(); // 所属应用名称
            if (!Convert.IsDBNull(dr["E_M_Name"])) nc.E_M_Name = Convert.ToString(dr["E_M_Name"]).Trim(); // PageCode模块名称与sys_Module相同
            if (!Convert.IsDBNull(dr["E_M_PageCode"])) nc.E_M_PageCode = Convert.ToString(dr["E_M_PageCode"]).Trim(); // 发生事件时模块名称
            if (!Convert.IsDBNull(dr["E_From"])) nc.E_From = Convert.ToString(dr["E_From"]).Trim(); // 来源
            if (!Convert.IsDBNull(dr["E_Type"])) nc.E_Type = Convert.ToInt32(dr["E_Type"]); // 日记类型,1:操作日记2:安全日志3
            if (!Convert.IsDBNull(dr["E_IP"])) nc.E_IP = Convert.ToString(dr["E_IP"]).Trim(); // 客户端IP地址
            if (!Convert.IsDBNull(dr["E_Record"])) nc.E_Record = Convert.ToString(dr["E_Record"]).Trim(); // 详细描述
            return nc;
        }
        #endregion

        #region "sys_Field - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Field
        /// </summary>
        /// <param name="fam">sys_FieldTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_FieldInsertUpdate(sys_FieldTable fam);

        /// <summary>
        /// 返回sys_FieldTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_FieldList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_FieldTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_FieldTable</returns>
        protected object Populatesys_Field(IDataReader dr)
        {
            sys_FieldTable nc = new sys_FieldTable();

            if (!Convert.IsDBNull(dr["FieldID"])) nc.FieldID = Convert.ToInt32(dr["FieldID"]); // 应用字段ID号
            if (!Convert.IsDBNull(dr["F_Key"])) nc.F_Key = Convert.ToString(dr["F_Key"]).Trim(); // 应用字段关键字
            if (!Convert.IsDBNull(dr["F_CName"])) nc.F_CName = Convert.ToString(dr["F_CName"]).Trim(); // 应用字段中文说明
            if (!Convert.IsDBNull(dr["F_Remark"])) nc.F_Remark = Convert.ToString(dr["F_Remark"]).Trim(); // 描述说明
            return nc;
        }
        #endregion

        #region "sys_FieldValue - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_FieldValue
        /// </summary>
        /// <param name="fam">sys_FieldValueTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_FieldValueInsertUpdate(sys_FieldValueTable fam);

        /// <summary>
        /// 返回sys_FieldValueTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldValueTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_FieldValueList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_FieldValueTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_FieldValueTable</returns>
        protected object Populatesys_FieldValue(IDataReader dr)
        {
            sys_FieldValueTable nc = new sys_FieldValueTable();

            if (!Convert.IsDBNull(dr["ValueID"])) nc.ValueID = Convert.ToInt32(dr["ValueID"]); // 索引ID号
            if (!Convert.IsDBNull(dr["V_F_Key"])) nc.V_F_Key = Convert.ToString(dr["V_F_Key"]).Trim(); // 与sys_Field表中F_Key字段关联
            if (!Convert.IsDBNull(dr["V_Text"])) nc.V_Text = Convert.ToString(dr["V_Text"]).Trim(); // 中文说明
            if (!Convert.IsDBNull(dr["V_Code"])) nc.V_Code = Convert.ToString(dr["V_Code"]).Trim(); // 编码
            if (!Convert.IsDBNull(dr["V_ShowOrder"])) nc.V_ShowOrder = Convert.ToInt32(dr["V_ShowOrder"]); // 同级显示顺序
            return nc;
        }
        #endregion

        #region "sys_Group - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Group
        /// </summary>
        /// <param name="fam">sys_GroupTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_GroupInsertUpdate(sys_GroupTable fam);

        /// <summary>
        /// 返回sys_GroupTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_GroupTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_GroupList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_GroupTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_GroupTable</returns>
        protected object Populatesys_Group(IDataReader dr)
        {
            sys_GroupTable nc = new sys_GroupTable();

            if (!Convert.IsDBNull(dr["GroupID"])) nc.GroupID = Convert.ToInt32(dr["GroupID"]); // 分类ID号
            if (!Convert.IsDBNull(dr["G_CName"])) nc.G_CName = Convert.ToString(dr["G_CName"]).Trim(); // 分类中文说明
            if (!Convert.IsDBNull(dr["G_ParentID"])) nc.G_ParentID = Convert.ToInt32(dr["G_ParentID"]); // 上级分类ID0:为最高级
            if (!Convert.IsDBNull(dr["G_ShowOrder"])) nc.G_ShowOrder = Convert.ToInt32(dr["G_ShowOrder"]); // 显示顺序
            if (!Convert.IsDBNull(dr["G_Level"])) nc.G_Level = Convert.ToInt32(dr["G_Level"]); // 当前分类所在层数
            if (!Convert.IsDBNull(dr["G_ChildCount"])) nc.G_ChildCount = Convert.ToInt32(dr["G_ChildCount"]); // 当前分类子分类数
            if (!Convert.IsDBNull(dr["G_Delete"])) nc.G_Delete = Convert.ToInt32(dr["G_Delete"]); // 是否删除1:是0:否
            return nc;
        }
        #endregion

        #region "sys_Module - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Module
        /// </summary>
        /// <param name="fam">sys_ModuleTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_ModuleInsertUpdate(sys_ModuleTable fam);

        /// <summary>
        /// 返回sys_ModuleTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_ModuleList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_ModuleTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_ModuleTable</returns>
        protected object Populatesys_Module(IDataReader dr)
        {
            sys_ModuleTable nc = new sys_ModuleTable();

            if (!Convert.IsDBNull(dr["ModuleID"])) nc.ModuleID = Convert.ToInt32(dr["ModuleID"]); // 功能模块ID号
            if (!Convert.IsDBNull(dr["M_ApplicationID"])) nc.M_ApplicationID = Convert.ToInt32(dr["M_ApplicationID"]); // 所属应用程序ID
            if (!Convert.IsDBNull(dr["M_ParentID"])) nc.M_ParentID = Convert.ToInt32(dr["M_ParentID"]); // 所属父级模块ID与ModuleID关联,0为顶级
            if (!Convert.IsDBNull(dr["M_PageCode"])) nc.M_PageCode = Convert.ToString(dr["M_PageCode"]).Trim(); // 模块编码Parent为0,则为S00(xx),否则为S00M00(xx)
            if (!Convert.IsDBNull(dr["M_CName"])) nc.M_CName = Convert.ToString(dr["M_CName"]).Trim(); // 模块/栏目名称当ParentID为0为模块名称
            if (!Convert.IsDBNull(dr["M_Directory"])) nc.M_Directory = Convert.ToString(dr["M_Directory"]).Trim(); // 模块/栏目目录名
            if (!Convert.IsDBNull(dr["M_OrderLevel"])) nc.M_OrderLevel = Convert.ToString(dr["M_OrderLevel"]).Trim(); // 当前所在排序级别支持双层99级菜单
            if (!Convert.IsDBNull(dr["M_IsSystem"])) nc.M_IsSystem = Convert.ToInt32(dr["M_IsSystem"]); // 是否为系统模块1:是0:否如为系统则无法修改
            if (!Convert.IsDBNull(dr["M_Close"])) nc.M_Close = Convert.ToInt32(dr["M_Close"]); // 是否关闭1:是0:否
            if (!Convert.IsDBNull(dr["M_Icon"])) nc.M_Icon = dr["M_Icon"].ToString().Trim(); // 模块/栏目名称当ParentID为0为模块名称
            return nc;
        }
        #endregion
        #region "sys_RoleApplication - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_RoleApplication
        /// </summary>
        /// <param name="fam">sys_RoleApplicationTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_RoleApplicationInsertUpdate(sys_RoleApplicationTable fam);

        /// <summary>
        /// 返回sys_RoleApplicationTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RoleApplicationTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_RoleApplicationList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_RoleApplicationTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_RoleApplicationTable</returns>
        protected object Populatesys_RoleApplication(IDataReader dr)
        {
            sys_RoleApplicationTable nc = new sys_RoleApplicationTable();

            if (!Convert.IsDBNull(dr["A_RoleID"])) nc.A_RoleID = Convert.ToInt32(dr["A_RoleID"]); // 角色ID与sys_Roles中RoleID相关
            if (!Convert.IsDBNull(dr["A_ApplicationID"])) nc.A_ApplicationID = Convert.ToInt32(dr["A_ApplicationID"]); // 应用ID与sys_Applications中Appl
            return nc;
        }
        #endregion

        #region "sys_RolePermission - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_RolePermission
        /// </summary>
        /// <param name="fam">sys_RolePermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_RolePermissionInsertUpdate(sys_RolePermissionTable fam);

        /// <summary>
        /// 返回sys_RolePermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolePermissionTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_RolePermissionList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_RolePermissionTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_RolePermissionTable</returns>
        protected object Populatesys_RolePermission(IDataReader dr)
        {
            sys_RolePermissionTable nc = new sys_RolePermissionTable();

            if (!Convert.IsDBNull(dr["PermissionID"])) nc.PermissionID = Convert.ToInt32(dr["PermissionID"]); // 角色应用权限自动ID
            if (!Convert.IsDBNull(dr["P_RoleID"])) nc.P_RoleID = Convert.ToInt32(dr["P_RoleID"]); // 角色ID与sys_Roles表中RoleID相
            if (!Convert.IsDBNull(dr["P_ApplicationID"])) nc.P_ApplicationID = Convert.ToInt32(dr["P_ApplicationID"]); // 角色所属应用ID与sys_Applicatio
            if (!Convert.IsDBNull(dr["P_PageCode"])) nc.P_PageCode = Convert.ToString(dr["P_PageCode"]).Trim(); // 角色应用中页面权限代码
            if (!Convert.IsDBNull(dr["P_Value"])) nc.P_Value = Convert.ToInt32(dr["P_Value"]); // 权限值
            return nc;
        }
        #endregion

        #region "sys_SystemInfo - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_SystemInfo
        /// </summary>
        /// <param name="fam">sys_SystemInfoTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_SystemInfoInsertUpdate(sys_SystemInfoTable fam);

        /// <summary>
        /// 返回sys_SystemInfoTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_SystemInfoTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_SystemInfoList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_SystemInfoTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_SystemInfoTable</returns>
        protected object Populatesys_SystemInfo(IDataReader dr)
        {
            sys_SystemInfoTable nc = new sys_SystemInfoTable();

            if (!Convert.IsDBNull(dr["SystemID"])) nc.SystemID = Convert.ToInt32(dr["SystemID"]); // 自动ID
            if (!Convert.IsDBNull(dr["S_Name"])) nc.S_Name = Convert.ToString(dr["S_Name"]).Trim(); // 系统名称
            if (!Convert.IsDBNull(dr["S_Version"])) nc.S_Version = Convert.ToString(dr["S_Version"]).Trim(); // 版本号
            if (!Convert.IsDBNull(dr["S_Licensed"])) nc.S_Licensed = Convert.ToString(dr["S_Licensed"]).Trim(); // 序列号
            if (!Convert.IsDBNull(dr["S_SystemConfigData"])) nc.S_SystemConfigData = FrameSystemInfo.Deserialize_sys_ConfigDataTable((byte[])dr["S_SystemConfigData"]); // 系统配置信息
            if (nc.S_SystemConfigData.C_UploadSizeKb == 0)
                nc.S_SystemConfigData.C_UploadSizeKb = 512;
            if (nc.S_SystemConfigData.C_LoginErrorDisableMinute == 0)
                nc.S_SystemConfigData.C_LoginErrorDisableMinute = 30;
            if (nc.S_SystemConfigData.C_LoginErrorMaxNum == 0)
                nc.S_SystemConfigData.C_LoginErrorMaxNum = 5;
            return nc;
        }
        #endregion

        #region "sys_Online - CommonDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Online
        /// </summary>
        /// <param name="fam">sys_OnlineTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_OnlineInsertUpdate(sys_OnlineTable fam);

        /// <summary>
        /// 返回sys_OnlineTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_OnlineTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_OnlineList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_OnlineTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_OnlineTable</returns>
        protected object Populatesys_Online(IDataReader dr)
        {
            sys_OnlineTable nc = new sys_OnlineTable();

            if (!Convert.IsDBNull(dr["OnlineID"])) nc.OnlineID = Convert.ToInt32(dr["OnlineID"]); // 自动ID
            if (!Convert.IsDBNull(dr["O_SessionID"])) nc.O_SessionID = Convert.ToString(dr["O_SessionID"]).Trim(); // 用户SessionID
            if (!Convert.IsDBNull(dr["O_UserName"])) nc.O_UserName = Convert.ToString(dr["O_UserName"]).Trim(); // 用户名
            if (!Convert.IsDBNull(dr["O_Ip"])) nc.O_Ip = Convert.ToString(dr["O_Ip"]).Trim(); // 用户IP地址
            if (!Convert.IsDBNull(dr["O_LoginTime"])) nc.O_LoginTime = Convert.ToDateTime(dr["O_LoginTime"]); // 登陆时间
            if (!Convert.IsDBNull(dr["O_LastTime"])) nc.O_LastTime = Convert.ToDateTime(dr["O_LastTime"]); // 最后访问时间
            if (!Convert.IsDBNull(dr["O_LastUrl"])) nc.O_LastUrl = Convert.ToString(dr["O_LastUrl"]).Trim(); // 最后请求网站
            return nc;
        }
        #endregion

        #region "sys_ModuleExtPermission - DataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_ModuleExtPermission
        /// </summary>
        /// <param name="fam">sys_ModuleExtPermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int sys_ModuleExtPermissionInsertUpdate(sys_ModuleExtPermissionTable fam);

        /// <summary>
        /// 返回sys_ModuleExtPermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleExtPermissionTable实体类的ArrayList对象</returns>
        public abstract ArrayList sys_ModuleExtPermissionList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为sys_ModuleExtPermissionTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>sys_ModuleExtPermissionTable</returns>
        protected object Populatesys_ModuleExtPermission(IDataReader dr)
        {
            sys_ModuleExtPermissionTable nc = new sys_ModuleExtPermissionTable();

            if (!Convert.IsDBNull(dr["ExtPermissionID"])) nc.ExtPermissionID = Convert.ToInt32(dr["ExtPermissionID"]); // 扩展权限ID
            if (!Convert.IsDBNull(dr["ModuleID"])) nc.ModuleID = Convert.ToInt32(dr["ModuleID"]); // 模块ID
            if (!Convert.IsDBNull(dr["PermissionName"])) nc.PermissionName = Convert.ToString(dr["PermissionName"]).Trim(); // 权限名称
            if (!Convert.IsDBNull(dr["PermissionValue"])) nc.PermissionValue = Convert.ToInt32(dr["PermissionValue"]); // 权限值
            return nc;
        }
        #endregion
             
        #region "获取表中字段值"
        /// <summary>
        /// 获取表中字段值
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="table_fileds">字段</param>
        /// <param name="where_fileds">查询条件字段</param>
        /// <param name="where_value">查询值</param>
        /// <returns></returns>
        public abstract string get_table_fileds(string table_name, string table_fileds, string where_fileds, string where_value);
        #endregion

        #region "列新表中字段值"
        /// <summary>
        /// 列新表中字段值
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Table_FiledsValue">需要列新值(不用带Set)</param>
        /// <param name="Wheres">更新条件(需带Where)</param>
        /// <returns></returns>
        public abstract int Update_Table_Fileds(string Table, string Table_FiledsValue, string Wheres);
        #endregion

        #region PM_Project - DataProvider
        /// <summary>
        /// 返回PM_Project实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PM_Project实体类的ArrayList对象</returns>
        public abstract ArrayList PM_ProjectsList(QueryParam qp, out int RecordCount);

        /// <summary>
        /// 将记录集转为PM_ProjectsTable实体类
        /// </summary>
        /// <param name="dr">记录集</param>
        /// <returns>PM_ProjectsTable</returns>
        protected object PopulatePM_ProjectsTable(IDataReader dr)
        {
            PM_ProjectsTable nc = new PM_ProjectsTable();

            if (!Convert.IsDBNull(dr["ProjectId"])) nc.ProjectId = Convert.ToString(dr["ProjectId"]).Trim();
            if (!Convert.IsDBNull(dr["ProjectNo"])) nc.ProjectNo = Convert.ToString(dr["ProjectNo"]).Trim();
            if (!Convert.IsDBNull(dr["ProjectName"])) nc.ProjectName = Convert.ToString(dr["ProjectName"]).Trim();
            if (!Convert.IsDBNull(dr["ProjectStatus"])) nc.ProjectStatus = Convert.ToInt32(dr["ProjectStatus"]);
            if (!Convert.IsDBNull(dr["ProjectStartTime"])) nc.ProjectStartTime = Convert.ToDateTime(dr["ProjectStartTime"]);
            if (!Convert.IsDBNull(dr["ProjectDuration"])) nc.ProjectDuration = Convert.ToDouble(dr["ProjectDuration"]);
            if (!Convert.IsDBNull(dr["ProjectBrief"])) nc.ProjectBrief = Convert.ToString(dr["ProjectBrief"]).Trim();
            if (!Convert.IsDBNull(dr["ProjectComments"])) nc.ProjectComments = Convert.ToString(dr["ProjectComments"]).Trim();
            if (!Convert.IsDBNull(dr["ProjectIcon"])) nc.ProjectIcon = Convert.ToString(dr["ProjectIcon"]).Trim();
            return nc;
        }
        /// <summary>
        /// 新增/删除/修改 PM_ProjectsTable
        /// </summary>
        /// <param name="fam">PM_ProjectsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public abstract int PM_ProjectsTableInsertUpdate(PM_ProjectsTable fam);
        #endregion
    }
}
