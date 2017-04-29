if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SupesoftPage]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SupesoftPage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ApplicationsInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ApplicationsInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_EventInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_EventInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_FieldInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_FieldInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_FieldValueInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_FieldValueInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_GroupInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_GroupInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ModuleInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ModuleInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_OnlineInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_OnlineInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_RoleApplicationInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_RoleApplicationInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_RolePermissionInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_RolePermissionInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_RolesInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_RolesInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_SystemInfoInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_SystemInfoInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_UserInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_UserInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_UserRolesInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_UserRolesInsertUpdateDelete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sys_ModuleExtPermissionInsertUpdateDelete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[sys_ModuleExtPermissionInsertUpdateDelete]
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/*********************************************************************************
*      Copyright (C) 2005 141421.com,All Rights Reserved						 *
*      Function:  SuperPaging													 *
*      Description:                                                              *
*             超强通用分页存储过程												 *
*      Author:                                                                   *
*             邱学军(Ryan)                                                       *
*             lifergb@hotmail.com                                                *
*             http://www.141421.com                                              *
*      Finish DateTime:                                                          *
*             2005年9月24日                                                      *
*      History:																	 *
*             2006/4/21 Edit By Michael Li                                       *         
*	   Example:																	 *
*	          SuperPaging @TableName='表名',@Orderfld='排序列名'                 *           
*********************************************************************************/
CREATE PROCEDURE SupesoftPage
(
	@TableName		nvarchar(50),			-- 表名
	@ReturnFields	nvarchar(2000) = '*',	-- 需要返回的列 
	@PageSize		int = 10,				-- 每页记录数
	@PageIndex		int = 1,				-- 当前页码
	@Where			nvarchar(2000) = '',		-- 查询条件
	@Orderfld		nvarchar(2000),			-- 排序字段名 最好为唯一主键
	@OrderType		int = 1					-- 排序类型 1:降序 其它为升序
	
)
AS
    DECLARE @TotalRecord int
	DECLARE @TotalPage int
	DECLARE @CurrentPageSize int
    DECLARE @TotalRecordForPageIndex int
    DECLARE @OrderBy nvarchar(255)
    DECLARE @CutOrderBy nvarchar(255)
	
	if @OrderType = 1
		BEGIN
			set @OrderBy = ' Order by ' + REPLACE(@Orderfld,',',' desc,') + ' desc '
			set @CutOrderBy = ' Order by '+ REPLACE(@Orderfld,',',' asc,') + ' asc '
		END
	else
		BEGIN
			set @OrderBy = ' Order by ' +  REPLACE(@Orderfld,',',' asc,') + ' asc '
			set @CutOrderBy = ' Order by '+ REPLACE(@Orderfld,',',' desc,') + ' desc '			
		END
	
	
        -- 记录总数
	declare @countSql nvarchar(4000)  
	set @countSql='SELECT @TotalRecord=Count(*) From '+@TableName+' '+@Where
	execute sp_executesql @countSql,N'@TotalRecord int out',@TotalRecord out
	
	SET @TotalPage=(@TotalRecord-1)/@PageSize+1
	SET @CurrentPageSize=@PageSize
        IF(@TotalPage=@PageIndex)
	BEGIN
		SET @CurrentPageSize=@TotalRecord%@PageSize
		IF(@CurrentPageSize=0)
			SET @CurrentPageSize=@PageSize
	END
	-- 返回记录
	set @TotalRecordForPageIndex=@PageIndex*@PageSize
	exec('SELECT * FROM
		(SELECT TOP '+@CurrentPageSize+' * FROM
			(SELECT TOP '+@TotalRecordForPageIndex+' '+@ReturnFields+'
			FROM '+@TableName+' '+@Where+' '+@OrderBy+') TB2
		'+@CutOrderBy+') TB3
              '+@OrderBy)
	-- 返回总页数和总记录数
	SELECT @TotalPage as PageCount,@TotalRecord as RecordCount




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_ApplicationsInsertUpdateDelete
(	

	@ApplicationID	 int = 0,	-- 自动ID 1:为系统管理应用
	@A_AppName	 nvarchar(50) = '',	-- 应用名称
	@A_AppDescription	 nvarchar(200) = '',	-- 应用介绍
	@A_AppUrl	 varchar(50) = '',	-- 应用Url地址
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Applications( 
			A_AppName,
			A_AppDescription,
			A_AppUrl
		) VALUES (	
			@A_AppName,
			@A_AppDescription,
			@A_AppUrl
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Applications SET	
			A_AppName = @A_AppName,
			A_AppDescription = @A_AppDescription,
			A_AppUrl = @A_AppUrl
		WHERE (ApplicationID = @ApplicationID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Applications	WHERE (ApplicationID = @ApplicationID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_EventInsertUpdateDelete
(	

	@EventID	 int = 0,	-- 事件ID号
	@E_U_LoginName	 nvarchar(20) = '',	-- 用户名
	@E_UserID	 int = 0,	-- 操作时用户ID与sys_Users中UserID
	@E_DateTime	 datetime = Null,	-- 事件发生的日期及时间
	@E_ApplicationID	 int = 0,	-- 所属应用程序ID与sys_Applicatio
	@E_A_AppName	 nvarchar(50) = '',	-- 所属应用名称
	@E_M_Name	 nvarchar(50) = '',	-- PageCode模块名称与sys_Module相同
	@E_M_PageCode	 varchar(6) = '',	-- 发生事件时模块名称
	@E_From	 nvarchar(500) = '',	-- 来源
	@E_Type	 tinyint = 0,	-- 日记类型,1:操作日记2:安全日志3
	@E_IP	 varchar(15) = '',	-- 客户端IP地址
	@E_Record	 nvarchar(500) = '',	-- 详细描述
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Event( 
			E_U_LoginName,
			E_UserID,
			E_DateTime,
			E_ApplicationID,
			E_A_AppName,
			E_M_Name,
			E_M_PageCode,
			E_From,
			E_Type,
			E_IP,
			E_Record
		) VALUES (	
			@E_U_LoginName,
			@E_UserID,
			@E_DateTime,
			@E_ApplicationID,
			@E_A_AppName,
			@E_M_Name,
			@E_M_PageCode,
			@E_From,
			@E_Type,
			@E_IP,
			@E_Record
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Event SET	
			E_U_LoginName = @E_U_LoginName,
			E_UserID = @E_UserID,
			E_DateTime = @E_DateTime,
			E_ApplicationID = @E_ApplicationID,
			E_A_AppName = @E_A_AppName,
			E_M_Name = @E_M_Name,
			E_M_PageCode = @E_M_PageCode,
			E_From = @E_From,
			E_Type = @E_Type,
			E_IP = @E_IP,
			E_Record = @E_Record
		WHERE (EventID = @EventID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Event	WHERE (EventID = @EventID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_FieldInsertUpdateDelete
(	

	@FieldID	 int = 0,	-- 应用字段ID号
	@F_Key	 varchar(50) = '',	-- 应用字段关键字
	@F_CName	 nvarchar(50) = '',	-- 应用字段中文说明
	@F_Remark	 nvarchar(200) = '',	-- 描述说明
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Field( 
			F_Key,
			F_CName,
			F_Remark
		) VALUES (	
			@F_Key,
			@F_CName,
			@F_Remark
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Field SET	
			F_Key = @F_Key,
			F_CName = @F_CName,
			F_Remark = @F_Remark
		WHERE (FieldID = @FieldID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Field	WHERE (FieldID = @FieldID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_FieldValueInsertUpdateDelete
(	

	@ValueID	 int = 0,	-- 索引ID号
	@V_F_Key	 varchar(50) = '',	-- 与sys_Field表中F_Key字段关联
	@V_Text	 nvarchar(100) = '',	-- 中文说明
	@V_Code	 nvarchar(100) = '',	-- 编码
	@V_ShowOrder	 int = 0,	-- 同级显示顺序
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_FieldValue( 
			V_F_Key,
			V_Text,
			V_Code,
			V_ShowOrder
		) VALUES (	
			@V_F_Key,
			@V_Text,
			@V_Code,
			@V_ShowOrder
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_FieldValue SET	
			V_F_Key = @V_F_Key,
			V_Text = @V_Text,
			V_Code = @V_Code,
			V_ShowOrder = @V_ShowOrder
		WHERE (ValueID = @ValueID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_FieldValue	WHERE (ValueID = @ValueID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_GroupInsertUpdateDelete
(	

	@GroupID	 int = 0,	-- 分类ID号
	@G_CName	 nvarchar(50) = '',	-- 分类中文说明
	@G_ParentID	 int = 0,	-- 上级分类ID0:为最高级
	@G_ShowOrder	 int = 0,	-- 显示顺序
	@G_Level	 int = 0,	-- 当前分类所在层数
	@G_ChildCount	 int = 0,	-- 当前分类子分类数
	@G_Delete	 tinyint = 0,	-- 是否删除1:是0:否
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Group( 
			G_CName,
			G_ParentID,
			G_ShowOrder,
			G_Level,
			G_ChildCount,
			G_Delete
		) VALUES (	
			@G_CName,
			@G_ParentID,
			@G_ShowOrder,
			@G_Level,
			@G_ChildCount,
			@G_Delete
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Group SET	
			G_CName = @G_CName,
			G_ParentID = @G_ParentID,
			G_ShowOrder = @G_ShowOrder,
			G_Level = @G_Level,
			G_ChildCount = @G_ChildCount,
			G_Delete = @G_Delete
		WHERE (GroupID = @GroupID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Group	WHERE (GroupID = @GroupID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
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

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

-- 创建更新删除
CREATE PROCEDURE sys_OnlineInsertUpdateDelete
(	

	@OnlineID	 int = 0,	-- 自动ID
	@O_SessionID	 varchar(24) = '',	-- 用户SessionID
	@O_UserName	 nvarchar(20) = '',	-- 用户名
	@O_Ip	 varchar(15) = '',	-- 用户IP地址
	@O_LoginTime	 datetime = Null,	-- 登陆时间
	@O_LastTime	 datetime = Null,	-- 最后访问时间
	@O_LastUrl	 nvarchar(500) = '',	-- 最后请求网站
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_Online( 
			O_SessionID,
			O_UserName,
			O_Ip,
			O_LoginTime,
			O_LastTime,
			O_LastUrl
		) VALUES (	
			@O_SessionID,
			@O_UserName,
			@O_Ip,
			@O_LoginTime,
			@O_LastTime,
			@O_LastUrl
		)
		SET @ReturnValue = @@IDENTITY
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_Online SET	
			O_SessionID = @O_SessionID,
			O_UserName = @O_UserName,
			O_Ip = @O_Ip,
			O_LoginTime = @O_LoginTime,
			O_LastTime = @O_LastTime,
			O_LastUrl = @O_LastUrl
		WHERE (OnlineID = @OnlineID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_Online	WHERE (OnlineID = @OnlineID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_RoleApplicationInsertUpdateDelete
(	

	@A_RoleID	 int = 0,	-- 角色ID与sys_Roles中RoleID相关
	@A_ApplicationID	 int = 0,	-- 应用ID与sys_Applications中Appl
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_RoleApplication( 
			A_RoleID,
			A_ApplicationID
		) VALUES (	
			@A_RoleID,
			@A_ApplicationID
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_RoleApplication SET	
			A_RoleID = @A_RoleID,
			A_ApplicationID = @A_ApplicationID
		WHERE ( A_RoleID= @A_RoleID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_RoleApplication	WHERE ( A_RoleID= @A_RoleID and A_ApplicationID = @A_ApplicationID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_RolePermissionInsertUpdateDelete
(	

	@PermissionID	 int = 0,	-- 角色应用权限自动ID
	@P_RoleID	 int = 0,	-- 角色ID与sys_Roles表中RoleID相
	@P_ApplicationID	 int = 0,	-- 角色所属应用ID与sys_Applicatio
	@P_PageCode	 varchar(20) = '',	-- 角色应用中页面权限代码
	@P_Value	 int = 0,	-- 权限值
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_RolePermission( 
			P_RoleID,
			P_ApplicationID,
			P_PageCode,
			P_Value
		) VALUES (	
			@P_RoleID,
			@P_ApplicationID,
			@P_PageCode,
			@P_Value
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_RolePermission SET	
			P_RoleID = @P_RoleID,
			P_ApplicationID = @P_ApplicationID,
			P_PageCode = @P_PageCode,
			P_Value = @P_Value
		WHERE (PermissionID = @PermissionID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_RolePermission	WHERE (PermissionID = @PermissionID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






-- 创建更新删除
CREATE  PROCEDURE sys_RolesInsertUpdateDelete
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

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


-- 创建更新删除
CREATE PROCEDURE sys_SystemInfoInsertUpdateDelete
(	

	@SystemID	 int = 0,	-- 自动ID
	@S_Name	 nvarchar(50) = '',	-- 系统名称
	@S_Version	 nvarchar(50) = '',	-- 版本号
	@S_SystemConfigData	 image ,	-- 系统配置信息
	@S_Licensed	 varchar(50) = '',	-- 序列号
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_SystemInfo( 
			S_Name,
			S_Version,
			S_SystemConfigData,
			S_Licensed
		) VALUES (	
			@S_Name,
			@S_Version,
			@S_SystemConfigData,
			@S_Licensed
		)
		SET @ReturnValue = @@IDENTITY
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_SystemInfo SET	
			S_Name = @S_Name,
			S_Version = @S_Version,
			S_SystemConfigData = @S_SystemConfigData,
			S_Licensed = @S_Licensed
		WHERE (SystemID = @SystemID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_SystemInfo	WHERE (SystemID = @SystemID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_UserInsertUpdateDelete
(	

	@UserID	 int = 0,	-- 用户ID号
	@U_LoginName	 nvarchar(20) = '',	-- 登陆名
	@U_Password	 varchar(32) = '',	-- 密码md5加密字符
	@U_CName	 nvarchar(20) = '',	-- 中文姓名
	@U_EName	 varchar(50) = '',	-- 英文名
	@U_GroupID	 int = 0,	-- 部门ID号与sys_Group表中GroupID关联
	@U_Email	 varchar(100) = '',	-- 电子邮件
	@U_Type	 tinyint = 0,	-- 用户类型0:超级用户1:普通用户
	@U_Status	 tinyint = 0,	-- 当前状态0:正常 1:禁止
	@U_Licence	 varchar(30) = '',	-- 用户序列号
	@U_Mac	 varchar(50) = '',	-- 锁定机器硬件地址
	@U_Remark	 nvarchar(200) = '',	-- 备注说明
	@U_IDCard	 varchar(30) = '',	-- 身份证号码
	@U_Sex	 tinyint = 0,	-- 性别1:男0:女
	@U_BirthDay	 datetime = Null,	-- 出生日期
	@U_MobileNo	 varchar(15) = '',	-- 手机号
	@U_UserNO	 varchar(20) = '',	-- 员工编号
	@U_WorkStartDate	 datetime = Null,	-- 到职日期
	@U_WorkEndDate	 datetime = Null,	-- 离职日期
	@U_CompanyMail	 varchar(255) = '',	-- 公司邮件地址
	@U_Title	 int = 0,	-- 职称与应用字段关联
	@U_Extension	 varchar(10) = '',	-- 分机号
	@U_HomeTel	 varchar(20) = '',	-- 家中电话
	@U_PhotoUrl	 nvarchar(255) = '',	-- 用户照片网址
	@U_DateTime	 datetime = Null,	-- 操作时间
	@U_LastIP	 varchar(15) = '',	-- 最后访问IP
	@U_LastDateTime	 datetime = Null,	-- 最后访问时间
	@U_ExtendField	 ntext = '',	-- 扩展字段
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_User( 
			U_LoginName,
			U_Password,
			U_CName,
			U_EName,
			U_GroupID,
			U_Email,
			U_Type,
			U_Status,
			U_Licence,
			U_Mac,
			U_Remark,
			U_IDCard,
			U_Sex,
			U_BirthDay,
			U_MobileNo,
			U_UserNO,
			U_WorkStartDate,
			U_WorkEndDate,
			U_CompanyMail,
			U_Title,
			U_Extension,
			U_HomeTel,
			U_PhotoUrl,
			U_DateTime,
			U_LastIP,
			U_LastDateTime,
			U_ExtendField
		) VALUES (	
			@U_LoginName,
			@U_Password,
			@U_CName,
			@U_EName,
			@U_GroupID,
			@U_Email,
			@U_Type,
			@U_Status,
			@U_Licence,
			@U_Mac,
			@U_Remark,
			@U_IDCard,
			@U_Sex,
			@U_BirthDay,
			@U_MobileNo,
			@U_UserNO,
			@U_WorkStartDate,
			@U_WorkEndDate,
			@U_CompanyMail,
			@U_Title,
			@U_Extension,
			@U_HomeTel,
			@U_PhotoUrl,
			@U_DateTime,
			@U_LastIP,
			@U_LastDateTime,
			@U_ExtendField
		)
		SET @ReturnValue = @@IDENTITY
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_User SET	
			U_LoginName = @U_LoginName,
			U_Password = @U_Password,
			U_CName = @U_CName,
			U_EName = @U_EName,
			U_GroupID = @U_GroupID,
			U_Email = @U_Email,
			U_Type = @U_Type,
			U_Status = @U_Status,
			U_Licence = @U_Licence,
			U_Mac = @U_Mac,
			U_Remark = @U_Remark,
			U_IDCard = @U_IDCard,
			U_Sex = @U_Sex,
			U_BirthDay = @U_BirthDay,
			U_MobileNo = @U_MobileNo,
			U_UserNO = @U_UserNO,
			U_WorkStartDate = @U_WorkStartDate,
			U_WorkEndDate = @U_WorkEndDate,
			U_CompanyMail = @U_CompanyMail,
			U_Title = @U_Title,
			U_Extension = @U_Extension,
			U_HomeTel = @U_HomeTel,
			U_PhotoUrl = @U_PhotoUrl,
			U_DateTime = @U_DateTime,
			U_LastIP = @U_LastIP,
			U_LastDateTime = @U_LastDateTime,
			U_ExtendField = @U_ExtendField
		WHERE (UserID = @UserID)
		
		SET @ReturnValue = @UserID
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_User	WHERE (UserID = @UserID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





-- 创建更新删除
CREATE PROCEDURE sys_UserRolesInsertUpdateDelete
(	

	@R_UserID	 int = 0,	-- 用户ID与sys_User表中UserID相关
	@R_RoleID	 int = 0,	-- 用户所属角色ID与Sys_Roles关联
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
		INSERT INTO sys_UserRoles( 
			R_UserID,
			R_RoleID
		) VALUES (	
			@R_UserID,
			@R_RoleID
		)
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		UPDATE sys_UserRoles SET	
			R_UserID = @R_UserID,
			R_RoleID = @R_RoleID
		WHERE ( R_UserID = @R_UserID and R_RoleID = @R_RoleID)
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
		DELETE sys_UserRoles	WHERE ( R_UserID = @R_UserID and R_RoleID = @R_RoleID)
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



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
