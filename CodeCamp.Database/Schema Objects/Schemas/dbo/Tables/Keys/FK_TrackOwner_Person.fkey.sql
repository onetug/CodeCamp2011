ALTER TABLE [dbo].[TrackOwner]
    ADD CONSTRAINT [FK_TrackOwner_Person] FOREIGN KEY ([Owners_Id]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

