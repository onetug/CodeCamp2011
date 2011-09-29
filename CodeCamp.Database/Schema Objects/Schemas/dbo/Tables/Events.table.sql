CREATE TABLE [dbo].[Events] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [LocationId]  INT            NOT NULL,
    [TwitterTag]  NVARCHAR (MAX) NOT NULL
);

