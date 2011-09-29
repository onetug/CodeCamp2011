ALTER TABLE [dbo].[Rooms]
    ADD CONSTRAINT [FK_LocationRoom] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Locations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

