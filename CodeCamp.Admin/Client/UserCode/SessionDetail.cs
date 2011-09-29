using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
namespace CodeCamp.Admin
{
    public partial class SessionDetail
    {
        partial void SessionDetail_Saved()
        {
            try
            { this.Close(true); }
            catch (System.Exception ex)
            {
                var message = ex.Message;
            }
             Application.Current.ShowEventPresentations();
        }
        partial void SessionDetail_Loaded()
        {
            if (!this.Id.HasValue)
            {
                //this.Session = new Session();
                this.Title = "Add a new Session";
            }
            else
            {


                //this.Session = ScreenHandler.GetCurrentParentEntity().Details.Entity.Details;
                this.Title = "Edit " + this.Session.Name;
            }

        }

        partial void SessionDetail_Closing(ref bool cancel)
        {
            try
            {
                if (this.DataWorkspace.CodeCampData.Details.HasChanges)
                {
                    this.DataWorkspace.CodeCampData.SaveChanges();
                }
                ScreenHandler.RefreshCurrentParent();
            }
            catch (System.Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
