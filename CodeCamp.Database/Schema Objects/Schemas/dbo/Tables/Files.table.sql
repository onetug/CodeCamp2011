CREATE TABLE [dbo].[Files] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [Type]           NVARCHAR (MAX) NOT NULL,
    [PresentationId] INT            NOT NULL
);

