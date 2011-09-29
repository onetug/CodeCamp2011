-- =============================================
-- Script Template
-- =============================================
USE OrlandoCodeCamp
GO

BEGIN TRANSACTION
GO
ALTER TABLE dbo.PresentationSpeakers ADD
       DummyField int NULL
GO
ALTER TABLE dbo.PresentationSpeakers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
