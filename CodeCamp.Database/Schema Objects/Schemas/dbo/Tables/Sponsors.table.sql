CREATE TABLE [dbo].[Sponsors] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [EventId]            INT            NOT NULL,
    [Name]               NVARCHAR (MAX) NOT NULL,
    [Description]        NVARCHAR (MAX) NOT NULL,
    [ApprovalStatus]     NVARCHAR (MAX) NOT NULL,
    [SponsorshipLevelId] INT            NOT NULL,
    [Url]                NVARCHAR (MAX) NOT NULL,
    [Image]              NVARCHAR (MAX) NOT NULL,
    [Notes]              NVARCHAR (MAX) NOT NULL
);

