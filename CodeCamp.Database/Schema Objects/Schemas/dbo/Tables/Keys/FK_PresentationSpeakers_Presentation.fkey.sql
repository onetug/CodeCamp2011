ALTER TABLE [dbo].[PresentationSpeakers]
    ADD CONSTRAINT [FK_PresentationSpeakers_Presentation] FOREIGN KEY ([PresentationsAsSpeaker_Id]) REFERENCES [dbo].[Presentations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

