using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;

namespace CodeCamp.Admin
{
    public partial class CodeCampDataService
    {
        partial void People_Inserting(Person entity)
        {
            entity.Name = entity.FirstName + " " + entity.LastName;
        }

        partial void People_Updating(Person entity)
        {
            entity.Name = entity.FirstName + " " + entity.LastName;
        }

        partial void People_Deleting(Person entity)
        {
            this.Details.Dispatcher.BeginInvoke(() =>
                {
                    foreach (EventAttendee eventAttendee in
                        this.DataWorkspace.CodeCampData.EventAttendees.Where(ea => ea.Person.Id == entity.Id))
                    {
                        foreach (EventAttendeePreferenceValue eventAttendeePreferenceValue in
                            this.DataWorkspace.CodeCampData.EventAttendeePreferenceValues.Where(eapv => eapv.EventAttendees_Id == eventAttendee.Id))
                        {
                            eventAttendeePreferenceValue.Delete();
                        }
                        eventAttendee.Delete();
                    }
                });
        }
    }
}
