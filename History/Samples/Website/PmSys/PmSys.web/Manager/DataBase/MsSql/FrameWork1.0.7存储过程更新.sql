--FrameWork1.0.7�洢���̸���
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ModuleInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ModuleInsertUpdateDelete]
GO

-- ��������ɾ��
CREATE PROCEDURE sys_ModuleInsertUpdateDelete
(	

	@ModuleID	 int = 0,	-- ����ģ��ID��
	@M_ApplicationID	 int = 0,	-- ����Ӧ�ó���ID
	@M_ParentID	 int = 0,	-- ��������ģ��ID��ModuleID����,0Ϊ����
	@M_PageCode	 varchar(6) = '',	-- ģ�����ParentΪ0,��ΪS00(xx),����ΪS00M00(xx)
	@M_CName	 nvarchar(50) = '',	-- ģ��/��Ŀ���Ƶ�ParentIDΪ0Ϊģ������
	@M_Directory	 nvarchar(255) = '',	-- ģ��/��ĿĿ¼��
	@M_OrderLevel	 varchar(4) = '',	-- ��ǰ�������򼶱�֧��˫��99���˵�
	@M_IsSystem	 tinyint = 0,	-- �Ƿ�Ϊϵͳģ��1:��0:����Ϊϵͳ���޷��޸�
	@M_Close	 tinyint = 0,	-- �Ƿ�ر�1:��0:��
	@M_Icon		 nvarchar(255) = '', --Ĭ��ͼ��
	@DB_Option_Action_  nvarchar(20) = ''		-- �������� Insert:���� Update:�޸� Delete:ɾ�� 
)
AS
	DECLARE @ReturnValue int -- ���ز������
	
	SET @ReturnValue = -1
	
	-- ����
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
	
	-- ����
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
	
	-- ɾ��
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

-- ��������ɾ��
CREATE PROCEDURE sys_ModuleExtPermissionInsertUpdateDelete
(	

	@ExtPermissionID	 int = 0,	-- ��չȨ��ID
	@ModuleID	 int = 0,	-- ģ��ID
	@PermissionName	 nvarchar(100) = '',	-- Ȩ������
	@PermissionValue	 int = 0,	-- Ȩ��ֵ
	@DB_Option_Action_  nvarchar(20) = ''		-- �������� Insert:���� Update:�޸� Delete:ɾ�� 
)
AS
	DECLARE @ReturnValue int -- ���ز������
	
	SET @ReturnValue = -1
	
	-- ����
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
	
	-- ����
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_ModuleExtPermission SET	
			ModuleID = @ModuleID,
			PermissionName = @PermissionName,
			PermissionValue = @PermissionValue
		WHERE (ExtPermissionID = @ExtPermissionID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- ɾ��
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

-- ��������ɾ��
CREATE PROCEDURE sys_RolesInsertUpdateDelete
(	

	@RoleID	 int = 0,	-- ��ɫID�Զ�ID
	@R_UserID	 int = 0,	-- �ǽ������û�ID
	@R_RoleName	 nvarchar(50) = '',	-- ��ɫ����
	@R_Description	 nvarchar(255) = '',	-- ��ɫ����
	@DB_Option_Action_  nvarchar(20) = ''		-- �������� Insert:���� Update:�޸� Delete:ɾ�� 
)
AS
	DECLARE @ReturnValue int -- ���ز������
	
	SET @ReturnValue = -1
	
	-- ����
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
	
	-- ����
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Roles SET	
			R_UserID = @R_UserID,
			R_RoleName = @R_RoleName,
			R_Description = @R_Description
		WHERE (RoleID = @RoleID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- ɾ��
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

