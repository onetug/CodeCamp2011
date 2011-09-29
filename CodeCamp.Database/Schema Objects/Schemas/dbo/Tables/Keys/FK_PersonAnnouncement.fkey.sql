ALTER TABLE [dbo].[Announcements]
    ADD CONSTRAINT [FK_PersonAnnouncement] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

