-- =============================================
-- Script Template
-- =============================================
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EventOrganizer ADD
	DummyField int NULL
GO
ALTER TABLE dbo.EventOrganizer SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
