/************************************************************************************
 *      Copyright (C) 2008 supesoft.com,All Rights Reserved						    *
 *      File:																		*
 *				BusinessFacade.cs	                                    			*
 *      Description:																*
 *				 业务逻辑类   													    *
 *      Author:																		*
 *				Lzppcc														        *
 *				Lzppcc@hotmail.com													*
 *				http://www.supesoft.com												*
 *      Finish DateTime:															*
 *				2007年8月6日														*
 *      History:																	*
 ***********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using PmSys.Components;
using PmSys.Data;

namespace PmSys
{
    /// <summary>
    /// 业务逻辑类
    /// </summary>
    public class BusinessFacade
    {

        #region "sys_Applications - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Applications
        /// </summary>
        /// <param name="fam">sys_ApplicationsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_ApplicationsInsertUpdate(sys_ApplicationsTable fam)
        {
            return DataProvider.Instance().sys_ApplicationsInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_ApplicationsTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ApplicationsTable实体类的ArrayList对象</returns>
        public static ArrayList sys_ApplicationsList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Applications";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ApplicationID";
            }
            return DataProvider.Instance().sys_ApplicationsList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_ApplicationsTable实体类 单笔资料
        /// </summary>
        /// <param name="ApplicationID">自动ID 1:为系统管理应用</param>
        /// <returns>返回sys_ApplicationsTable实体类 ApplicationID为0则无记录</returns>
        public static sys_ApplicationsTable sys_ApplicationsDisp(int ApplicationID)
        {
            sys_ApplicationsTable fam = new sys_ApplicationsTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Applications.ApplicationID = " + ApplicationID;
            int RecordCount = 0;
            ArrayList lst = sys_ApplicationsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_ApplicationsTable)lst[0];
            }
            return fam;
        }
        #endregion

        #region "sys_Event - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Event
        /// </summary>
        /// <param name="fam">sys_EventTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_EventInsertUpdate(sys_EventTable fam)
        {
            return DataProvider.Instance().sys_EventInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_EventTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_EventTable实体类的ArrayList对象</returns>
        public static ArrayList sys_EventList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Event";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "EventID";
            }
            else if (qp.Orderfld != "EventID")
            {
                qp.Orderfld += ",EventID";
            }
            return DataProvider.Instance().sys_EventList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_EventTable实体类 单笔资料
        /// </summary>
        /// <param name="EventID">事件ID号</param>
        /// <returns>返回sys_EventTable实体类 EventID为0则无记录</returns>
        public static sys_EventTable sys_EventDisp(int EventID)
        {
            sys_EventTable fam = new sys_EventTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Event.EventID = " + EventID;
            int RecordCount = 0;
            ArrayList lst = sys_EventList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_EventTable)lst[0];
            }
            return fam;
        }
        #endregion

        #region "sys_Field - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Field
        /// </summary>
        /// <param name="fam">sys_FieldTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_FieldInsertUpdate(sys_FieldTable fam)
        {
            return DataProvider.Instance().sys_FieldInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_FieldTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldTable实体类的ArrayList对象</returns>
        public static ArrayList sys_FieldList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Field";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "FieldID";
            }
            return DataProvider.Instance().sys_FieldList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_FieldTable实体类 单笔资料
        /// </summary>
        /// <param name="FieldID">应用字段ID号</param>
        /// <returns>返回sys_FieldTable实体类 FieldID为0则无记录</returns>
        public static sys_FieldTable sys_FieldDisp(int FieldID)
        {
            sys_FieldTable fam = new sys_FieldTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Field.FieldID = " + FieldID;
            int RecordCount = 0;
            ArrayList lst = sys_FieldList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_FieldTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 检测是否违反sys_Field表的PK值
        /// </summary>
        /// <param name="fam">sys_FieldTable类</param>
        /// <param name="pt">PopedomType类型，只对New,Edit有效</param>
        /// <returns></returns>
        public static bool sys_FieldCheckPK(sys_FieldTable fam, PopedomType pt)
        {
            fam.F_Key = Common.inSQL(fam.F_Key);
            QueryParam qp = new QueryParam();
            if (pt == PopedomType.New)
                qp.Where = string.Format(" Where F_Key='{0}'", fam.F_Key);
            else if (pt == PopedomType.Edit)
                qp.Where = string.Format(" Where F_Key='{0}' and FieldID<>{1} ", fam.F_Key, fam.FieldID);

            int RecordCount = 0;
            sys_FieldList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region "sys_FieldValue - Method"
        /// <summary>
        /// 新增/删除/修改 sys_FieldValue
        /// </summary>
        /// <param name="fam">sys_FieldValueTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_FieldValueInsertUpdate(sys_FieldValueTable fam)
        {
            return DataProvider.Instance().sys_FieldValueInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_FieldValueTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_FieldValueTable实体类的ArrayList对象</returns>
        public static ArrayList sys_FieldValueList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_FieldValue";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ValueID";
            }
            return DataProvider.Instance().sys_FieldValueList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_FieldValueTable实体类 单笔资料
        /// </summary>
        /// <param name="ValueID">索引ID号</param>
        /// <returns>返回sys_FieldValueTable实体类 ValueID为0则无记录</returns>
        public static sys_FieldValueTable sys_FieldValueDisp(int ValueID)
        {
            sys_FieldValueTable fam = new sys_FieldValueTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_FieldValue.ValueID = " + ValueID;
            int RecordCount = 0;
            ArrayList lst = sys_FieldValueList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_FieldValueTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据Key,获取相关应用字段列表
        /// </summary>
        /// <param name="Key">应用字段key</param>
        /// <returns>列表</returns>
        public static ArrayList sys_FieldValueFromKey(string Key)
        {
            QueryParam qp = new QueryParam();
            qp.Where = " Where V_F_Key='" + Common.inSQL(Key) + "' ";
            qp.Orderfld = " V_ShowOrder ";
            int rInt = 0;
            return sys_FieldValueList(qp, out rInt);
        }

        /// <summary>
        /// 根据key和code获得应用字段值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="code">code</param>
        /// <returns>sys_FieldValueTable实体类</returns>
        public static sys_FieldValueTable sys_FieldValueFromKey(string key, string code)
        {
            sys_FieldValueTable fam = new sys_FieldValueTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format(" Where sys_FieldValue.V_F_Key = '{0}' and sys_FieldValue.V_Code='{1}' ", Common.inSQL(key), Common.inSQL(code));
            int RecordCount = 0;
            ArrayList lst = sys_FieldValueList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_FieldValueTable)lst[0];
            }
            return fam;
        }

        #endregion

        #region "sys_Group - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Group
        /// </summary>
        /// <param name="fam">sys_GroupTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_GroupInsertUpdate(sys_GroupTable fam)
        {
            return DataProvider.Instance().sys_GroupInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_GroupTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_GroupTable实体类的ArrayList对象</returns>
        public static ArrayList sys_GroupList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Group";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "GroupID";
            }
            return DataProvider.Instance().sys_GroupList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_GroupTable实体类 单笔资料
        /// </summary>
        /// <param name="GroupID">分类ID号</param>
        /// <returns>返回sys_GroupTable实体类 GroupID为0则无记录</returns>
        public static sys_GroupTable sys_GroupDisp(int GroupID)
        {
            sys_GroupTable fam = new sys_GroupTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Group.GroupID = " + GroupID;
            int RecordCount = 0;
            ArrayList lst = sys_GroupList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_GroupTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 获取部门标题路径
        /// </summary>
        /// <param name="GroupID">部门ID</param>
        /// <returns></returns>
        public static string GetGroupTitle(int GroupID)
        {
            int TotalRecord = 0;
            QueryParam qp = new QueryParam();
            qp.Where = " where GroupID =  " + GroupID;
            qp.PageIndex = 1;
            qp.PageSize = 1;
            ArrayList lst = sys_GroupList(qp, out TotalRecord);
            if (TotalRecord == 1)
            {
                foreach (sys_GroupTable x in lst)
                {
                    return GetGroupTitle(x.G_ParentID) + ">" + string.Format("<a href='GroupList.aspx?GroupID={0}'>{1}</a>", x.GroupID, x.G_CName);
                }
            }
            return "";
        }

        /// <summary>
        /// 获取当前分类及子分类列表以,号分开
        /// </summary>
        /// <param name="GroupID">部门ID</param>
        /// <returns></returns>
        public static string GetGroupCatID(int GroupID)
        {
            string All_ID = GroupID.ToString();
            int TotalRecord = 0;
            QueryParam qp = new QueryParam();
            qp.Where = " where G_ParentID =  " + GroupID;
            ArrayList lst = sys_GroupList(qp, out TotalRecord);
            if (TotalRecord > 0)
            {

                foreach (sys_GroupTable x in lst)
                {
                    All_ID = string.Format("{0},{1}", All_ID, GetGroupCatID(x.GroupID));
                    //All_ID + "," + GetGroupCatID(x.GroupID);

                }
            }
            return All_ID;
        }

        /// <summary>
        /// 排序分类ID子分类
        /// </summary>
        /// <param name="GroupID">分类ID</param>
        public static void sys_Group_By(int GroupID)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Orderfld = "G_Level,G_ShowOrder";
            qp.OrderType = 0;
            qp.Where = string.Format("Where G_ParentID ={0} and G_Delete=0 ", GroupID);
            ArrayList lst = sys_GroupList(qp, out RecordCount);
            RecordCount = 1;
            foreach (sys_GroupTable var in lst)
            {
                Update_Table_Fileds("sys_Group", string.Format("G_ShowOrder={0}", RecordCount), string.Format("GroupID={0}", var.GroupID));
                RecordCount++;
            }
        }
        #endregion

        #region "sys_Module - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Module
        /// </summary>
        /// <param name="fam">sys_ModuleTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_ModuleInsertUpdate(sys_ModuleTable fam)
        {
            return DataProvider.Instance().sys_ModuleInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_ModuleTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleTable实体类的ArrayList对象</returns>
        public static ArrayList sys_ModuleList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Module";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ModuleID";
            }
            return DataProvider.Instance().sys_ModuleList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_ModuleTable实体类 单笔资料
        /// </summary>
        /// <param name="ModuleID">功能模块ID号</param>
        /// <returns>返回sys_ModuleTable实体类 ModuleID为0则无记录</returns>
        public static sys_ModuleTable sys_ModuleDisp(int ModuleID)
        {
            sys_ModuleTable fam = new sys_ModuleTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Module.ModuleID = " + ModuleID;
            int RecordCount = 0;
            ArrayList lst = sys_ModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_ModuleTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据M_ApplicationID和M_PageCode返回 sys_ModuleTable实体类 单笔资料
        /// </summary>
        /// <param name="M_ApplicationID">功能模块ID号</param>
        /// <param name="M_PageCode">M_PageCode</param>
        /// <returns>返回sys_ModuleTable实体类 ModuleID为0则无记录</returns>
        public static sys_ModuleTable sys_ModuleDisp(int M_ApplicationID, string M_PageCode)
        {
            sys_ModuleTable fam = new sys_ModuleTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format(" Where sys_Module.M_ApplicationID = {0} and M_PageCode = '{1}'", M_ApplicationID, Common.inSQL(M_PageCode));
            int RecordCount = 0;
            ArrayList lst = sys_ModuleList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_ModuleTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 获取模块标题路径
        /// </summary>
        /// <param name="ModuleID">模块ID</param>
        /// <returns></returns>
        public static string GetModuleTitle(int ModuleID)
        {
            int TotalRecord = 0;
            QueryParam qp = new QueryParam();
            qp.Where = " where ModuleID =  " + ModuleID;
            qp.PageIndex = 1;
            qp.PageSize = 1;
            ArrayList lst = sys_ModuleList(qp, out TotalRecord);
            if (TotalRecord == 1)
            {
                foreach (sys_ModuleTable x in lst)
                {

                    return GetModuleTitle(x.M_ParentID) + ">" + string.Format("<a href='modulemanager.aspx?S_ID={0}&ModuleID={1}'>{2}</a>", x.M_ApplicationID, x.ModuleID, x.M_CName);
                    //Title = "<a href=\"/Home.aspx?ProgCode=F06M02&C_ID=" + x.C_ID.ToString() + "\">" + x.C_Name + "</a> &raquo; " + Title;
                    //load_M_ProductCatID(x.C_Parent.ToString());
                }
            }
            return "";
        }

        /// <summary>
        /// 检测是否违反sys_Module表的PK值
        /// </summary>
        /// <param name="M_ApplicationID">应用ID</param>
        /// <param name="M_PageCode">PageCode</param>
        /// <param name="Not_IS_ModuleID">ModuleID不等于此ID,当为0时不进行检测</param>
        /// <returns>true存在,false否存在</returns>
        public static bool sys_Module_CheckPK(int M_ApplicationID, string M_PageCode, int Not_IS_ModuleID)
        {
            M_PageCode = Common.inSQL(M_PageCode);
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = Not_IS_ModuleID == 0 ? string.Format("Where M_ApplicationID={0} and M_PageCode='{1}'", M_ApplicationID, M_PageCode) : string.Format("Where M_ApplicationID={0} and M_PageCode='{1}' and ModuleID<>{2}", M_ApplicationID, M_PageCode, Not_IS_ModuleID);
            sys_ModuleList(qp, out RecordCount);
            if (RecordCount > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 状态判断
        /// </summary>
        /// <param name="ID">状态ID</param>
        /// <returns>否,是</returns>
        public static string GetStatus(int ID)
        {
            if (ID == 0)
                return "否";
            else
                return "是";
        }

        /// <summary>
        /// 根据应用ID获取所有模板列表
        /// </summary>
        /// <param name="applicationid">应用ID</param>
        /// <returns>模块实体类</returns>
        public static ArrayList sys_Module_GetModules(int applicationid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where M_ApplicationID={0} ", applicationid);
            int rInt = 0;
            return sys_ModuleList(qp, out rInt);
        }

        /// <summary>
        /// 根据应用ID删除所有模块
        /// </summary>
        /// <param name="applicationid"></param>
        public static void sys_Module_DeleteFormAppID(int applicationid)
        {
            ArrayList lst = sys_Module_GetModules(applicationid);
            foreach (sys_ModuleTable var in lst)
            {
                var.DB_Option_Action_ = "Delete";
                sys_ModuleInsertUpdate(var);

            }
        }


        #endregion

        #region "sys_RolePermission - Method"
        /// <summary>
        /// 新增/删除/修改 sys_RolePermission
        /// </summary>
        /// <param name="fam">sys_RolePermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_RolePermissionInsertUpdate(sys_RolePermissionTable fam)
        {
            return DataProvider.Instance().sys_RolePermissionInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_RolePermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolePermissionTable实体类的ArrayList对象</returns>
        public static ArrayList sys_RolePermissionList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_RolePermission";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "PermissionID";
            }
            return DataProvider.Instance().sys_RolePermissionList(qp, out RecordCount);
        }

        /// <summary>
        /// 根据ID返回 sys_RolePermissionTable实体类 单笔资料
        /// </summary>
        /// <param name="PermissionID">角色应用权限自动ID</param>
        /// <returns>返回sys_RolePermissionTable实体类 PermissionID为0则无记录</returns>
        public static sys_RolePermissionTable sys_RolePermissionDisp(int PermissionID)
        {
            sys_RolePermissionTable fam = new sys_RolePermissionTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_RolePermission.PermissionID = " + PermissionID;
            int RecordCount = 0;
            ArrayList lst = sys_RolePermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_RolePermissionTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 删除根据角色ID, 应用ID,角色权限记录
        /// </summary>
        /// <param name="RoleID">角色Id</param>
        /// <param name="ApplicationID">应用ID</param>
        public static void sys_RolePermission_Move(int RoleID, int ApplicationID)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where P_RoleID={0} and P_ApplicationID = {1}", RoleID, ApplicationID);
            ArrayList lst = sys_RolePermissionList(qp, out RecordCount);
            foreach (sys_RolePermissionTable var in lst)
            {
                var.DB_Option_Action_ = "Delete";
                sys_RolePermissionInsertUpdate(var);
            }
        }

        /// <summary>
        /// 删除根据角色ID,角色权限记录
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        public static void sys_RolePermission_Move(int RoleID)
        {
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where P_RoleID={0}", RoleID);
            ArrayList lst = sys_RolePermissionList(qp, out RecordCount);
            foreach (sys_RolePermissionTable var in lst)
            {
                var.DB_Option_Action_ = "Delete";
                sys_RolePermissionInsertUpdate(var);
            }
        }

        /// <summary>
        /// 获取角色应用权限资料
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="ApplicationID">应用ID</param>
        /// <param name="PageCode">PageCode</param>
        /// <returns></returns>
        public static sys_RolePermissionTable sys_RolePermissionDisp(int RoleID, int ApplicationID, string PageCode)
        {
            sys_RolePermissionTable s_Rp = new sys_RolePermissionTable();
            PageCode = Common.inSQL(PageCode);
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where P_RoleID= {0} and P_ApplicationID={1} and P_PageCode='{2}'", RoleID, ApplicationID, PageCode);
            int RecordCount = 0;
            ArrayList lst = sys_RolePermissionList(qp, out RecordCount);
            if (lst.Count > 0)
            {
                s_Rp = (sys_RolePermissionTable)lst[0];
            }
            return s_Rp;
        }

        /// <summary>
        /// 根据应用ID,获得角色权限列表
        /// </summary>
        /// <param name="applicationid">应用ID</param>
        /// <returns>角色权限实体类</returns>
        public static ArrayList sys_RolePermission_GetList(int applicationid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where P_ApplicationID={0} ", applicationid);
            int rInt = 0;
            return sys_RolePermissionList(qp, out rInt);
        }

        /// <summary>
        /// 根据应用ID,删除角色权限记录
        /// </summary>
        /// <param name="applicationid"></param>
        public static void sys_RolePermission_DeleteFromAppid(int applicationid)
        {
            foreach (sys_RolePermissionTable var in sys_RolePermission_GetList(applicationid))
            {
                var.DB_Option_Action_ = "Delete";
                sys_RolePermissionInsertUpdate(var);
            }
        }

        /// <summary>
        /// 根据应用Id,pagecode,获得角色权限列表
        /// </summary>
        /// <param name="applicationid">应用id</param>
        /// <param name="pagecode">pagecode</param>
        /// <returns>角色权限实体类</returns>
        public static ArrayList sys_RolePermission_GetList(int applicationid, string pagecode)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where P_ApplicationID={0} and P_PageCode='{1}' ", applicationid, Common.inSQL(pagecode));
            int rInt = 0;
            return sys_RolePermissionList(qp, out rInt);
        }

        /// <summary>
        /// 根据应用Id,角色ID,获得权限列表
        /// </summary>
        /// <param name="applicationid">应用id</param>
        /// <param name="roleid">角色ID</param>
        /// <returns>角色权限实体类</returns>
        public static ArrayList sys_RolePermission_GetList(int applicationid, int roleid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where P_ApplicationID={0} and P_RoleID='{1}' ", applicationid, roleid);
            int rInt = 0;
            return sys_RolePermissionList(qp, out rInt);
        }

        /// <summary>
        /// 根据应用Id和pagecode删除角色权限
        /// </summary>
        /// <param name="applicationid">应用id</param>
        /// <param name="pagecode">pagecode</param>
        public static void sys_RolePermission_DeleteForm(int applicationid, string pagecode)
        {
            foreach (sys_RolePermissionTable var in sys_RolePermission_GetList(applicationid, pagecode))
            {
                var.DB_Option_Action_ = "Delete";
                sys_RolePermissionInsertUpdate(var);
                UserData.Move_RoleUserPermissionCache(var.P_RoleID);
            }
        }

        #endregion

        #region "sys_Roles - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Roles
        /// </summary>
        /// <param name="fam">sys_RolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_RolesInsertUpdate(sys_RolesTable fam)
        {
            return DataProvider.Instance().sys_RolesInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_RolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RolesTable实体类的ArrayList对象</returns>
        public static ArrayList sys_RolesList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Roles";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "RoleID";
            }
            return DataProvider.Instance().sys_RolesList(qp, out RecordCount);
        }

        /// <summary>
        /// 获得当前登陆用户创建角色列表
        /// </summary>
        /// <param name="qp">查询</param>
        /// <param name="RecordCount">总记录</param>
        /// <returns>角色实体类</returns>
        public static ArrayList sys_RolesListUser(QueryParam qp, out int RecordCount)
        {
            if (UserData.GetUserDate.U_Type != 0)
            {
                qp.Where = string.Format("Where R_UserID ={0}", UserData.GetUserDate.UserID);
            }
            qp.TableName = "sys_Roles";
            qp.ReturnFields = "*";

            if (qp.Orderfld == null)
            {
                qp.Orderfld = "RoleID";
            }
            return DataProvider.Instance().sys_RolesList(qp, out RecordCount);
        }

        /// <summary>
        /// 根据ID返回 sys_RolesTable实体类 单笔资料
        /// </summary>
        /// <param name="RoleID">角色ID自动ID</param>
        /// <returns>返回sys_RolesTable实体类 RoleID为0则无记录</returns>
        public static sys_RolesTable sys_RolesDisp(int RoleID)
        {
            sys_RolesTable fam = new sys_RolesTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Roles.RoleID = " + RoleID;
            int RecordCount = 0;
            ArrayList lst = sys_RolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_RolesTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 检测是否存在相同角色名
        /// </summary>
        /// <param name="R_RoleName">角色名</param>
        /// <param name="Not_IS_RoleID">RoleID不等于此ID,当为0时不进行检测</param>
        /// <returns>存在true,不存在false</returns>
        public static bool sys_Roles_PK(string R_RoleName, int Not_IS_RoleID)
        {
            R_RoleName = Common.inSQL(R_RoleName);
            int RecordCount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = Not_IS_RoleID == 0 ? string.Format("Where R_RoleName='{0}'", R_RoleName) : string.Format("Where R_RoleName='{0}' and RoleID<>{1}", R_RoleName, Not_IS_RoleID);
            sys_RolesList(qp, out RecordCount);
            if (RecordCount > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检测当前用户是否可以管理当前角色ID
        /// </summary>
        /// <param name="rolesid">角色id</param>
        public static bool sys_Roles_CheckUser(int rolesid)
        {
            if (UserData.GetUserDate.U_Type == 0)
                return true;
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = string.Format(" Where RoleID ={0} and R_UserID={1}", rolesid,UserData.GetUserDate.UserID);
            int RecordCount = 0;
            sys_RolesList(qp, out RecordCount);
            if (RecordCount > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检测当前用户是否可以管理当前角色ID(直接提示)
        /// </summary>
        /// <param name="rolesid">角色id</param>
        public static void sys_Roles_CheckUserVoid(int rolesid)
        { 
            if (!sys_Roles_CheckUser(rolesid))
                EventMessage.MessageBox(2, "禁止访问", string.Format("你无权操作当前角色ID:{0}!",rolesid), Icon_Type.Error, "history.back();", UrlType.JavaScript);
        }

        /// <summary>
        /// 获得当前用户创建角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>角色id</returns>
        public static ArrayList sys_RolesList(int userid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where R_UserID ={0}", userid);
            qp.TableName = "sys_Roles";
            qp.ReturnFields = "*";

            if (qp.Orderfld == null)
            {
                qp.Orderfld = "RoleID";
            }
            int RecordCount = 0;
            return DataProvider.Instance().sys_RolesList(qp, out RecordCount);
        }


        #endregion

        #region "sys_User - Method"
        /// <summary>
        /// 新增/删除/修改 sys_User
        /// </summary>
        /// <param name="fam">sys_UserTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_UserInsertUpdate(sys_UserTable fam)
        {
            return DataProvider.Instance().sys_UserInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_UserTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserTable实体类的ArrayList对象</returns>
        public static ArrayList sys_UserList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_User";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "UserID";
            }
            else if (qp.Orderfld != "UserID")
            {
                qp.Orderfld += ",UserID";
            }
            return DataProvider.Instance().sys_UserList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_UserTable实体类 单笔资料
        /// </summary>
        /// <param name="UserID">用户ID号</param>
        /// <returns>返回sys_UserTable实体类 UserID为0则无记录</returns>
        public static sys_UserTable sys_UserDisp(int UserID)
        {
            sys_UserTable fam = new sys_UserTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_User.UserID = " + UserID;
            int RecordCount = 0;
            ArrayList lst = sys_UserList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_UserTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 检测是否违反sys_User表的PK值
        /// </summary>
        /// <param name="fam">sys_UserTable类</param>
        /// <param name="pt">PopedomType类型，只对New,Edit有效</param>
        /// <returns></returns>
        public static bool sys_UserTableCheckPK(sys_UserTable fam, PopedomType pt)
        {
            QueryParam qp = new QueryParam();
            if (pt == PopedomType.New)
                qp.Where = string.Format(" Where U_LoginName='{0}'", fam.U_LoginName);
            else if (pt == PopedomType.Edit)
                qp.Where = string.Format(" Where U_LoginName='{0}' and UserID<>{1} ", fam.U_LoginName, fam.UserID);

            int RecordCount = 0;
            sys_UserList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据部门ID,获得用户列表
        /// </summary>
        /// <param name="groupid">部门id</param>
        /// <returns>用户实体类列表</returns>
        public static ArrayList sys_UserList(int groupid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where U_GroupID={0} ", groupid);
            int rInt = 0;
            return sys_UserList(qp, out rInt);
        }

        /// <summary>
        /// 获得用户类型字符
        /// </summary>
        /// <param name="typeid">类型值</param>
        /// <returns>类型字符</returns>
        public static string sys_UserType(int typeid)
        {
            if (typeid == 0)
                return "超级用户";
            else if (typeid == 1)
                return "普通用户";
            else if (typeid == 2)
            {
                return "管理员";
            }
            else
                return "未知用户类型";
        }

        /// <summary>
        /// 检测用户是否是管理员
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>是/否</returns>
        public static bool sys_UserCheckManager(int userid)
        {
            sys_UserTable ut = UserData.Get_sys_UserTable(userid);
            if (ut.UserID != 0)
            {
                if (ut.U_Type == 2)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 检测当前用户是否为管理员/超级用户(直接提示)
        /// </summary>
        public static void sys_UserCheckManagerVoid()
        {
            if (!sys_UserCheckManager())
            {
                EventMessage.MessageBox(1, "禁止访问", "你不是管理员/超级用户,无权限执行当前操作!", Icon_Type.Error, "history.back();", UrlType.JavaScript);
            }
        }

        /// <summary>
        /// 检测当前用户是否为管理员/超级用户
        /// </summary>
        /// <returns>是/否</returns>
        public static bool sys_UserCheckManager()
        {
            if (UserData.GetUserDate.U_Type != 2 && UserData.GetUserDate.U_Type != 0)
                return false;
            else
                return true;
        }

        #endregion

        #region "sys_RoleApplication - Method"
        /// <summary>
        /// 新增/删除/修改 sys_RoleApplication
        /// </summary>
        /// <param name="fam">sys_RoleApplicationTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_RoleApplicationInsertUpdate(sys_RoleApplicationTable fam)
        {
            return DataProvider.Instance().sys_RoleApplicationInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_RoleApplicationTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_RoleApplicationTable实体类的ArrayList对象</returns>
        public static ArrayList sys_RoleApplicationList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_RoleApplication";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "A_RoleID,A_ApplicationID";
            }
            return DataProvider.Instance().sys_RoleApplicationList(qp, out RecordCount);
        }


        /// <summary>
        /// 根据ID返回 sys_RoleApplicationTable实体类 单笔资料
        /// </summary>
        /// <param name="A_RoleID"></param>
        /// <param name="A_ApplicationID"></param>
        /// <returns>返回sys_RoleApplicationTable实体类 为0则无记录</returns>

        public static sys_RoleApplicationTable sys_RoleApplicationDisp(int A_RoleID, int A_ApplicationID)
        {
            sys_RoleApplicationTable fam = new sys_RoleApplicationTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_RoleApplication.A_RoleID = " + A_RoleID.ToString() + " sys_RoleApplication.A_ApplicationID=" + A_ApplicationID.ToString();
            int RecordCount = 0;
            ArrayList lst = sys_RoleApplicationList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_RoleApplicationTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据角色ID,删除角色应用表
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        public static void sys_RoleApplication_Move(int RoleID)
        {
            int Recordcount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where A_RoleID = {0}", RoleID);
            qp.OrderType = 0;
            ArrayList lst = BusinessFacade.sys_RoleApplicationList(qp, out Recordcount);
            foreach (sys_RoleApplicationTable var in lst)
            {
                var.DB_Option_Action_ = "Delete";
                sys_RoleApplicationInsertUpdate(var);
            }
        }

        /// <summary>
        /// 根据应用ID,获得角色应用列表
        /// </summary>
        /// <param name="applicationid">应用ID</param>
        /// <returns>角色应用实体类</returns>
        public static ArrayList sys_RoleApplication_GetList(int applicationid)
        {
            QueryParam qp = new QueryParam();
            qp.Where = string.Format(" Where A_ApplicationID={0} ", applicationid);
            int rInt = 0;
            return sys_RoleApplicationList(qp, out rInt);
        }

        /// <summary>
        /// 根据应用ID删除角色应用表
        /// </summary>
        /// <param name="applicationid">应用ID</param>
        public static void sys_RoleApplication_DeleteFormAppid(int applicationid)
        {
            foreach (sys_RoleApplicationTable var in sys_RoleApplication_GetList(applicationid))
            {
                //移除用户角色缓存
                UserData.Move_RoleUserPermissionCache(var.A_RoleID);
                var.DB_Option_Action_ = "Delete";
                sys_RoleApplicationInsertUpdate(var);
            }
        }
        #endregion

        #region "sys_UserRoles - Method"
        /// <summary>
        /// 新增/删除/修改 sys_UserRoles
        /// </summary>
        /// <param name="fam">sys_UserRolesTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_UserRolesInsertUpdate(sys_UserRolesTable fam)
        {
            return DataProvider.Instance().sys_UserRolesInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_UserRolesTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_UserRolesTable实体类的ArrayList对象</returns>
        public static ArrayList sys_UserRolesList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_UserRoles";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "R_UserID,R_RoleID";
            }
            return DataProvider.Instance().sys_UserRolesList(qp, out RecordCount);
        }


        /// <summary>
        /// 根据ID返回 sys_UserRolesTable实体类 单笔资料
        /// </summary>
        /// <param name="R_UserID"></param>
        /// <param name="R_RoleID"></param>
        /// <returns>返回sys_UserRolesTable实体类 为0则无记录</returns>
        public static sys_UserRolesTable sys_UserRolesDisp(int R_UserID, int R_RoleID)
        {
            sys_UserRolesTable fam = new sys_UserRolesTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_UserRoles.R_UserID = " + R_UserID.ToString() + " and sys_UserRoles.R_RoleID=" + R_RoleID.ToString();
            int RecordCount = 0;
            ArrayList lst = sys_UserRolesList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_UserRolesTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据ID返回 sys_UserRolesTable实体类 集合
        /// </summary>
        /// <param name="R_UserID"></param>
        /// <returns>返回sys_UserRolesTable实体类 为0则无记录</returns>
        public static ArrayList sys_UserRolesDisp(int R_UserID)
        {
            sys_UserRolesTable fam = new sys_UserRolesTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.Where = " Where sys_UserRoles.R_UserID = " + R_UserID.ToString();
            int RecordCount = 0;
            return sys_UserRolesList(qp, out RecordCount);
        }

        /// <summary>
        /// 根据角色ID,返回 sys_UserRolesTable实体类 集合
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns>返回sys_UserRolesTable实体类 为0则无记录</returns>
        public static ArrayList sys_UserRolesList(int roleid)
        {
            sys_UserRolesTable fam = new sys_UserRolesTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.Where = " Where sys_UserRoles.R_RoleID = " + roleid.ToString();
            int RecordCount = 0;
            return sys_UserRolesList(qp, out RecordCount);
        }
        #endregion

        #region "sys_SystemInfo - Method"
        /// <summary>
        /// 新增/删除/修改 sys_SystemInfo
        /// </summary>
        /// <param name="fam">sys_SystemInfoTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_SystemInfoInsertUpdate(sys_SystemInfoTable fam)
        {
            return DataProvider.Instance().sys_SystemInfoInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_SystemInfoTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_SystemInfoTable实体类的ArrayList对象</returns>
        public static ArrayList sys_SystemInfoList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_SystemInfo";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "SystemID";
            }
            return DataProvider.Instance().sys_SystemInfoList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_SystemInfoTable实体类 单笔资料
        /// </summary>
        /// <param name="SystemID">自动ID</param>
        /// <returns>返回sys_SystemInfoTable实体类 SystemID为0则无记录</returns>
        public static sys_SystemInfoTable sys_SystemInfoDisp(int SystemID)
        {
            sys_SystemInfoTable fam = new sys_SystemInfoTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_SystemInfo.SystemID = " + SystemID;
            int RecordCount = 0;
            ArrayList lst = sys_SystemInfoList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_SystemInfoTable)lst[0];
            }
            return fam;
        }
        #endregion

        #region "sys_Online - Method"
        /// <summary>
        /// 新增/删除/修改 sys_Online
        /// </summary>
        /// <param name="fam">sys_OnlineTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_OnlineInsertUpdate(sys_OnlineTable fam)
        {
            return DataProvider.Instance().sys_OnlineInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_OnlineTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_OnlineTable实体类的ArrayList对象</returns>
        public static ArrayList sys_OnlineList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_Online";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "OnlineID";
            }
            return DataProvider.Instance().sys_OnlineList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_OnlineTable实体类 单笔资料
        /// </summary>
        /// <param name="OnlineID">自动ID</param>
        /// <returns>返回sys_OnlineTable实体类 OnlineID为0则无记录</returns>
        public static sys_OnlineTable sys_OnlineDisp(int OnlineID)
        {
            sys_OnlineTable fam = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Online.OnlineID = " + OnlineID;
            int RecordCount = 0;
            ArrayList lst = sys_OnlineList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_OnlineTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据UserID返回 sys_OnlineTable实体类 单笔资料
        /// </summary>
        /// <param name="sessionid">用户sessionID</param>
        /// <returns>返回sys_OnlineTable实体类 OnlineID为0则无记录</returns>
        public static sys_OnlineTable sys_OnlineDispSessionID(string sessionid)
        {
            sys_OnlineTable fam = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_Online.O_SessionID = '" + Common.inSQL(sessionid) + "'";
            int RecordCount = 0;
            ArrayList lst = sys_OnlineList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_OnlineTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据用户名读取在线用户
        /// </summary>
        /// <param name="O_UserName">用户名</param>
        /// <returns>返回sys_OnlineTable实例</returns>
        public static sys_OnlineTable sys_OnlineDisp(string O_UserName)
        {
            sys_OnlineTable Online = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where O_UserName='{0}'", Common.inSQL(O_UserName));
            qp.PageIndex = 1;
            qp.PageSize = 1;
            int rInt = 0;
            ArrayList lst = sys_OnlineList(qp, out rInt);
            if (rInt > 0)
            {
                Online = (sys_OnlineTable)lst[0];
            }
            return Online;
        }


        /// <summary>
        /// 根据用户名读取在线用户
        /// </summary>
        /// <param name="O_UserName">用户名</param>
        /// <param name="sessionid">用户SessionID</param>
        /// <returns>返回sys_OnlineTable实例</returns>
        public static sys_OnlineTable sys_OnlineDisp(string O_UserName,string sessionid)
        {
            sys_OnlineTable Online = new sys_OnlineTable();
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where O_SessionID='{0}' and O_UserName='{1}'",sessionid, Common.inSQL(O_UserName));
            qp.PageIndex = 1;
            qp.PageSize = 1;
            int rInt = 0;
            ArrayList lst = sys_OnlineList(qp, out rInt);
            if (rInt == 1)
            {
                Online = (sys_OnlineTable)lst[0];
            }
            return Online;
        }

        /// <summary>
        /// 检测用户sessionid是否在线
        /// </summary>
        /// <param name="sessionid">用户sessionid</param>
        /// <returns>true/false</returns>
        public static bool CheckSessionIDOnline(string sessionid)
        {
            if (sys_OnlineDispSessionID(sessionid).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检测用户名是否在线
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static bool CheckMemberOnline(string username)
        {
            if (sys_OnlineDisp(username).OnlineID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 访问方法
        /// </summary>
        /// <param name="sessionid">用户SessionID</param>
        public static void AccessMemberOnline(string sessionid)
        {
            sys_OnlineTable online = sys_OnlineDispSessionID(sessionid);
            online.O_LastTime = DateTime.Now;
            online.O_LastUrl = Common.GetScriptUrl;
            online.DB_Option_Action_ = "Update";
            sys_OnlineInsertUpdate(online);
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sessionid">用户SessionID</param>
        public static void RemoveMemberOnline(string sessionid)
        {
            sys_OnlineTable online = sys_OnlineDispSessionID(sessionid);
            online.DB_Option_Action_ = "Delete";
            sys_OnlineInsertUpdate(online);
        }

        /// <summary>
        /// 插入在线用户表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="sessionid">用户sessionID</param>
        public static void InsertMemberOnline(string username, string sessionid)
        {
			sys_OnlineTable online = PutUserOnline(username, sessionid, "Insert");
            sys_OnlineInsertUpdate(online);
        }

		private static sys_OnlineTable PutUserOnline(string username, string sessionid, string action)
		{
			sys_OnlineTable online = new sys_OnlineTable();
			online.DB_Option_Action_ = action;
			online.O_Ip = Common.GetIPAddress();
			online.O_LastTime = DateTime.Now;
			online.O_LastUrl = Common.GetScriptUrl;
			online.O_LoginTime = online.O_LastTime;
			online.O_SessionID = sessionid;
			online.O_UserName = username;
			return online;
		}

		public static void UpdateMemberOnline(string username, string sessionid)
		{
			sys_OnlineTable online = PutUserOnline(username, sessionid, "Update");
			sys_OnlineInsertUpdate(online);
		}


        #endregion

        #region "sys_ModuleExtPermission - Method"
        /// <summary>
        /// 新增/删除/修改 sys_ModuleExtPermission
        /// </summary>
        /// <param name="fam">sys_ModuleExtPermissionTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int sys_ModuleExtPermissionInsertUpdate(sys_ModuleExtPermissionTable fam)
        {
            return DataProvider.Instance().sys_ModuleExtPermissionInsertUpdate(fam);
        }

        /// <summary>
        /// 返回sys_ModuleExtPermissionTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>sys_ModuleExtPermissionTable实体类的ArrayList对象</returns>
        public static ArrayList sys_ModuleExtPermissionList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "sys_ModuleExtPermission";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ExtPermissionID";
            }
            return DataProvider.Instance().sys_ModuleExtPermissionList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 sys_ModuleExtPermissionTable实体类 单笔资料
        /// </summary>
        /// <param name="ExtPermissionID">扩展权限ID</param>
        /// <returns>返回sys_ModuleExtPermissionTable实体类 ExtPermissionID为0则无记录</returns>
        public static sys_ModuleExtPermissionTable sys_ModuleExtPermissionDisp(int ExtPermissionID)
        {
            sys_ModuleExtPermissionTable fam = new sys_ModuleExtPermissionTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where sys_ModuleExtPermission.ExtPermissionID = " + ExtPermissionID;
            int RecordCount = 0;
            ArrayList lst = sys_ModuleExtPermissionList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (sys_ModuleExtPermissionTable)lst[0];
            }
            return fam;
        }

        /// <summary>
        /// 根据模块ID,获得模块新增自定义权限值
        /// </summary>
        /// <param name="moduleid">模块ID</param>
        /// <returns>自定义权限值 0:已经达到最大值,无法再增加!</returns>
        public static int sys_ModuleExtPermissionGetLastPermissionValue(int moduleid)
        {
            int recordcount = 0;
            QueryParam qp = new QueryParam();
            qp.Where = string.Format("Where ModuleID={0}", moduleid);
            qp.Orderfld = "PermissionValue";
            qp.OrderType = 1;
            qp.PageSize = int.MaxValue;
            qp.PageIndex = 1;
            ArrayList lst = sys_ModuleExtPermissionList(qp, out recordcount);
            if (lst.Count >= 21)
                return 0;
            if (lst.Count == 0)
            {
                return 256 * 2;
            }
            else
            {
                return ((sys_ModuleExtPermissionTable)lst[0]).PermissionValue * 2;
            }
        }

        #endregion
        
        #region "根据ModuleID获取当前用户拥有权限的子菜单项"
        /// <summary>
        /// 根据ModuleID获取当前用户拥有权限的子菜单项
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <returns></returns>
        public static ArrayList GetPermissionModuleSub(int ModuleID)
        {
            QueryParam qp = new QueryParam();
            qp.Orderfld = " M_OrderLevel ";
            qp.OrderType = 0;
            qp.Where = string.Format("Where M_Close=0 and M_ParentID ={0}", ModuleID);
            int RecordCount = 0;
            ArrayList lst = sys_ModuleList(qp, out RecordCount);
            Remove_MenuNoPermission(lst);
            return lst;
        }
        #endregion

        #region "移除用户无权限菜单项"
        /// <summary>
        /// 移除用户无权限菜单项
        /// </summary>
        /// <param name="lst"></param>
        public static void Remove_MenuNoPermission(ArrayList lst)
        {
            int iCount = lst.Count;
            for (int i = 0; i <= iCount; i++)
            {
                int iIndex = 0;
                foreach (sys_ModuleTable var in lst)
                {
                    if (!UserData.CheckPageCode(Common.Get_UserID, var.M_ApplicationID, var.M_PageCode))
                    {
                        lst.RemoveAt(iIndex);
                        break;
                    }
                    iIndex++;
                }
            }
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
        public static string get_table_fileds(string table_name, string table_fileds, string where_fileds, string where_value)
        {
            return DataProvider.Instance().get_table_fileds(table_name, table_fileds, where_fileds, where_value);
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
        public static int Update_Table_Fileds(string Table, string Table_FiledsValue, string Wheres)
        {
            return DataProvider.Instance().Update_Table_Fileds(Table, Table_FiledsValue, Wheres);
        }

        #endregion

        #region Project
        /// <summary>
        /// 返回PM_ProjectsTable实体类的ArrayList对象
        /// </summary>
        /// <param name="qp">查询类</param>
        /// <param name="RecordCount">返回记录总数</param>
        /// <returns>PM_ProjectsTable实体类的ArrayList对象</returns>
        public static ArrayList PM_ProjectsList(QueryParam qp, out int RecordCount)
        {
            qp.TableName = "PM_Project";
            qp.ReturnFields = "*";
            if (qp.Orderfld == null)
            {
                qp.Orderfld = "ProjectId";
            }
            return DataProvider.Instance().PM_ProjectsList(qp, out RecordCount);
        }
        /// <summary>
        /// 根据ID返回 PM_ProjectsTable实体类 单笔资料
        /// </summary>
        /// <param name="ProjectId">自动ID </param>
        /// <returns>返回PM_ProjectsTable实体类 </returns>
        public static PM_ProjectsTable PM_ProjectsDisp(string ProjectId)
        {
            PM_ProjectsTable fam = new PM_ProjectsTable();
            QueryParam qp = new QueryParam();
            qp.PageIndex = 1;
            qp.PageSize = 1;
            qp.Where = " Where PM_Project.ProjectId = '" + ProjectId +"'";
            int RecordCount = 0;
            ArrayList lst = PM_ProjectsList(qp, out RecordCount);
            if (RecordCount > 0)
            {
                fam = (PM_ProjectsTable)lst[0];
            }
            return fam;
        }
        /// <summary>
        /// 新增/删除/修改 PM_ProjectsTable
        /// </summary>
        /// <param name="fam">PM_ProjectsTable实体类</param>
        /// <returns>返回0操正常</returns>
        public static int PM_ProjectsTableInsertUpdate(PM_ProjectsTable fam)
        {
            return DataProvider.Instance().PM_ProjectsTableInsertUpdate(fam);
        }
        #endregion
    }
}
