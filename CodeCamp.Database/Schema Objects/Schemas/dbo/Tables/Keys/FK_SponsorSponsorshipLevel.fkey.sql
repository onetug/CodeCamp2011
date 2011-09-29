ALTER TABLE [dbo].[Sponsors]
    ADD CONSTRAINT [FK_SponsorSponsorshipLevel] FOREIGN KEY ([SponsorshipLevelId]) REFERENCES [dbo].[SponsorshipLevels] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

