ALTER TABLE [dbo].[SponsorPerson]
    ADD CONSTRAINT [FK_SponsorPerson_Person] FOREIGN KEY ([Owners_Id]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

