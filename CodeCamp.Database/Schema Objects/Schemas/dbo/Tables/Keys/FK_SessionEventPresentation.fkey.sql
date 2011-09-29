ALTER TABLE [dbo].[Sessions]
    ADD CONSTRAINT [FK_SessionEventPresentation] FOREIGN KEY ([EventPresentation_Id]) REFERENCES [dbo].[EventPresentations] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

