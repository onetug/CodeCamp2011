-- =============================================
-- Script Template
-- =============================================
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
USE [OrlandoCodeCamp]
GO
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EventAttendeePreferenceValues
	DROP CONSTRAINT FK_EventAttendeePreferenceValues_PreferenceValue
GO
ALTER TABLE dbo.PreferenceValues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EventAttendeePreferenceValues ADD CONSTRAINT
	PK_EventAttendeePreferenceValues PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.EventAttendeePreferenceValues
	DROP CONSTRAINT FK_EventAttendeePreferenceValues_EventAttendee
GO

ALTER TABLE dbo.EventAttendees SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_EventAttendeePreferenceValues
	(
	Id int NOT NULL IDENTITY (1, 1),
	EventAttendees_Id int NOT NULL,
	PreferenceValues_Id int NOT NULL,
	DummyField int NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_EventAttendeePreferenceValues SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_EventAttendeePreferenceValues OFF
GO
IF EXISTS(SELECT * FROM dbo.EventAttendeePreferenceValues)
	 EXEC('INSERT INTO dbo.Tmp_EventAttendeePreferenceValues (EventAttendees_Id, PreferenceValues_Id, DummyField)
		SELECT EventAttendees_Id, PreferenceValues_Id, DummyField FROM dbo.EventAttendeePreferenceValues WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.EventAttendeePreferenceValues
GO
EXECUTE sp_rename N'dbo.Tmp_EventAttendeePreferenceValues', N'EventAttendeePreferenceValues', 'OBJECT' 
GO
ALTER TABLE dbo.EventAttendeePreferenceValues ADD CONSTRAINT
	FK_EventAttendeePreferenceValues_EventAttendee FOREIGN KEY
	(
	EventAttendees_Id
	) REFERENCES dbo.EventAttendees
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EventAttendeePreferenceValues ADD CONSTRAINT
	FK_EventAttendeePreferenceValues_PreferenceValue FOREIGN KEY
	(
	PreferenceValues_Id
	) REFERENCES dbo.PreferenceValues
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
