ALTER TABLE [dbo].[EventAttendees]
    ADD CONSTRAINT [FK_PersonEventAttendee] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

