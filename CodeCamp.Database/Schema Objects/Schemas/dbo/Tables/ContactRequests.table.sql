CREATE TABLE [dbo].[ContactRequests] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Subject]     NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Category]    NVARCHAR (MAX) NOT NULL,
    [Email]       NVARCHAR (MAX) NOT NULL,
    [EventId]     INT            NOT NULL,
    [Status]      NVARCHAR (MAX) NOT NULL
);

