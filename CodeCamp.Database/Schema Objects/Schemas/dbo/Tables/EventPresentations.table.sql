CREATE TABLE [dbo].[EventPresentations] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [PresentationId] INT            NOT NULL,
    [ApprovalStatus] NVARCHAR (MAX) NOT NULL,
    [EventId]        INT            NOT NULL
);

