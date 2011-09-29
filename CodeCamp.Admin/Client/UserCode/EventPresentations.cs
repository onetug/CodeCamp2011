using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;
namespace CodeCamp.Admin
{
    public partial class EventPresentations
    {
        partial void SessionCollectionAddAndEditNew_CanExecute(ref bool result)
        {
            result = true;

        }

        partial void SessionCollectionAddAndEditNew_Execute()
        {
            var session = new Session();
            session.Name = this.EventPresentationCollection.SelectedItem.PresentationTitle;
            session.Description = this.EventPresentationCollection.SelectedItem.Presentation.Description;
            session.EventPresentation = this.EventPresentationCollection.SelectedItem;
            session.SessionType = "Regular Session";
            session.Status = this.EventPresentationCollection.SelectedItem.ApprovalStatus;
        }
    }
}
