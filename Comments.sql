USE [BoardSystemDb]
GO

/****** Object:  Table [dbo].[Comments]    Script Date: 2020-03-17 오전 10:50:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comments](
	[CommentNum] [int] IDENTITY(1,1) NOT NULL,
	[BoardNum] [int] NOT NULL,
	[CommentContents] [nvarchar](max) NOT NULL,
	[CommentDate] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_CommentDate]  DEFAULT (getdate()) FOR [CommentDate]
GO

