CREATE TABLE [dbo].[Locations] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [Address1] NVARCHAR (MAX) NOT NULL,
    [Address2] NVARCHAR (MAX) NOT NULL,
    [City]     NVARCHAR (MAX) NOT NULL,
    [State]    NVARCHAR (MAX) NOT NULL,
    [Zip]      NVARCHAR (MAX) NOT NULL
);

