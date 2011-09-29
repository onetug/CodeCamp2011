
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2010 16:30:19
-- Generated from EDMX file: C:\projects\ONETUG\CodeCamp\CodeCamp.Model\CodeCamp.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeCamp];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EventTrack]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tracks] DROP CONSTRAINT [FK_EventTrack];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganizer_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventOrganizer] DROP CONSTRAINT [FK_EventOrganizer_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EventOrganizer_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventOrganizer] DROP CONSTRAINT [FK_EventOrganizer_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonAnnouncement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Announcements] DROP CONSTRAINT [FK_PersonAnnouncement];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_TrackActivity];
GO
IF OBJECT_ID(N'[dbo].[FK_EventLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_EventLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_LocationRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_PresentationFile];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationSpeakers_Presentation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresentationSpeakers] DROP CONSTRAINT [FK_PresentationSpeakers_Presentation];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationSpeakers_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PresentationSpeakers] DROP CONSTRAINT [FK_PresentationSpeakers_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_EventSponsor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sponsors] DROP CONSTRAINT [FK_EventSponsor];
GO
IF OBJECT_ID(N'[dbo].[FK_SponsorPerson_Sponsor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SponsorPerson] DROP CONSTRAINT [FK_SponsorPerson_Sponsor];
GO
IF OBJECT_ID(N'[dbo].[FK_SponsorPerson_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SponsorPerson] DROP CONSTRAINT [FK_SponsorPerson_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_EventEventAttendee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventAttendees] DROP CONSTRAINT [FK_EventEventAttendee];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEventAttendee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventAttendees] DROP CONSTRAINT [FK_PersonEventAttendee];
GO
IF OBJECT_ID(N'[dbo].[FK_EventAttendeeSessionAttendee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionAttendees] DROP CONSTRAINT [FK_EventAttendeeSessionAttendee];
GO
IF OBJECT_ID(N'[dbo].[FK_PresentationSubmissionPresentation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPresentations] DROP CONSTRAINT [FK_PresentationSubmissionPresentation];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPresentationSubmission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventPresentations] DROP CONSTRAINT [FK_EventPresentationSubmission];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionEventPresentation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionEventPresentation];
GO
IF OBJECT_ID(N'[dbo].[FK_SponsorSponsorshipLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sponsors] DROP CONSTRAINT [FK_SponsorSponsorshipLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackOwner_Track]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackOwner] DROP CONSTRAINT [FK_TrackOwner_Track];
GO
IF OBJECT_ID(N'[dbo].[FK_TrackOwner_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrackOwner] DROP CONSTRAINT [FK_TrackOwner_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sessions] DROP CONSTRAINT [FK_SessionRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_EventAnnouncement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Announcements] DROP CONSTRAINT [FK_EventAnnouncement];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionSessionAttendee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionAttendees] DROP CONSTRAINT [FK_SessionSessionAttendee];
GO
IF OBJECT_ID(N'[dbo].[FK_EventContactRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactRequests] DROP CONSTRAINT [FK_EventContactRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_EventPreference]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Preferences] DROP CONSTRAINT [FK_EventPreference];
GO
IF OBJECT_ID(N'[dbo].[FK_PreferencePreferenceValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PreferenceValues] DROP CONSTRAINT [FK_PreferencePreferenceValue];
GO
IF OBJECT_ID(N'[dbo].[FK_EventAttendeePreferenceValues_EventAttendee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventAttendeePreferenceValues] DROP CONSTRAINT [FK_EventAttendeePreferenceValues_EventAttendee];
GO
IF OBJECT_ID(N'[dbo].[FK_EventAttendeePreferenceValues_PreferenceValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventAttendeePreferenceValues] DROP CONSTRAINT [FK_EventAttendeePreferenceValues_PreferenceValue];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Tracks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tracks];
GO
IF OBJECT_ID(N'[dbo].[Announcements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Announcements];
GO
IF OBJECT_ID(N'[dbo].[Sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sessions];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Presentations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Presentations];
GO
IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[Sponsors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sponsors];
GO
IF OBJECT_ID(N'[dbo].[EventAttendees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventAttendees];
GO
IF OBJECT_ID(N'[dbo].[SessionAttendees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionAttendees];
GO
IF OBJECT_ID(N'[dbo].[EventPresentations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventPresentations];
GO
IF OBJECT_ID(N'[dbo].[SponsorshipLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SponsorshipLevels];
GO
IF OBJECT_ID(N'[dbo].[ContactRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactRequests];
GO
IF OBJECT_ID(N'[dbo].[Preferences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Preferences];
GO
IF OBJECT_ID(N'[dbo].[PreferenceValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PreferenceValues];
GO
IF OBJECT_ID(N'[dbo].[EventOrganizer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventOrganizer];
GO
IF OBJECT_ID(N'[dbo].[PresentationSpeakers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PresentationSpeakers];
GO
IF OBJECT_ID(N'[dbo].[SponsorPerson]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SponsorPerson];
GO
IF OBJECT_ID(N'[dbo].[TrackOwner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrackOwner];
GO
IF OBJECT_ID(N'[dbo].[EventAttendeePreferenceValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventAttendeePreferenceValues];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Bio] nvarchar(max)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL,
    [Blog] nvarchar(max)  NOT NULL,
    [Twitter] nvarchar(max)  NOT NULL,
    [PasswordHash] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [LocationId] int  NOT NULL,
    [TwitterTag] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Tracks'
CREATE TABLE [dbo].[Tracks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL,
    [RoomId] int  NOT NULL
);
GO

-- Creating table 'Announcements'
CREATE TABLE [dbo].[Announcements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [AuthorId] int  NOT NULL,
    [PublishDate] datetime  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [TrackId] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [SessionType] nvarchar(max)  NOT NULL,
    [RoomId] int  NOT NULL,
    [MaxCapacity] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [EventPresentation_Id] int  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address1] nvarchar(max)  NOT NULL,
    [Address2] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [MaxCapacity] int  NOT NULL,
    [LocationId] int  NOT NULL
);
GO

-- Creating table 'Presentations'
CREATE TABLE [dbo].[Presentations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Level] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [PresentationId] int  NOT NULL
);
GO

-- Creating table 'Sponsors'
CREATE TABLE [dbo].[Sponsors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ApprovalStatus] nvarchar(max)  NOT NULL,
    [SponsorshipLevelId] int  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EventAttendees'
CREATE TABLE [dbo].[EventAttendees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CheckedIn] bit  NOT NULL,
    [RaffleTicket] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'SessionAttendees'
CREATE TABLE [dbo].[SessionAttendees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventAttendeeId] int  NOT NULL,
    [CheckedIn] nvarchar(max)  NOT NULL,
    [Rating] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [SessionId] int  NOT NULL
);
GO

-- Creating table 'EventPresentations'
CREATE TABLE [dbo].[EventPresentations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PresentationId] int  NOT NULL,
    [ApprovalStatus] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'SponsorshipLevels'
CREATE TABLE [dbo].[SponsorshipLevels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Amount] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContactRequests'
CREATE TABLE [dbo].[ContactRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Preferences'
CREATE TABLE [dbo].[Preferences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EventId] int  NOT NULL,
    [Question] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PreferenceValues'
CREATE TABLE [dbo].[PreferenceValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PreferenceId] int  NOT NULL,
    [Answer] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EventOrganizer'
CREATE TABLE [dbo].[EventOrganizer] (
    [EventsAsOrganizer_Id] int  NOT NULL,
    [Organizers_Id] int  NOT NULL
);
GO

-- Creating table 'PresentationSpeakers'
CREATE TABLE [dbo].[PresentationSpeakers] (
    [PresentationsAsSpeaker_Id] int  NOT NULL,
    [Speakers_Id] int  NOT NULL
);
GO

-- Creating table 'SponsorPerson'
CREATE TABLE [dbo].[SponsorPerson] (
    [SponsorsAsOwner_Id] int  NOT NULL,
    [Owners_Id] int  NOT NULL
);
GO

-- Creating table 'TrackOwner'
CREATE TABLE [dbo].[TrackOwner] (
    [TracksAsOwner_Id] int  NOT NULL,
    [Owners_Id] int  NOT NULL
);
GO

-- Creating table 'EventAttendeePreferenceValues'
CREATE TABLE [dbo].[EventAttendeePreferenceValues] (
    [EventAttendees_Id] int  NOT NULL,
    [PreferenceValues_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tracks'
ALTER TABLE [dbo].[Tracks]
ADD CONSTRAINT [PK_Tracks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [PK_Announcements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Presentations'
ALTER TABLE [dbo].[Presentations]
ADD CONSTRAINT [PK_Presentations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [PK_Sponsors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventAttendees'
ALTER TABLE [dbo].[EventAttendees]
ADD CONSTRAINT [PK_EventAttendees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionAttendees'
ALTER TABLE [dbo].[SessionAttendees]
ADD CONSTRAINT [PK_SessionAttendees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventPresentations'
ALTER TABLE [dbo].[EventPresentations]
ADD CONSTRAINT [PK_EventPresentations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SponsorshipLevels'
ALTER TABLE [dbo].[SponsorshipLevels]
ADD CONSTRAINT [PK_SponsorshipLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactRequests'
ALTER TABLE [dbo].[ContactRequests]
ADD CONSTRAINT [PK_ContactRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Preferences'
ALTER TABLE [dbo].[Preferences]
ADD CONSTRAINT [PK_Preferences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PreferenceValues'
ALTER TABLE [dbo].[PreferenceValues]
ADD CONSTRAINT [PK_PreferenceValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EventsAsOrganizer_Id], [Organizers_Id] in table 'EventOrganizer'
ALTER TABLE [dbo].[EventOrganizer]
ADD CONSTRAINT [PK_EventOrganizer]
    PRIMARY KEY NONCLUSTERED ([EventsAsOrganizer_Id], [Organizers_Id] ASC);
GO

-- Creating primary key on [PresentationsAsSpeaker_Id], [Speakers_Id] in table 'PresentationSpeakers'
ALTER TABLE [dbo].[PresentationSpeakers]
ADD CONSTRAINT [PK_PresentationSpeakers]
    PRIMARY KEY NONCLUSTERED ([PresentationsAsSpeaker_Id], [Speakers_Id] ASC);
GO

-- Creating primary key on [SponsorsAsOwner_Id], [Owners_Id] in table 'SponsorPerson'
ALTER TABLE [dbo].[SponsorPerson]
ADD CONSTRAINT [PK_SponsorPerson]
    PRIMARY KEY NONCLUSTERED ([SponsorsAsOwner_Id], [Owners_Id] ASC);
GO

-- Creating primary key on [TracksAsOwner_Id], [Owners_Id] in table 'TrackOwner'
ALTER TABLE [dbo].[TrackOwner]
ADD CONSTRAINT [PK_TrackOwner]
    PRIMARY KEY NONCLUSTERED ([TracksAsOwner_Id], [Owners_Id] ASC);
GO

-- Creating primary key on [EventAttendees_Id], [PreferenceValues_Id] in table 'EventAttendeePreferenceValues'
ALTER TABLE [dbo].[EventAttendeePreferenceValues]
ADD CONSTRAINT [PK_EventAttendeePreferenceValues]
    PRIMARY KEY NONCLUSTERED ([EventAttendees_Id], [PreferenceValues_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [EventId] in table 'Tracks'
ALTER TABLE [dbo].[Tracks]
ADD CONSTRAINT [FK_EventTrack]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventTrack'
CREATE INDEX [IX_FK_EventTrack]
ON [dbo].[Tracks]
    ([EventId]);
GO

-- Creating foreign key on [EventsAsOrganizer_Id] in table 'EventOrganizer'
ALTER TABLE [dbo].[EventOrganizer]
ADD CONSTRAINT [FK_EventOrganizer_Event]
    FOREIGN KEY ([EventsAsOrganizer_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Organizers_Id] in table 'EventOrganizer'
ALTER TABLE [dbo].[EventOrganizer]
ADD CONSTRAINT [FK_EventOrganizer_Person]
    FOREIGN KEY ([Organizers_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventOrganizer_Person'
CREATE INDEX [IX_FK_EventOrganizer_Person]
ON [dbo].[EventOrganizer]
    ([Organizers_Id]);
GO

-- Creating foreign key on [AuthorId] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [FK_PersonAnnouncement]
    FOREIGN KEY ([AuthorId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAnnouncement'
CREATE INDEX [IX_FK_PersonAnnouncement]
ON [dbo].[Announcements]
    ([AuthorId]);
GO

-- Creating foreign key on [TrackId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_TrackActivity]
    FOREIGN KEY ([TrackId])
    REFERENCES [dbo].[Tracks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackActivity'
CREATE INDEX [IX_FK_TrackActivity]
ON [dbo].[Sessions]
    ([TrackId]);
GO

-- Creating foreign key on [LocationId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_EventLocation]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventLocation'
CREATE INDEX [IX_FK_EventLocation]
ON [dbo].[Events]
    ([LocationId]);
GO

-- Creating foreign key on [LocationId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_LocationRoom]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRoom'
CREATE INDEX [IX_FK_LocationRoom]
ON [dbo].[Rooms]
    ([LocationId]);
GO

-- Creating foreign key on [PresentationId] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_PresentationFile]
    FOREIGN KEY ([PresentationId])
    REFERENCES [dbo].[Presentations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresentationFile'
CREATE INDEX [IX_FK_PresentationFile]
ON [dbo].[Files]
    ([PresentationId]);
GO

-- Creating foreign key on [PresentationsAsSpeaker_Id] in table 'PresentationSpeakers'
ALTER TABLE [dbo].[PresentationSpeakers]
ADD CONSTRAINT [FK_PresentationSpeakers_Presentation]
    FOREIGN KEY ([PresentationsAsSpeaker_Id])
    REFERENCES [dbo].[Presentations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Speakers_Id] in table 'PresentationSpeakers'
ALTER TABLE [dbo].[PresentationSpeakers]
ADD CONSTRAINT [FK_PresentationSpeakers_Person]
    FOREIGN KEY ([Speakers_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresentationSpeakers_Person'
CREATE INDEX [IX_FK_PresentationSpeakers_Person]
ON [dbo].[PresentationSpeakers]
    ([Speakers_Id]);
GO

-- Creating foreign key on [EventId] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [FK_EventSponsor]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventSponsor'
CREATE INDEX [IX_FK_EventSponsor]
ON [dbo].[Sponsors]
    ([EventId]);
GO

-- Creating foreign key on [SponsorsAsOwner_Id] in table 'SponsorPerson'
ALTER TABLE [dbo].[SponsorPerson]
ADD CONSTRAINT [FK_SponsorPerson_Sponsor]
    FOREIGN KEY ([SponsorsAsOwner_Id])
    REFERENCES [dbo].[Sponsors]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Owners_Id] in table 'SponsorPerson'
ALTER TABLE [dbo].[SponsorPerson]
ADD CONSTRAINT [FK_SponsorPerson_Person]
    FOREIGN KEY ([Owners_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SponsorPerson_Person'
CREATE INDEX [IX_FK_SponsorPerson_Person]
ON [dbo].[SponsorPerson]
    ([Owners_Id]);
GO

-- Creating foreign key on [EventId] in table 'EventAttendees'
ALTER TABLE [dbo].[EventAttendees]
ADD CONSTRAINT [FK_EventEventAttendee]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventEventAttendee'
CREATE INDEX [IX_FK_EventEventAttendee]
ON [dbo].[EventAttendees]
    ([EventId]);
GO

-- Creating foreign key on [PersonId] in table 'EventAttendees'
ALTER TABLE [dbo].[EventAttendees]
ADD CONSTRAINT [FK_PersonEventAttendee]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEventAttendee'
CREATE INDEX [IX_FK_PersonEventAttendee]
ON [dbo].[EventAttendees]
    ([PersonId]);
GO

-- Creating foreign key on [EventAttendeeId] in table 'SessionAttendees'
ALTER TABLE [dbo].[SessionAttendees]
ADD CONSTRAINT [FK_EventAttendeeSessionAttendee]
    FOREIGN KEY ([EventAttendeeId])
    REFERENCES [dbo].[EventAttendees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventAttendeeSessionAttendee'
CREATE INDEX [IX_FK_EventAttendeeSessionAttendee]
ON [dbo].[SessionAttendees]
    ([EventAttendeeId]);
GO

-- Creating foreign key on [PresentationId] in table 'EventPresentations'
ALTER TABLE [dbo].[EventPresentations]
ADD CONSTRAINT [FK_PresentationSubmissionPresentation]
    FOREIGN KEY ([PresentationId])
    REFERENCES [dbo].[Presentations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PresentationSubmissionPresentation'
CREATE INDEX [IX_FK_PresentationSubmissionPresentation]
ON [dbo].[EventPresentations]
    ([PresentationId]);
GO

-- Creating foreign key on [EventId] in table 'EventPresentations'
ALTER TABLE [dbo].[EventPresentations]
ADD CONSTRAINT [FK_EventPresentationSubmission]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPresentationSubmission'
CREATE INDEX [IX_FK_EventPresentationSubmission]
ON [dbo].[EventPresentations]
    ([EventId]);
GO

-- Creating foreign key on [EventPresentation_Id] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionEventPresentation]
    FOREIGN KEY ([EventPresentation_Id])
    REFERENCES [dbo].[EventPresentations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionEventPresentation'
CREATE INDEX [IX_FK_SessionEventPresentation]
ON [dbo].[Sessions]
    ([EventPresentation_Id]);
GO

-- Creating foreign key on [SponsorshipLevelId] in table 'Sponsors'
ALTER TABLE [dbo].[Sponsors]
ADD CONSTRAINT [FK_SponsorSponsorshipLevel]
    FOREIGN KEY ([SponsorshipLevelId])
    REFERENCES [dbo].[SponsorshipLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SponsorSponsorshipLevel'
CREATE INDEX [IX_FK_SponsorSponsorshipLevel]
ON [dbo].[Sponsors]
    ([SponsorshipLevelId]);
GO

-- Creating foreign key on [TracksAsOwner_Id] in table 'TrackOwner'
ALTER TABLE [dbo].[TrackOwner]
ADD CONSTRAINT [FK_TrackOwner_Track]
    FOREIGN KEY ([TracksAsOwner_Id])
    REFERENCES [dbo].[Tracks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Owners_Id] in table 'TrackOwner'
ALTER TABLE [dbo].[TrackOwner]
ADD CONSTRAINT [FK_TrackOwner_Person]
    FOREIGN KEY ([Owners_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TrackOwner_Person'
CREATE INDEX [IX_FK_TrackOwner_Person]
ON [dbo].[TrackOwner]
    ([Owners_Id]);
GO

-- Creating foreign key on [RoomId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionRoom]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionRoom'
CREATE INDEX [IX_FK_SessionRoom]
ON [dbo].[Sessions]
    ([RoomId]);
GO

-- Creating foreign key on [EventId] in table 'Announcements'
ALTER TABLE [dbo].[Announcements]
ADD CONSTRAINT [FK_EventAnnouncement]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventAnnouncement'
CREATE INDEX [IX_FK_EventAnnouncement]
ON [dbo].[Announcements]
    ([EventId]);
GO

-- Creating foreign key on [SessionId] in table 'SessionAttendees'
ALTER TABLE [dbo].[SessionAttendees]
ADD CONSTRAINT [FK_SessionSessionAttendee]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[Sessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionSessionAttendee'
CREATE INDEX [IX_FK_SessionSessionAttendee]
ON [dbo].[SessionAttendees]
    ([SessionId]);
GO

-- Creating foreign key on [EventId] in table 'ContactRequests'
ALTER TABLE [dbo].[ContactRequests]
ADD CONSTRAINT [FK_EventContactRequest]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventContactRequest'
CREATE INDEX [IX_FK_EventContactRequest]
ON [dbo].[ContactRequests]
    ([EventId]);
GO

-- Creating foreign key on [EventId] in table 'Preferences'
ALTER TABLE [dbo].[Preferences]
ADD CONSTRAINT [FK_EventPreference]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPreference'
CREATE INDEX [IX_FK_EventPreference]
ON [dbo].[Preferences]
    ([EventId]);
GO

-- Creating foreign key on [PreferenceId] in table 'PreferenceValues'
ALTER TABLE [dbo].[PreferenceValues]
ADD CONSTRAINT [FK_PreferencePreferenceValue]
    FOREIGN KEY ([PreferenceId])
    REFERENCES [dbo].[Preferences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PreferencePreferenceValue'
CREATE INDEX [IX_FK_PreferencePreferenceValue]
ON [dbo].[PreferenceValues]
    ([PreferenceId]);
GO

-- Creating foreign key on [EventAttendees_Id] in table 'EventAttendeePreferenceValues'
ALTER TABLE [dbo].[EventAttendeePreferenceValues]
ADD CONSTRAINT [FK_EventAttendeePreferenceValues_EventAttendee]
    FOREIGN KEY ([EventAttendees_Id])
    REFERENCES [dbo].[EventAttendees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PreferenceValues_Id] in table 'EventAttendeePreferenceValues'
ALTER TABLE [dbo].[EventAttendeePreferenceValues]
ADD CONSTRAINT [FK_EventAttendeePreferenceValues_PreferenceValue]
    FOREIGN KEY ([PreferenceValues_Id])
    REFERENCES [dbo].[PreferenceValues]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventAttendeePreferenceValues_PreferenceValue'
CREATE INDEX [IX_FK_EventAttendeePreferenceValues_PreferenceValue]
ON [dbo].[EventAttendeePreferenceValues]
    ([PreferenceValues_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------