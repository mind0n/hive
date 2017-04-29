
USE [Hive]
GO

/****** Object:  Table [dbo].[tb_hierarchy]    Script Date: 2013/5/5 14:41:03 ******/
DROP TABLE [dbo].[tb_hierarchy]
GO

/****** Object:  Table [dbo].[tb_hierarchy]    Script Date: 2013/5/5 14:41:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tb_hierarchy](
	[id] [int] NULL,
	[name] [nvarchar](50) NULL,
	[pid] [int] NULL
) ON [PRIMARY]

GO

insert into tb_hierarchy(id, name, pid) values(1, 'root', 0)
insert into tb_hierarchy(id, name, pid) values(2, 'user', 1)
insert into tb_hierarchy(id, name, pid) values(3, 'config', 1)
insert into tb_hierarchy(id, name, pid) values(4, 'administrators', 2)
go

with sub as (select * from tb_hierarchy where id=4
	union all select tb_hierarchy.* from tb_hierarchy,sub where sub.pid = tb_hierarchy.id
)
select * from sub
go

with sub as (select * from tb_hierarchy where id=2
	union all select tb_hierarchy.* from tb_hierarchy,sub where sub.id = tb_hierarchy.pid
)
select * from sub

go


