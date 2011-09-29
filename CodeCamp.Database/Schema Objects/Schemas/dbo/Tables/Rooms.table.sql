CREATE TABLE [dbo].[Rooms] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [MaxCapacity] INT            NOT NULL,
    [LocationId]  INT            NOT NULL
);

