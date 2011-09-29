ALTER TABLE [dbo].[EventAttendeePreferenceValues]
    ADD CONSTRAINT [FK_EventAttendeePreferenceValues_PreferenceValue] FOREIGN KEY ([PreferenceValues_Id]) REFERENCES [dbo].[PreferenceValues] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

