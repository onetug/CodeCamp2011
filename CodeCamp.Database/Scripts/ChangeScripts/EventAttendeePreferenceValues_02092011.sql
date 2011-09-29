-- =============================================
-- Script Template
-- =============================================
USE OrlandoCodeCamp
GO

BEGIN TRANSACTION
GO
ALTER TABLE dbo.EventAttendeePreferenceValues ADD
	DummyField int NULL
GO
ALTER TABLE dbo.EventAttendeePreferenceValues SET (LOCK_ESCALATION = TABLE)
GO
COMMIT