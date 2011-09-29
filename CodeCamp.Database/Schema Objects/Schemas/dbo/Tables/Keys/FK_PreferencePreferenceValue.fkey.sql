ALTER TABLE [dbo].[PreferenceValues]
    ADD CONSTRAINT [FK_PreferencePreferenceValue] FOREIGN KEY ([PreferenceId]) REFERENCES [dbo].[Preferences] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

