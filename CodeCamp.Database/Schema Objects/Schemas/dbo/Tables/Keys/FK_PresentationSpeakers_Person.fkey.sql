ALTER TABLE [dbo].[PresentationSpeakers]
    ADD CONSTRAINT [FK_PresentationSpeakers_Person] FOREIGN KEY ([Speakers_Id]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

