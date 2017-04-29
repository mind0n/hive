/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				SqlDataProvider.cs                         	                		*
 *      Description:																*
 *				 Sql数据库操作类    	       						        	    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using PmSys.Components;

namespace PmSys.Data
{
    /// <summary>
    /// Sql数据库操作类
    /// </summary>
    public class SqlDataProvider : DataProvider
    {

        #region "SqlDataProvider"
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string ConnString = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlDataProvider()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            ConnString = Common.GetConnString;
        }

        /// <summary>
        /// 连接数据库字符串
        /// </summary>
        /// <param name="strConn"></param>
        public SqlDataProvider(string strConn)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            ConnString = strConn;
        }


        /// <summary>
        /// 获取数据连接
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetSqlConnection()
        {
            try
            {
                return new SqlConnection(ConnString);
            }
            catch
            {
                throw new Exception("没有提供数据庫连接字符串！");
            }
        }
        #endregion

        #region "sys_Applications - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Applications
        /// </summary>
        /// <param name="fam">sys_ApplicationsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_ApplicationsInsertUpdate(sys_ApplicationsTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_ApplicationsInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = fam.ApplicationID;  //自动ID 1:为系统管理应用
                cmd.Parameters.Add("@A_AppName", SqlDbType.NVarChar).Value = fam.A_AppName;  //应用名称
                cmd.Parameters.Add("@A_AppDescription", SqlDbType.NVarChar).Value = fam.A_AppDescription;  //应用介绍	
                cmd.Parameters.Add("@A_AppUrl", SqlDbType.VarChar).Value = fam.A_AppUrl;  //应用Url地址
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_ApplicationsTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ApplicationsTable实体类的ArrayList对象</returns>
        public override ArrayList sys_ApplicationsList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Applications);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_Event - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Event
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_EventInsertUpdate(sys_EventTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_EventInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@EventID", SqlDbType.Int).Value = fam.EventID;  //事件ID号
                cmd.Parameters.Add("@E_U_LoginName", SqlDbType.NVarChar).Value = fam.E_U_LoginName;  //用户名
                cmd.Parameters.Add("@E_UserID", SqlDbType.Int).Value = fam.E_UserID;  //操作时用户ID与sys_Users中UserID
                cmd.Parameters.Add("@E_DateTime", SqlDbType.DateTime).Value = fam.E_DateTime;  //事件发生的日期及时间
                cmd.Parameters.Add("@E_ApplicationID", SqlDbType.Int).Value = fam.E_ApplicationID;  //所属应用程序ID与sys_Applicatio
                cmd.Parameters.Add("@E_A_AppName", SqlDbType.NVarChar).Value = fam.E_A_AppName;  //所属应用名称
                cmd.Parameters.Add("@E_M_Name", SqlDbType.NVarChar).Value = fam.E_M_Name;  //PageCode模块名称与sys_Module相同	
                cmd.Parameters.Add("@E_M_PageCode", SqlDbType.VarChar).Value = fam.E_M_PageCode;  //发生事件时模块名称
                cmd.Parameters.Add("@E_From", SqlDbType.NVarChar).Value = fam.E_From;  //来源
                cmd.Parameters.Add("@E_Type", SqlDbType.Int).Value = fam.E_Type;  //日记类型,1:操作日记2:安全日志3	
                cmd.Parameters.Add("@E_IP", SqlDbType.VarChar).Value = fam.E_IP;  //客户端IP地址
                cmd.Parameters.Add("@E_Record", SqlDbType.NVarChar).Value = fam.E_Record;  //详细描述
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_EventTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_EventTable实体类的ArrayList对象</returns>
        public override ArrayList sys_EventList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Event);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_Field - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Field
        /// </summary>
        /// <param name="fam">sys_FieldTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_FieldInsertUpdate(sys_FieldTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_FieldInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@FieldID", SqlDbType.Int).Value = fam.FieldID;  //应用字段ID号	
                cmd.Parameters.Add("@F_Key", SqlDbType.VarChar).Value = fam.F_Key;  //应用字段关键字
                cmd.Parameters.Add("@F_CName", SqlDbType.NVarChar).Value = fam.F_CName;  //应用字段中文说明
                cmd.Parameters.Add("@F_Remark", SqlDbType.NVarChar).Value = fam.F_Remark;  //描述说明
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_FieldTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldTable实体类的ArrayList对象</returns>
        public override ArrayList sys_FieldList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Field);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_FieldValue - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_FieldValue
        /// </summary>
        /// <param name="fam">sys_FieldValueTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_FieldValueInsertUpdate(sys_FieldValueTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_FieldValueInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@ValueID", SqlDbType.Int).Value = fam.ValueID;  //索引ID号	
                cmd.Parameters.Add("@V_F_Key", SqlDbType.VarChar).Value = fam.V_F_Key;  //与sys_Field表中F_Key字段关联
                cmd.Parameters.Add("@V_Text", SqlDbType.NVarChar).Value = fam.V_Text;  //中文说明
                cmd.Parameters.Add("@V_Code", SqlDbType.NVarChar).Value = fam.V_Code;  //编码
                cmd.Parameters.Add("@V_ShowOrder", SqlDbType.Int).Value = fam.V_ShowOrder;  //同级显示顺序
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_FieldValueTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldValueTable实体类的ArrayList对象</returns>
        public override ArrayList sys_FieldValueList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_FieldValue);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_Group - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Group
        /// </summary>
        /// <param name="fam">sys_GroupTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_GroupInsertUpdate(sys_GroupTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_GroupInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@GroupID", SqlDbType.Int).Value = fam.GroupID;  //分类ID号
                cmd.Parameters.Add("@G_CName", SqlDbType.NVarChar).Value = fam.G_CName;  //分类中文说明
                cmd.Parameters.Add("@G_ParentID", SqlDbType.Int).Value = fam.G_ParentID;  //上级分类ID0:为最高级
                cmd.Parameters.Add("@G_ShowOrder", SqlDbType.Int).Value = fam.G_ShowOrder;  //显示顺序
                cmd.Parameters.Add("@G_Level", SqlDbType.Int).Value = fam.G_Level;  //当前分类所在层数
                cmd.Parameters.Add("@G_ChildCount", SqlDbType.Int).Value = fam.G_ChildCount;  //当前分类子分类数
                cmd.Parameters.Add("@G_Delete", SqlDbType.Int).Value = fam.G_Delete;  //是否删除1:是0:否
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_GroupTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_GroupTable实体类的ArrayList对象</returns>
        public override ArrayList sys_GroupList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Group);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_Module - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Module
        /// </summary>
        /// <param name="fam">sys_ModuleTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_ModuleInsertUpdate(sys_ModuleTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_ModuleInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = fam.ModuleID;  //功能模块ID号
                cmd.Parameters.Add("@M_ApplicationID", SqlDbType.Int).Value = fam.M_ApplicationID;  //所属应用程序ID
                cmd.Parameters.Add("@M_ParentID", SqlDbType.Int).Value = fam.M_ParentID;  //所属父级模块ID与ModuleID关联,0为顶级	
                cmd.Parameters.Add("@M_PageCode", SqlDbType.VarChar).Value = fam.M_PageCode;  //模块编码Parent为0,则为S00(xx),否则为S00M00(xx)
                cmd.Parameters.Add("@M_CName", SqlDbType.NVarChar).Value = fam.M_CName;  //模块/栏目名称当ParentID为0为模块名称
                cmd.Parameters.Add("@M_Directory", SqlDbType.NVarChar).Value = fam.M_Directory;  //模块/栏目目录名	
                cmd.Parameters.Add("@M_OrderLevel", SqlDbType.VarChar).Value = fam.M_OrderLevel;  //当前所在排序级别支持双层99级菜单
                cmd.Parameters.Add("@M_IsSystem", SqlDbType.Int).Value = fam.M_IsSystem;  //是否为系统模块1:是0:否如为系统则无法修改
                cmd.Parameters.Add("@M_Close", SqlDbType.Int).Value = fam.M_Close;  //是否关闭1:是0:否
                cmd.Parameters.Add("@M_Icon", SqlDbType.NVarChar).Value = fam.M_Icon;  //默认显示图标
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_ModuleTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleTable实体类的ArrayList对象</returns>
        public override ArrayList sys_ModuleList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Module);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_RolePermission - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_RolePermission
        /// </summary>
        /// <param name="fam">sys_RolePermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_RolePermissionInsertUpdate(sys_RolePermissionTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_RolePermissionInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@PermissionID", SqlDbType.Int).Value = fam.PermissionID;  //角色应用权限自动ID
                cmd.Parameters.Add("@P_RoleID", SqlDbType.Int).Value = fam.P_RoleID;  //角色ID与sys_Roles表中RoleID相
                cmd.Parameters.Add("@P_ApplicationID", SqlDbType.Int).Value = fam.P_ApplicationID;  //角色所属应用ID与sys_Applicatio	
                cmd.Parameters.Add("@P_PageCode", SqlDbType.VarChar).Value = fam.P_PageCode;  //角色应用中页面权限代码
                cmd.Parameters.Add("@P_Value", SqlDbType.Int).Value = fam.P_Value;  //权限值
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_RolePermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolePermissionTable实体类的ArrayList对象</returns>
        public override ArrayList sys_RolePermissionList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_RolePermission);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion


        #region "sys_Roles - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Roles
        /// </summary>
        /// <param name="fam">sys_RolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_RolesInsertUpdate(sys_RolesTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_RolesInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = fam.RoleID;  //角色ID自动ID
                cmd.Parameters.Add("@R_UserID", SqlDbType.Int).Value = fam.R_UserID;  //角角所属用户ID
                cmd.Parameters.Add("@R_RoleName", SqlDbType.NVarChar).Value = fam.R_RoleName;  //角色名称
                cmd.Parameters.Add("@R_Description", SqlDbType.NVarChar).Value = fam.R_Description;  //角色介绍
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_RolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolesTable实体类的ArrayList对象</returns>
        public override ArrayList sys_RolesList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Roles);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_User - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_User
        /// </summary>
        /// <param name="fam">sys_UserTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_UserInsertUpdate(sys_UserTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_UserInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = fam.UserID;  //用户ID号
                cmd.Parameters.Add("@U_LoginName", SqlDbType.NVarChar).Value = fam.U_LoginName;  //登陆名	
                cmd.Parameters.Add("@U_Password", SqlDbType.VarChar).Value = fam.U_Password;  //密码md5加密字符
                cmd.Parameters.Add("@U_CName", SqlDbType.NVarChar).Value = fam.U_CName;  //中文姓名	
                cmd.Parameters.Add("@U_EName", SqlDbType.VarChar).Value = fam.U_EName;  //英文名
                cmd.Parameters.Add("@U_GroupID", SqlDbType.Int).Value = fam.U_GroupID;  //部门ID号与sys_Group表中GroupID关联	
                cmd.Parameters.Add("@U_Email", SqlDbType.VarChar).Value = fam.U_Email;  //电子邮件
                cmd.Parameters.Add("@U_Type", SqlDbType.Int).Value = fam.U_Type;  //用户类型0:超级用户1:普通用户
                cmd.Parameters.Add("@U_Status", SqlDbType.Int).Value = fam.U_Status;  //当前状态0:正常 1:禁止	
                cmd.Parameters.Add("@U_Licence", SqlDbType.VarChar).Value = fam.U_Licence;  //用户序列号	
                cmd.Parameters.Add("@U_Mac", SqlDbType.VarChar).Value = fam.U_Mac;  //锁定机器硬件地址
                cmd.Parameters.Add("@U_Remark", SqlDbType.NVarChar).Value = fam.U_Remark;  //备注说明	
                cmd.Parameters.Add("@U_IDCard", SqlDbType.VarChar).Value = fam.U_IDCard;  //身份证号码
                cmd.Parameters.Add("@U_Sex", SqlDbType.Int).Value = fam.U_Sex;  //性别1:男0:女
                if (fam.U_BirthDay.HasValue)
                    cmd.Parameters.Add("@U_BirthDay", SqlDbType.DateTime).Value = fam.U_BirthDay;  //出生日期	
                else
                    cmd.Parameters.Add("@U_BirthDay", SqlDbType.DateTime).Value = DBNull.Value;  //出生日期	
                cmd.Parameters.Add("@U_MobileNo", SqlDbType.VarChar).Value = fam.U_MobileNo;  //手机号	
                cmd.Parameters.Add("@U_UserNO", SqlDbType.VarChar).Value = fam.U_UserNO;  //员工编号
                if (fam.U_WorkStartDate.HasValue)
                    cmd.Parameters.Add("@U_WorkStartDate", SqlDbType.DateTime).Value = fam.U_WorkStartDate;  //到职日期
                else
                    cmd.Parameters.Add("@U_WorkStartDate", SqlDbType.DateTime).Value = DBNull.Value;  //到职日期
                if (fam.U_WorkEndDate.HasValue)
                    cmd.Parameters.Add("@U_WorkEndDate", SqlDbType.DateTime).Value = fam.U_WorkEndDate;  //离职日期	
                else
                    cmd.Parameters.Add("@U_WorkEndDate", SqlDbType.DateTime).Value = DBNull.Value;  //离职日期	
                cmd.Parameters.Add("@U_CompanyMail", SqlDbType.VarChar).Value = fam.U_CompanyMail;  //公司邮件地址
                cmd.Parameters.Add("@U_Title", SqlDbType.Int).Value = fam.U_Title;  //职称与应用字段关联	
                cmd.Parameters.Add("@U_Extension", SqlDbType.VarChar).Value = fam.U_Extension;  //分机号	
                cmd.Parameters.Add("@U_HomeTel", SqlDbType.VarChar).Value = fam.U_HomeTel;  //家中电话
                cmd.Parameters.Add("@U_PhotoUrl", SqlDbType.NVarChar).Value = fam.U_PhotoUrl;  //用户照片网址
                cmd.Parameters.Add("@U_DateTime", SqlDbType.DateTime).Value = fam.U_DateTime;  //操作时间	
                cmd.Parameters.Add("@U_LastIP", SqlDbType.VarChar).Value = fam.U_LastIP;  //最后访问IP
                cmd.Parameters.Add("@U_LastDateTime", SqlDbType.DateTime).Value = fam.U_LastDateTime;  //最后访问时间	
                cmd.Parameters.Add("@U_ExtendField", SqlDbType.NText).Value = fam.U_ExtendField;  //扩展字段
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_UserTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserTable实体类的ArrayList对象</returns>
        public override ArrayList sys_UserList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_User);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_RoleApplication - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_RoleApplication
        /// </summary>
        /// <param name="fam">sys_RoleApplicationTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_RoleApplicationInsertUpdate(sys_RoleApplicationTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_RoleApplicationInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@A_RoleID", SqlDbType.Int).Value = fam.A_RoleID;  //角色ID与sys_Roles中RoleID相关
                cmd.Parameters.Add("@A_ApplicationID", SqlDbType.Int).Value = fam.A_ApplicationID;  //应用ID与sys_Applications中Appl
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_RoleApplicationTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RoleApplicationTable实体类的ArrayList对象</returns>
        public override ArrayList sys_RoleApplicationList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_RoleApplication);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_UserRoles - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_UserRoles
        /// </summary>
        /// <param name="fam">sys_UserRolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_UserRolesInsertUpdate(sys_UserRolesTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_UserRolesInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@R_UserID", SqlDbType.Int).Value = fam.R_UserID;  //用户ID与sys_User表中UserID相关
                cmd.Parameters.Add("@R_RoleID", SqlDbType.Int).Value = fam.R_RoleID;  //用户所属角色ID与Sys_Roles关联
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_UserRolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserRolesTable实体类的ArrayList对象</returns>
        public override ArrayList sys_UserRolesList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_UserRoles);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_SystemInfo - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_SystemInfo
        /// </summary>
        /// <param name="fam">sys_SystemInfoTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_SystemInfoInsertUpdate(sys_SystemInfoTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_SystemInfoInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@SystemID", SqlDbType.Int).Value = fam.SystemID;  //自动ID
                cmd.Parameters.Add("@S_Name", SqlDbType.NVarChar).Value = fam.S_Name;  //系统名称
                cmd.Parameters.Add("@S_Version", SqlDbType.NVarChar).Value = fam.S_Version;  //版本号
                cmd.Parameters.Add("@S_Licensed", SqlDbType.VarChar).Value = fam.S_Licensed;  //序列号
                cmd.Parameters.Add("@S_SystemConfigData", SqlDbType.Image).Value = FrameSystemInfo.Serializable_sys_ConfigDataTable(fam.S_SystemConfigData);  //系统配置信息
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_SystemInfoTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_SystemInfoTable实体类的ArrayList对象</returns>
        public override ArrayList sys_SystemInfoList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_SystemInfo);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_Online - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_Online
        /// </summary>
        /// <param name="fam">sys_OnlineTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_OnlineInsertUpdate(sys_OnlineTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_OnlineInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@OnlineID", SqlDbType.Int).Value = fam.OnlineID;  //自动ID	
                cmd.Parameters.Add("@O_SessionID", SqlDbType.VarChar).Value = fam.O_SessionID;  //用户SessionID
                cmd.Parameters.Add("@O_UserName", SqlDbType.NVarChar).Value = fam.O_UserName;  //用户名	
                cmd.Parameters.Add("@O_Ip", SqlDbType.VarChar).Value = fam.O_Ip;  //用户IP地址
                cmd.Parameters.Add("@O_LoginTime", SqlDbType.DateTime).Value = fam.O_LoginTime;  //登陆时间
                cmd.Parameters.Add("@O_LastTime", SqlDbType.DateTime).Value = fam.O_LastTime;  //最后访问时间
                cmd.Parameters.Add("@O_LastUrl", SqlDbType.NVarChar).Value = fam.O_LastUrl;  //最后请求网站
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_OnlineTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_OnlineTable实体类的ArrayList对象</returns>
        public override ArrayList sys_OnlineList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_Online);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "sys_ModuleExtPermission - SQLDataProvider"
        /// <summary>
        /// 新增/删除/修改 sys_ModuleExtPermission
        /// </summary>
        /// <param name="fam">sys_ModuleExtPermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int sys_ModuleExtPermissionInsertUpdate(sys_ModuleExtPermissionTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("sys_ModuleExtPermissionInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                cmd.Parameters.Add("@ExtPermissionID", SqlDbType.Int).Value = fam.ExtPermissionID;  //扩展权限ID
                cmd.Parameters.Add("@ModuleID", SqlDbType.Int).Value = fam.ModuleID;  //模块ID
                cmd.Parameters.Add("@PermissionName", SqlDbType.NVarChar).Value = fam.PermissionName;  //权限名称
                cmd.Parameters.Add("@PermissionValue", SqlDbType.Int).Value = fam.PermissionValue;  //权限值
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }

        /// <summary>
        /// 返回sys_ModuleExtPermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleExtPermissionTable实体类的ArrayList对象</returns>
        public override ArrayList sys_ModuleExtPermissionList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.Populatesys_ModuleExtPermission);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }

        #endregion

        #region "常用函数"
        /// <summary>
        /// 获取表中字段值
        /// </summary>
        /// <param name="table_name">表名</param>
        /// <param name="table_fileds">字段</param>
        /// <param name="where_fileds">查询条件字段</param>
        /// <param name="where_value">查询值</param>
        /// <returns></returns>
        public override string get_table_fileds(string table_name, string table_fileds, string where_fileds, string where_value)
        {
            where_value = Common.inSQL(where_value);
            string rStr = "";
            using (SqlConnection Conn = GetSqlConnection())
            {
                string strSql = string.Format("select {0} from {1} where upper({2})='{3}'", table_fileds, table_name, where_fileds, where_value);
                SqlCommand cmd = new SqlCommand(strSql, Conn);
                cmd.CommandType = CommandType.Text;
                Conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    rStr = dr[0].ToString();
                }
                dr.Close();
                dr.Dispose();
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rStr;
        }
        #endregion

        #region "更新表中字段值"
        /// <summary>
        /// 更新表中字段值
        /// </summary>
        /// <param name="Table">表名</param>
        /// <param name="Table_FiledsValue">需要更新值(不用带Set)</param>
        /// <param name="Wheres">更新条件(不用带Where)</param>
        /// <returns></returns>
        public override int Update_Table_Fileds(string Table, string Table_FiledsValue, string Wheres)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                string strSql = string.Format("Update {0} Set {1}  Where {2}", Table, Table_FiledsValue, Wheres);
                SqlCommand cmd = new SqlCommand(strSql, Conn);
                cmd.CommandType = CommandType.Text;
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }
        #endregion

        #region 公共查询数据函数Sql存储过程版
        /// <summary>
        /// 公共查询数据函数Sql存储过程版
        /// </summary>
        /// <param name="pd">委托对象</param>
        /// <param name="pp">查询字符串</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>返回记录集ArrayList</returns>
        private ArrayList GetObjectList(PopulateDelegate pd, QueryParam pp, out int RecordCount)
        {
            ArrayList lst = new ArrayList();
            RecordCount = 0;
            using (SqlConnection conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("SupesoftPage", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // 设置参数
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 500).Value = pp.TableName;
                cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 500).Value = pp.ReturnFields;
                cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 500).Value = pp.Where;
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pp.PageIndex;
                cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pp.PageSize;
                cmd.Parameters.Add("@Orderfld", SqlDbType.NVarChar, 200).Value = pp.Orderfld;
                cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = pp.OrderType;
                // 执行
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(pd(dr));
                }
                // 取记录总数 及页数
                if (dr.NextResult())
                {
                    if (dr.Read())
                    {
                        RecordCount = Convert.ToInt32(dr["RecordCount"]);
                    }
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lst;
        }
        #endregion

        #region PM_Projects - SQLDataProvider

        /// <summary>
        /// 返回PM_ProjectsTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PM_ProjectsTable实体类的ArrayList对象</returns>
        public override ArrayList PM_ProjectsList(QueryParam qp, out int RecordCount)
        {
            PopulateDelegate mypd = new PopulateDelegate(base.PopulatePM_ProjectsTable);
            return this.GetObjectList(mypd, qp, out RecordCount);
        }
        /// <summary>
        /// 新增/删除/修改 PM_ProjectsTable
        /// </summary>
        /// <param name="fam">PM_ProjectsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public override int PM_ProjectsTableInsertUpdate(PM_ProjectsTable fam)
        {
            int rInt = 0;
            using (SqlConnection Conn = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand("PM_ProjectsInsertUpdateDelete", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //设置参数
                cmd.Parameters.Add("@DB_Option_Action_", SqlDbType.NVarChar).Value = fam.DB_Option_Action_; //操作方法 Insert:增加 Update:修改 Delete:删除 Disp:显示单笔记录
                if (fam.DB_Option_Action_.Equals("Insert", StringComparison.OrdinalIgnoreCase))
                {
                    cmd.Parameters.Add("@ProjectId", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                }
                else
                {
                    cmd.Parameters.Add("@ProjectId", SqlDbType.UniqueIdentifier).Value = new Guid(fam.ProjectId);
                }
                cmd.Parameters.Add("@ProjectNo", SqlDbType.NVarChar).Value = fam.ProjectNo;
                cmd.Parameters.Add("@ProjectName", SqlDbType.NVarChar).Value = fam.ProjectName;
                cmd.Parameters.Add("@ProjectStatus", SqlDbType.Int).Value = fam.ProjectStatus;
                cmd.Parameters.Add("@ProjectStartTime", SqlDbType.DateTime).Value = fam.ProjectStartTime;
                cmd.Parameters.Add("@ProjectDuration", SqlDbType.Int).Value = fam.ProjectDuration;
                cmd.Parameters.Add("@ProjectBrief", SqlDbType.NVarChar).Value = fam.ProjectBrief;
                cmd.Parameters.Add("@ProjectComments", SqlDbType.NVarChar).Value = fam.ProjectComments;
                cmd.Parameters.Add("@ProjectIcon", SqlDbType.NVarChar).Value = fam.ProjectIcon;
                Conn.Open();
                rInt = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                Conn.Dispose();
                Conn.Close();
            }
            return rInt;
        }
        #endregion
    }
}
