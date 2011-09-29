CREATE TABLE [dbo].[Sessions] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (MAX) NOT NULL,
    [Description]          NVARCHAR (MAX) NOT NULL,
    [TrackId]              INT            NOT NULL,
    [StartTime]            DATETIME       NOT NULL,
    [EndTime]              DATETIME       NOT NULL,
    [SessionType]          NVARCHAR (MAX) NOT NULL,
    [RoomId]               INT            NOT NULL,
    [MaxCapacity]          INT            NOT NULL,
    [Status]               NVARCHAR (MAX) NOT NULL,
    [EventPresentation_Id] INT            NOT NULL
);

