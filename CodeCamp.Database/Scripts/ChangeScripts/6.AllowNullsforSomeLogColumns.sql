-- =============================================
-- Script Template
-- =============================================

USE [OrlandoCodeCamp]
GO

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Log
	(
	LogID int NOT NULL IDENTITY (1, 1),
	EventID int NULL,
	Priority int NOT NULL,
	Severity nvarchar(32) NULL,
	Title nvarchar(256) NULL,
	Timestamp datetime NOT NULL,
	MachineName nvarchar(32) NULL,
	AppDomainName nvarchar(512) NULL,
	ProcessID nvarchar(256) NULL,
	ProcessName nvarchar(512) NULL,
	ThreadName nvarchar(512) NULL,
	Win32ThreadId nvarchar(128) NULL,
	Message nvarchar(1500) NULL,
	FormattedMessage ntext NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Log SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Log ON
GO
IF EXISTS(SELECT * FROM dbo.[Log])
	 EXEC('INSERT INTO dbo.Tmp_Log (LogID, EventID, Priority, Severity, Title, Timestamp, MachineName, AppDomainName, ProcessID, ProcessName, ThreadName, Win32ThreadId, Message, FormattedMessage)
		SELECT LogID, EventID, Priority, Severity, Title, Timestamp, MachineName, AppDomainName, ProcessID, ProcessName, ThreadName, Win32ThreadId, Message, FormattedMessage FROM dbo.[Log] WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Log OFF
GO
ALTER TABLE dbo.CategoryLog
	DROP CONSTRAINT FK_CategoryLog_Log
GO
DROP TABLE dbo.[Log]
GO
EXECUTE sp_rename N'dbo.Tmp_Log', N'Log', 'OBJECT' 
GO
ALTER TABLE dbo.[Log] ADD CONSTRAINT
	PK_Log PRIMARY KEY CLUSTERED 
	(
	LogID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.CategoryLog ADD CONSTRAINT
	FK_CategoryLog_Log FOREIGN KEY
	(
	LogID
	) REFERENCES dbo.[Log]
	(
	LogID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CategoryLog SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
