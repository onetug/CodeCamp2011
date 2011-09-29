CREATE TABLE [dbo].[PreferenceValues] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PreferenceId] INT            NOT NULL,
    [Answer]       NVARCHAR (MAX) NOT NULL
);

