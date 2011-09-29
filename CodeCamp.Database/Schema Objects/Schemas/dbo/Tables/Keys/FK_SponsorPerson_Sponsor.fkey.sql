ALTER TABLE [dbo].[SponsorPerson]
    ADD CONSTRAINT [FK_SponsorPerson_Sponsor] FOREIGN KEY ([SponsorsAsOwner_Id]) REFERENCES [dbo].[Sponsors] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

