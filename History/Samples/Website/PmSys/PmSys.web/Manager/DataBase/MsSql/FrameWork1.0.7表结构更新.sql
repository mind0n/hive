--FrameWork1.07��ṹ����.sql by supesoft.com 2008/10/16
--sys_Module �� ���� M_Icon��
alter table sys_Module add M_Icon nvarchar(255) default('')
go

--sys_Roles �� ���� M_Icon��
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


EXECUTE sp_addextendedproperty N'MS_Description', N'ģ����չȨ��', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', NULL, NULL
go


EXECUTE sp_addextendedproperty N'MS_Description', N'��չȨ��ID', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'ExtPermissionID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'ģ��ID', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'ModuleID'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'Ȩ������', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'PermissionName'
go


EXECUTE sp_addextendedproperty N'MS_Description', N'Ȩ��ֵ', N'user', N'dbo', N'table', N'sys_ModuleExtPermission', N'column', N'PermissionValue'
go

