ALTER TABLE [dbo].[EventAttendees]
    ADD CONSTRAINT [FK_EventEventAttendee] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

