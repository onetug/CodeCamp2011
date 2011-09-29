ALTER TABLE [dbo].[ContactRequests]
    ADD CONSTRAINT [FK_EventContactRequest] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

