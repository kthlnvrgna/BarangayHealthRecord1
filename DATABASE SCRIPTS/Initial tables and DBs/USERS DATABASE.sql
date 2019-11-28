CREATE DATABASE Users
GO

USE [Users]
GO

/****** Object:  Table [dbo].[tbUserInfo]    Script Date: 11/28/2019 12:47:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbUserInfo](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UseName] [varchar](250) NULL,
	[Paasword] [varchar](100) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[BirthDate] [datetime] NULL,
	[Sex] [varchar](10) NULL,
	[UserType] [varchar](20) NULL,
	[UserName] [varchar](250) NULL,
	[Password] [varchar](100) NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


