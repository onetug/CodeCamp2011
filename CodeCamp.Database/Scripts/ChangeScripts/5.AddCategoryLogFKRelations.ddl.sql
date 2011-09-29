-- =============================================
-- Script Template
-- =============================================
USE [OrlandoCodeCamp]
GO

/****** Object:  Table [dbo].[CategoryLog]    Script Date: 02/18/2011 09:49:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[CategoryLog] WITH CHECK ADD CONSTRAINT [FK_CategoryLog_Log] FOREIGN KEY([LogId])
REFERENCES [dbo].[Log] ([LogId])
GO

ALTER TABLE [dbo].[CategoryLog] WITH CHECK ADD CONSTRAINT [FK_CategoryLog_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO