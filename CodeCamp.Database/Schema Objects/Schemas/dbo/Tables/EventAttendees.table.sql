CREATE TABLE [dbo].[EventAttendees] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CheckedIn]    BIT            NOT NULL,
    [RaffleTicket] NVARCHAR (MAX) NOT NULL,
    [EventId]      INT            NOT NULL,
    [PersonId]     INT            NOT NULL
);

