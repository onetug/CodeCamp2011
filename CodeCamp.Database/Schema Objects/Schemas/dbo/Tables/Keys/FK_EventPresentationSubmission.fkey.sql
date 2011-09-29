ALTER TABLE [dbo].[EventPresentations]
    ADD CONSTRAINT [FK_EventPresentationSubmission] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

