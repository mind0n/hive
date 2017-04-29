/*==============================================================*/
/* Database name:  FrameWork                                    */
/* DBMS name:      SQL SERVER 2000(EXTENDED)                    */
/* Created on:     2009-1-11 20:35:48                           */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Applications')
            and   type = 'U')
   drop table dbo.sys_Applications
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Event')
            and   type = 'U')
   drop table dbo.sys_Event
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Field')
            and   type = 'U')
   drop table dbo.sys_Field
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_FieldValue')
            and   type = 'U')
   drop table dbo.sys_FieldValue
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Group')
            and   type = 'U')
   drop table dbo.sys_Group
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Module')
            and   type = 'U')
   drop table dbo.sys_Module
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_ModuleExtPermission')
            and   type = 'U')
   drop table dbo.sys_ModuleExtPermission
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Online')
            and   type = 'U')
   drop table dbo.sys_Online
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_RoleApplication')
            and   type = 'U')
   drop table dbo.sys_RoleApplication
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_RolePermission')
            and   type = 'U')
   drop table dbo.sys_RolePermission
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_Roles')
            and   type = 'U')
   drop table dbo.sys_Roles
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_SystemInfo')
            and   type = 'U')
   drop table dbo.sys_SystemInfo
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_User')
            and   type = 'U')
   drop table dbo.sys_User
go


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sys_UserRoles')
            and   type = 'U')
   drop table dbo.sys_UserRoles
go


/*==============================================================*/
/* Table: sys_Applications                                      */
/*==============================================================*/
create table dbo.sys_Applications (
   ApplicationID        int                  identity,
   A_AppName            nvarchar(50)         null,
   A_AppDescription     nvarchar(200)        null,
   A_AppUrl             varchar(50)          null,
   constraint PK_SYS_APPLICATIONS primary key clustered (ApplicationID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用表', N'user', N'dbo', N'table', N'sys_Applications', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'自动ID 1:为系统管理应用', N'user', N'dbo', N'table', N'sys_Applications', N'column', N'ApplicationID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用名称', N'user', N'dbo', N'table', N'sys_Applications', N'column', N'A_AppName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用介绍', N'user', N'dbo', N'table', N'sys_Applications', N'column', N'A_AppDescription'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用Url地址', N'user', N'dbo', N'table', N'sys_Applications', N'column', N'A_AppUrl'
go


/*==============================================================*/
/* Table: sys_Event                                             */
/*==============================================================*/
create table dbo.sys_Event (
   EventID              int                  identity,
   E_U_LoginName        nvarchar(20)         null,
   E_UserID             int                  null,
   E_DateTime           datetime             not null default getdate(),
   E_ApplicationID      int                  null,
   E_A_AppName          nvarchar(50)         null,
   E_M_Name             nvarchar(50)         null,
   E_M_PageCode         varchar(6)           null,
   E_From               nvarchar(500)        null,
   E_Type               tinyint              not null default 1,
   E_IP                 varchar(15)          null,
   E_Record             nvarchar(500)        null,
   constraint PK_SYS_EVENT primary key clustered (EventID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'系统日记表', N'user', N'dbo', N'table', N'sys_Event', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'事件ID号', N'user', N'dbo', N'table', N'sys_Event', N'column', N'EventID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户名', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_U_LoginName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'操作时用户ID与sys_Users中UserID', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_UserID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'事件发生的日期及时间', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_DateTime'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'所属应用程序ID与sys_Applicatio', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_ApplicationID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'所属应用名称', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_A_AppName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'PageCode模块名称与sys_Module相同', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_M_Name'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'发生事件时模块名称', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_M_PageCode'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'来源', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_From'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'日记类型,1:操作日记2:安全日志3', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_Type'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'客户端IP地址', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_IP'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'详细描述', N'user', N'dbo', N'table', N'sys_Event', N'column', N'E_Record'
go


/*==============================================================*/
/* Table: sys_Field                                             */
/*==============================================================*/
create table dbo.sys_Field (
   FieldID              int                  identity,
   F_Key                varchar(50)          null,
   F_CName              nvarchar(50)         null,
   F_Remark             nvarchar(200)        null,
   constraint PK_Sys_Field primary key clustered (FieldID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'系统应用字段', N'user', N'dbo', N'table', N'sys_Field', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用字段ID号', N'user', N'dbo', N'table', N'sys_Field', N'column', N'FieldID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用字段关键字', N'user', N'dbo', N'table', N'sys_Field', N'column', N'F_Key'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用字段中文说明', N'user', N'dbo', N'table', N'sys_Field', N'column', N'F_CName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'描述说明', N'user', N'dbo', N'table', N'sys_Field', N'column', N'F_Remark'
go


/*==============================================================*/
/* Table: sys_FieldValue                                        */
/*==============================================================*/
create table dbo.sys_FieldValue (
   ValueID              int                  identity,
   V_F_Key              varchar(50)          null,
   V_Text               nvarchar(100)        null,
   V_Code               varchar(100)         null,
   V_ShowOrder          int                  not null default 0,
   constraint PK_Sys_FieldValue primary key clustered (ValueID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用字段值', N'user', N'dbo', N'table', N'sys_FieldValue', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'索引ID号', N'user', N'dbo', N'table', N'sys_FieldValue', N'column', N'ValueID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'与sys_Field表中F_Key字段关联', N'user', N'dbo', N'table', N'sys_FieldValue', N'column', N'V_F_Key'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'中文说明', N'user', N'dbo', N'table', N'sys_FieldValue', N'column', N'V_Text'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'编码', N'user', N'dbo', N'table', N'sys_FieldValue', N'column', N'V_Code'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'同级显示顺序', N'user', N'dbo', N'table', N'sys_FieldValue', N'column', N'V_ShowOrder'
go


/*==============================================================*/
/* Table: sys_Group                                             */
/*==============================================================*/
create table dbo.sys_Group (
   GroupID              int                  identity,
   G_CName              nvarchar(50)         null,
   G_ParentID           int                  not null default 0,
   G_ShowOrder          int                  not null default 0,
   G_Level              int                  null,
   G_ChildCount         int                  null,
   G_Delete             tinyint              null,
   constraint PK_SYS_GROUP primary key clustered (GroupID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'部门', N'user', N'dbo', N'table', N'sys_Group', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'分类ID号', N'user', N'dbo', N'table', N'sys_Group', N'column', N'GroupID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'分类中文说明', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_CName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'上级分类ID0:为最高级', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_ParentID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'显示顺序', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_ShowOrder'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'当前分类所在层数', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_Level'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'当前分类子分类数', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_ChildCount'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'是否删除1:是0:否', N'user', N'dbo', N'table', N'sys_Group', N'column', N'G_Delete'
go


/*==============================================================*/
/* Table: sys_Module                                            */
/*==============================================================*/
create table dbo.sys_Module (
   ModuleID             int                  identity,
   M_ApplicationID      int                  not null,
   M_ParentID           int                  not null,
   M_PageCode           varchar(6)           not null,
   M_CName              nvarchar(50)         null,
   M_Directory          nvarchar(255)        null,
   M_OrderLevel         varchar(4)           null,
   M_IsSystem           tinyint              null,
   M_Close              tinyint              null,
   M_Icon               varchar(255)         null,
   constraint PK_Sys_Module primary key clustered (M_PageCode, M_ApplicationID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'功能模块', N'user', N'dbo', N'table', N'sys_Module', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'功能模块ID号', N'user', N'dbo', N'table', N'sys_Module', N'column', N'ModuleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'所属应用程序ID', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_ApplicationID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'所属父级模块ID与ModuleID关联,0为顶级', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_ParentID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块编码Parent为0,则为S00(xx),否则为S00M00(xx)', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_PageCode'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块/栏目名称当ParentID为0为模块名称', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_CName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块/栏目目录名', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_Directory'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'当前所在排序级别支持双层99级菜单', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_OrderLevel'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'是否为系统模块1:是0:否如为系统则无法修改', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_IsSystem'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'是否关闭1:是0:否', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_Close'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块Icon', N'user', N'dbo', N'table', N'sys_Module', N'column', N'M_Icon'
go


/*==============================================================*/
/* Table: sys_ModuleExtPermission                               */
/*==============================================================*/
create table dbo.sys_ModuleExtPermission (
   ExtPermissionID      int                  identity,
   ModuleID             int                  not null,
   PermissionName       nvarchar(100)        not null,
   PermissionValue      int                  not null,
   constraint PK_SYS_MODULEEXTPERMISSION primary key  (ModuleID, PermissionValue)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块扩展权限', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'扩展权限ID', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'ExtPermissionID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'模块ID', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'ModuleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'权限名称', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'PermissionName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'权限值', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'PermissionValue'
go


/*==============================================================*/
/* Table: sys_Online                                            */
/*==============================================================*/
create table dbo.sys_Online (
   OnlineID             int                  identity,
   O_SessionID          varchar(24)          not null,
   O_UserName           nvarchar(20)         not null,
   O_Ip                 varchar(15)          not null,
   O_LoginTime          datetime             not null,
   O_LastTime           datetime             not null,
   O_LastUrl            nvarchar(500)        not null,
   constraint PK_SYS_ONLINE primary key  (O_SessionID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'在线用户表', N'user', N'dbo', N'table', N'sys_Online', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'自动ID', N'user', N'dbo', N'table', N'sys_Online', N'column', N'OnlineID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户SessionID', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_SessionID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户名', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_UserName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户IP地址', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_Ip'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'登陆时间', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_LoginTime'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'最后访问时间', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_LastTime'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'最后请求网站', N'user', N'dbo', N'table', N'sys_Online', N'column', N'O_LastUrl'
go


/*==============================================================*/
/* Table: sys_RoleApplication                                   */
/*==============================================================*/
create table dbo.sys_RoleApplication (
   A_RoleID             int                  not null,
   A_ApplicationID      int                  not null,
   constraint PK_SYS_ROLEAPPLICATION primary key clustered (A_RoleID, A_ApplicationID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色应用表', N'user', N'dbo', N'table', N'sys_RoleApplication', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色ID与sys_Roles中RoleID相关', N'user', N'dbo', N'table', N'sys_RoleApplication', N'column', N'A_RoleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'应用ID与sys_Applications中Appl', N'user', N'dbo', N'table', N'sys_RoleApplication', N'column', N'A_ApplicationID'
go


/*==============================================================*/
/* Table: sys_RolePermission                                    */
/*==============================================================*/
create table dbo.sys_RolePermission (
   PermissionID         int                  identity,
   P_RoleID             int                  not null,
   P_ApplicationID      int                  not null,
   P_PageCode           varchar(20)          not null,
   P_Value              int                  null,
   constraint PK_sys_RolePermission primary key clustered (P_RoleID, P_ApplicationID, P_PageCode)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色应用权限表', N'user', N'dbo', N'table', N'sys_RolePermission', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色应用权限自动ID', N'user', N'dbo', N'table', N'sys_RolePermission', N'column', N'PermissionID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色ID与sys_Roles表中RoleID相', N'user', N'dbo', N'table', N'sys_RolePermission', N'column', N'P_RoleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色所属应用ID与sys_Applicatio', N'user', N'dbo', N'table', N'sys_RolePermission', N'column', N'P_ApplicationID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色应用中页面权限代码', N'user', N'dbo', N'table', N'sys_RolePermission', N'column', N'P_PageCode'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'权限值', N'user', N'dbo', N'table', N'sys_RolePermission', N'column', N'P_Value'
go


/*==============================================================*/
/* Table: sys_Roles                                             */
/*==============================================================*/
create table dbo.sys_Roles (
   RoleID               int                  identity,
   R_UserID             int                  not null,
   R_RoleName           nvarchar(50)         not null,
   R_Description        nvarchar(255)        null,
   constraint PK_sys_Roles primary key clustered (RoleID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色表', N'user', N'dbo', N'table', N'sys_Roles', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色ID自动ID', N'user', N'dbo', N'table', N'sys_Roles', N'column', N'RoleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角角所属用户ID', N'user', N'dbo', N'table', N'sys_Roles', N'column', N'R_UserID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色名称', N'user', N'dbo', N'table', N'sys_Roles', N'column', N'R_RoleName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'角色介绍', N'user', N'dbo', N'table', N'sys_Roles', N'column', N'R_Description'
go


/*==============================================================*/
/* Table: sys_SystemInfo                                        */
/*==============================================================*/
create table dbo.sys_SystemInfo (
   SystemID             int                  identity,
   S_Name               nvarchar(50)         null,
   S_Version            nvarchar(50)         null,
   S_SystemConfigData   image                null,
   S_Licensed           varchar(50)          null,
   constraint PK_SYS_SYSTEMINFO primary key  (SystemID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'系统信息表', N'user', N'dbo', N'table', N'sys_SystemInfo', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'自动ID', N'user', N'dbo', N'table', N'sys_SystemInfo', N'column', N'SystemID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'系统名称', N'user', N'dbo', N'table', N'sys_SystemInfo', N'column', N'S_Name'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'版本号', N'user', N'dbo', N'table', N'sys_SystemInfo', N'column', N'S_Version'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'系统配置信息', N'user', N'dbo', N'table', N'sys_SystemInfo', N'column', N'S_SystemConfigData'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'序列号', N'user', N'dbo', N'table', N'sys_SystemInfo', N'column', N'S_Licensed'
go


/*==============================================================*/
/* Table: sys_User                                              */
/*==============================================================*/
create table dbo.sys_User (
   UserID               int                  identity,
   U_LoginName          nvarchar(20)         not null,
   U_Password           varchar(32)          not null,
   U_CName              nvarchar(20)         null,
   U_EName              varchar(50)          null,
   U_GroupID            int                  not null,
   U_Email              varchar(100)         null,
   U_Type               tinyint              not null default 1,
   U_Status             tinyint              not null default 1,
   U_Licence            varchar(30)          null,
   U_Mac                varchar(50)          null,
   U_Remark             nvarchar(200)        null,
   U_IDCard             varchar(30)          null,
   U_Sex                tinyint              null,
   U_BirthDay           datetime             null,
   U_MobileNo           varchar(15)          null,
   U_UserNO             varchar(20)          null,
   U_WorkStartDate      datetime             null,
   U_WorkEndDate        datetime             null,
   U_CompanyMail        varchar(255)         null,
   U_Title              int                  null,
   U_Extension          varchar(10)          null,
   U_HomeTel            varchar(20)          null,
   U_PhotoUrl           nvarchar(255)        null,
   U_DateTime           datetime             null,
   U_LastIP             varchar(15)          null,
   U_LastDateTime       datetime             null,
   U_ExtendField        ntext                null,
   constraint PK_Sys_User primary key clustered (UserID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户表', N'user', N'dbo', N'table', N'sys_User', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户ID号', N'user', N'dbo', N'table', N'sys_User', N'column', N'UserID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'登陆名', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_LoginName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'密码md5加密字符', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Password'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'中文姓名', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_CName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'英文名', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_EName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'部门ID号与sys_Group表中GroupID关联', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_GroupID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'电子邮件', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Email'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户类型0:超级用户1:普通用户', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Type'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'当前状态0:正常 1:禁止登陆 2:删除', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Status'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户序列号', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Licence'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'锁定机器硬件地址', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Mac'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'备注说明', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Remark'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'身份证号码', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_IDCard'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'性别1:男0:女', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Sex'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'出生日期', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_BirthDay'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'手机号', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_MobileNo'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'员工编号', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_UserNO'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'到职日期', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_WorkStartDate'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'离职日期', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_WorkEndDate'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'公司邮件地址', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_CompanyMail'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'职称与应用字段关联', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Title'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'分机号', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_Extension'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'家中电话', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_HomeTel'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户照片网址', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_PhotoUrl'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'操作时间', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_DateTime'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'最后访问IP', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_LastIP'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'最后访问时间', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_LastDateTime'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'扩展字段', N'user', N'dbo', N'table', N'sys_User', N'column', N'U_ExtendField'
go


/*==============================================================*/
/* Table: sys_UserRoles                                         */
/*==============================================================*/
create table dbo.sys_UserRoles (
   R_UserID             int                  not null,
   R_RoleID             int                  not null,
   constraint PK_SYS_USERROLES primary key clustered (R_UserID, R_RoleID)
)
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户角色表', N'user', N'dbo', N'table', N'sys_UserRoles', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户ID与sys_User表中UserID相关', N'user', N'dbo', N'table', N'sys_UserRoles', N'column', N'R_UserID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'用户所属角色ID与Sys_Roles关联', N'user', N'dbo', N'table', N'sys_UserRoles', N'column', N'R_RoleID'
go


