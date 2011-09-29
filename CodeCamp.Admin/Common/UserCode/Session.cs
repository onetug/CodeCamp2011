using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class Session
    {
        partial void AttendeeCount_Compute(ref int result)
        {
            // Set result to the desired field value
            result = this.SessionAttendees.Count();

        }

        partial void Room_Changed()
        {
            this.MaxCapacity = Room.MaxCapacity;
        }

        partial void Track_Changed()
        {
            if (this.Track.Rooms.Count() > 0)
            {
                var room = this.Track.Rooms.FirstOrDefault();
                this.Room.MaxCapacity = room.MaxCapacity;
                this.Room.Name = room.Name;
                this.Room.Description = room.Description;

            }
        }

    }
}
