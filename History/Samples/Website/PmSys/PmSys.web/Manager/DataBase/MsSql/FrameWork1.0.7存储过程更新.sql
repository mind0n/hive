--FrameWork1.0.7存储过程更新
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ModuleInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ModuleInsertUpdateDelete]
GO

-- 创建更新删除
CREATE PROCEDURE sys_ModuleInsertUpdateDelete
(	

	@ModuleID	 int = 0,	-- 功能模块ID号
	@M_ApplicationID	 int = 0,	-- 所属应用程序ID
	@M_ParentID	 int = 0,	-- 所属父级模块ID与ModuleID关联,0为顶级
	@M_PageCode	 varchar(6) = '',	-- 模块编码Parent为0,则为S00(xx),否则为S00M00(xx)
	@M_CName	 nvarchar(50) = '',	-- 模块/栏目名称当ParentID为0为模块名称
	@M_Directory	 nvarchar(255) = '',	-- 模块/栏目目录名
	@M_OrderLevel	 varchar(4) = '',	-- 当前所在排序级别支持双层99级菜单
	@M_IsSystem	 tinyint = 0,	-- 是否为系统模块1:是0:否如为系统则无法修改
	@M_Close	 tinyint = 0,	-- 是否关闭1:是0:否
	@M_Icon		 nvarchar(255) = '', --默认图标
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Module( 
			M_ApplicationID,
			M_ParentID,
			M_PageCode,
			M_CName,
			M_Directory,
			M_OrderLevel,
			M_IsSystem,
			M_Close,
			M_Icon
		) VALUES (	
			@M_ApplicationID,
			@M_ParentID,
			@M_PageCode,
			@M_CName,
			@M_Directory,
			@M_OrderLevel,
			@M_IsSystem,
			@M_Close,
			@M_Icon
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Module SET	
			M_ApplicationID = @M_ApplicationID,
			M_ParentID = @M_ParentID,
			M_PageCode = @M_PageCode,
			M_CName = @M_CName,
			M_Directory = @M_Directory,
			M_OrderLevel = @M_OrderLevel,
			M_IsSystem = @M_IsSystem,
			M_Close = @M_Close,
			M_Icon = @M_Icon
		WHERE (ModuleID = @ModuleID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Module	WHERE (ModuleID = @ModuleID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue
go


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ModuleExtPermissionInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ModuleExtPermissionInsertUpdateDelete]
GO

-- 创建更新删除
CREATE PROCEDURE sys_ModuleExtPermissionInsertUpdateDelete
(	

	@ExtPermissionID	 int = 0,	-- 扩展权限ID
	@ModuleID	 int = 0,	-- 模块ID
	@PermissionName	 nvarchar(100) = '',	-- 权限名称
	@PermissionValue	 int = 0,	-- 权限值
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_ModuleExtPermission( 
			ModuleID,
			PermissionName,
			PermissionValue
		) VALUES (	
			@ModuleID,
			@PermissionName,
			@PermissionValue
		)
		SET @ReturnValue = @@IDENTITY
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_ModuleExtPermission SET	
			ModuleID = @ModuleID,
			PermissionName = @PermissionName,
			PermissionValue = @PermissionValue
		WHERE (ExtPermissionID = @ExtPermissionID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_ModuleExtPermission	WHERE (ExtPermissionID = @ExtPermissionID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue
go

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_RolesInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_RolesInsertUpdateDelete]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- 创建更新删除
CREATE PROCEDURE sys_RolesInsertUpdateDelete
(	

	@RoleID	 int = 0,	-- 角色ID自动ID
	@R_UserID	 int = 0,	-- 角角所属用户ID
	@R_RoleName	 nvarchar(50) = '',	-- 角色名称
	@R_Description	 nvarchar(255) = '',	-- 角色介绍
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Roles( 
			R_UserID,
			R_RoleName,
			R_Description
		) VALUES (	
			@R_UserID,
			@R_RoleName,
			@R_Description
		)
		SET @ReturnValue = @@IDENTITY
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Roles SET	
			R_UserID = @R_UserID,
			R_RoleName = @R_RoleName,
			R_Description = @R_Description
		WHERE (RoleID = @RoleID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Roles	WHERE (RoleID = @RoleID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

