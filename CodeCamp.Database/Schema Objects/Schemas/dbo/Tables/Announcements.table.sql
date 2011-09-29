CREATE TABLE [dbo].[Announcements] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (MAX) NOT NULL,
    [Text]        NVARCHAR (MAX) NOT NULL,
    [AuthorId]    INT            NOT NULL,
    [PublishDate] DATETIME       NULL,
    [EventId]     INT            NOT NULL
);

