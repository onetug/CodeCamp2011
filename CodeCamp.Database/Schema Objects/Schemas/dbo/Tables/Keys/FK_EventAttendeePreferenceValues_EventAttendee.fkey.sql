ALTER TABLE [dbo].[EventAttendeePreferenceValues]
    ADD CONSTRAINT [FK_EventAttendeePreferenceValues_EventAttendee] FOREIGN KEY ([EventAttendees_Id]) REFERENCES [dbo].[EventAttendees] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

