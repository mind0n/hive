--FrameWork1.07表结构更新.sql by supesoft.com 2008/10/16
--sys_Module 表 增加 M_Icon列
alter table SYS_MODULE add M_Icon VARCHAR2(255 BYTE);

--增加角色所属用户ID表
alter table SYS_ROLES add R_USERID NUMBER(10,0);

update SYS_ROLES set R_USERID  = 1;


--创建扩展模块序列
CREATE SEQUENCE SEQ_SYS_MODULEEXTPERMISSION_ID  increment by 1 start with 100 maxvalue 999999999;
--创建扩展权限值表
CREATE TABLE SYS_MODULEEXTPERMISSION
(
  EXTPERMISSIONID INTEGER NOT NULL,
  MODULEID INTEGER NOT NULL,
  PERMISSIONNAME VARCHAR2(100) NOT NULL,
  PERMISSIONVALUE INTEGER NOT NULL
, CONSTRAINT SYS_MODULEEXTPERMISSION_PK PRIMARY KEY
  (
    MODULEID,
    PERMISSIONVALUE
  )
)
;