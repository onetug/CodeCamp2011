ALTER TABLE [dbo].[Preferences]
    ADD CONSTRAINT [FK_EventPreference] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

