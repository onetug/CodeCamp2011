ALTER TABLE [dbo].[SessionAttendees]
    ADD CONSTRAINT [FK_EventAttendeeSessionAttendee] FOREIGN KEY ([EventAttendeeId]) REFERENCES [dbo].[EventAttendees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

