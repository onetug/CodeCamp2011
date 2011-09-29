ALTER TABLE [dbo].[Events]
    ADD CONSTRAINT [FK_EventLocation] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

