ALTER TABLE [dbo].[EventOrganizer]
    ADD CONSTRAINT [FK_EventOrganizer_Person] FOREIGN KEY ([Organizers_Id]) REFERENCES [dbo].[People] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

