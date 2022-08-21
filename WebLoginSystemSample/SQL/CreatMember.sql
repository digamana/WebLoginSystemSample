USE [TestDB]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2022/8/21 下午 05:35:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[UserID] [varchar](10) NOT NULL,
	[UserPwd] [varchar](64) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[UserEmail] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
