CREATE TABLE [dbo].[People] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [Bio]          NVARCHAR (MAX) NOT NULL,
    [Website]      NVARCHAR (MAX) NOT NULL,
    [Blog]         NVARCHAR (MAX) NOT NULL,
    [Twitter]      NVARCHAR (MAX) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL
);

