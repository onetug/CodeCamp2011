ALTER TABLE [dbo].[TrackOwner]
    ADD CONSTRAINT [FK_TrackOwner_Track] FOREIGN KEY ([TracksAsOwner_Id]) REFERENCES [dbo].[Tracks] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

