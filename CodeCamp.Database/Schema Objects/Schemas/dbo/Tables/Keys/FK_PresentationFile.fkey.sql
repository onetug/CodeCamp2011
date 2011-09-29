ALTER TABLE [dbo].[Files]
    ADD CONSTRAINT [FK_PresentationFile] FOREIGN KEY ([PresentationId]) REFERENCES [dbo].[Presentations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

