USE [PmSys]
GO
/****** Object:  StoredProcedure [dbo].[PM_ProjectsInsertUpdateDelete]    Script Date: 09/09/2012 11:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS(select 1 from sysobjects where id= OBJECT_ID('[dbo].[PM_ProjectsInsertUpdateDelete]') and OBJECTPROPERTY(id,N'IsProcedure')=1)
drop procedure [dbo].[PM_ProjectsInsertUpdateDelete]
GO


-- 创建更新删除
CREATE PROCEDURE [dbo].[PM_ProjectsInsertUpdateDelete]
(	
	@ProjectId uniqueidentifier,
	@ProjectNo nvarchar(50),
	@ProjectName nvarchar(255),
	@ProjectStatus int,
	@ProjectStartTime datetime,
	@ProjectDuration numeric(18,1),
	@ProjectBrief nvarchar(1024),
	@ProjectComments nvarchar(MAX),
	@ProjectIcon nvarchar(MAX), 
	@DB_Option_Action_  nvarchar(20) = ''		-- 操作方法 Insert:增加 Update:修改 Delete:删除 
)
AS
	DECLARE @ReturnValue int -- 返回操作结果
	
	SET @ReturnValue = -1
	
	-- 新增
	IF (@DB_Option_Action_='Insert')
	BEGIN
	Insert Into [PM_Project]
		([ProjectNo],[ProjectName],[ProjectStatus],[ProjectStartTime],[ProjectDuration],[ProjectBrief],[ProjectComments],[ProjectIcon] )
	Values
		(@ProjectNo,@ProjectName,@ProjectStatus,@ProjectStartTime,@ProjectDuration,@ProjectBrief,@ProjectComments,@ProjectIcon )

		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 更新
	IF (@DB_Option_Action_='Update')
	BEGIN
		Update PM_Project
		Set
		[ProjectName] = @ProjectName,
		[ProjectStatus] = @ProjectStatus,
		[ProjectStartTime] = @ProjectStartTime,
		[ProjectDuration] = @ProjectDuration,
		[ProjectBrief] = @ProjectBrief,
		[ProjectComments] = @ProjectComments,
		[ProjectIcon] = @ProjectIcon
		Where		
		[ProjectId] = @ProjectId
		
		SET @ReturnValue = @@ROWCOUNT
	END
	
	-- 删除
	IF (@DB_Option_Action_='Delete')
	BEGIN
			Delete PM_Project
	Where
		[ProjectId] = @ProjectId
		SET @ReturnValue = @@ROWCOUNT
  	END

SELECT @ReturnValue







