ALTER TABLE [dbo].[Announcements]
    ADD CONSTRAINT [FK_EventAnnouncement] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

