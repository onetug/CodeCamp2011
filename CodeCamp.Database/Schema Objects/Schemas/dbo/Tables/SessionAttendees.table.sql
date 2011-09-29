CREATE TABLE [dbo].[SessionAttendees] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [EventAttendeeId] INT            NOT NULL,
    [CheckedIn]       NVARCHAR (MAX) NOT NULL,
    [Rating]          NVARCHAR (MAX) NOT NULL,
    [Comment]         NVARCHAR (MAX) NOT NULL,
    [SessionId]       INT            NOT NULL
);

