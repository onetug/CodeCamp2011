﻿ALTER TABLE [dbo].[Sessions]
    ADD CONSTRAINT [FK_SessionRoom] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

