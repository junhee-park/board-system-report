USE [BoardSystemDb]
GO

/****** Object:  Table [dbo].[Boards]    Script Date: 2020-03-17 오전 10:46:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Boards](
	[BoardNum] [int] IDENTITY(1,1) NOT NULL,
	[BoardContents] [nvarchar](max) NOT NULL,
	[BoardDate] [datetime2](7) NOT NULL,
	[BoardTitle] [nvarchar](max) NOT NULL,
	[BoardViews] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Boards] PRIMARY KEY CLUSTERED 
(
	[BoardNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Boards] ADD  CONSTRAINT [DF_Boards_BoardDate]  DEFAULT (getdate()) FOR [BoardDate]
GO

ALTER TABLE [dbo].[Boards]  WITH CHECK ADD  CONSTRAINT [FK_Boards_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Boards] CHECK CONSTRAINT [FK_Boards_Users_UserId]
GO

