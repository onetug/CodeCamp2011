ALTER TABLE [dbo].[EventPresentations]
    ADD CONSTRAINT [FK_PresentationSubmissionPresentation] FOREIGN KEY ([PresentationId]) REFERENCES [dbo].[Presentations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

