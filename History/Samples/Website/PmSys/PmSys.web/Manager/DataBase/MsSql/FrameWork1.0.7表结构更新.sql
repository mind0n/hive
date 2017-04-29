--FrameWork1.07表结构更新.sql by supesoft.com 2008/10/16
--sys_Module 表 增加 M_Icon列
alter table sys_Module add M_Icon nvarchar(255) default('')
go

--sys_Roles 表 增加 M_Icon列
alter table sys_Roles add R_UserID int 
go
update sys_Roles  set R_UserID  = 1
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

