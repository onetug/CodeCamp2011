CREATE TABLE [dbo].[Tracks] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [EventId] INT            NOT NULL,
    [RoomId]  INT            NOT NULL
);

