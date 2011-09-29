ALTER TABLE [dbo].[EventOrganizer]
    ADD CONSTRAINT [FK_EventOrganizer_Event] FOREIGN KEY ([EventsAsOrganizer_Id]) REFERENCES [dbo].[Events] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

