CREATE TABLE [dbo].[Presentations] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Level]       NVARCHAR (MAX) NOT NULL
);

