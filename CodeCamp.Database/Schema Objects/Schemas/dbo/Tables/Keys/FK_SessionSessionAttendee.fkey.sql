ALTER TABLE [dbo].[SessionAttendees]
    ADD CONSTRAINT [FK_SessionSessionAttendee] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Sessions] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

