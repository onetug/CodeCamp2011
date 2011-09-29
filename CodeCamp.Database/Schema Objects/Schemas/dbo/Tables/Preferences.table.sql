CREATE TABLE [dbo].[Preferences] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [EventId]  INT            NOT NULL,
    [Question] NVARCHAR (MAX) NOT NULL
);

