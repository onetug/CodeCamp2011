using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeCamp.RIA.Data.Web;

namespace CodeCamp.ASP.Web.UI
{
    public partial class PresentationPickupView : System.Web.UI.Page
    {
        private int SESSION_CODE_COLUMN_INDEX = 0;
        private string PRESENTATION_FILE_DIRECTORY = "Presentations";
        private int SessionId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetPresentionNavigationLink(object session)
        {
            var s = session as CodeCamp.RIA.Data.Web.Session;
            File presentionFile = null;

            if (s == null)
            {
                return string.Empty;
            }

            if (s.EventPresentation.Presentation != null)
            {
                if (s.EventPresentation.Presentation.Files.Count() > 0)
                {
                    presentionFile = s.EventPresentation.Presentation.Files.FirstOrDefault();

                    if (presentionFile != null)
                    {
                        string x = string.Format("~/{0}/{1}", PRESENTATION_FILE_DIRECTORY, presentionFile.Name);
                        return x;
                    }
                }
            }
            return string.Empty;
        }

        public string GetPresenterName(object session)
        {
            var s = session as CodeCamp.RIA.Data.Web.Session;

            if (s == null)
            {
                return string.Empty;
            }

            if ((session as CodeCamp.RIA.Data.Web.Session).EventPresentation.Presentation != null)
            {
                return (session as CodeCamp.RIA.Data.Web.Session).EventPresentation
                                                                 .Presentation
                                                                 .PresentationSpeakers
                                                                 .FirstOrDefault()
                                                                 .Person
                                                                 .Name;
            }
            return string.Empty;
        }

        protected void SessionDownloadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page" || e.CommandName == "Sort")
            {
                return;
            }
            DeriveSessionIdentifier(e);

            //if (this.SessionId == 0)
            //{
            //    this.UploadStatusLabel.Text = "Unable to determine Session Id";
            //    return;
            //}

            //Button bts = e.CommandSource as Button;
            //if (e.CommandName.ToLower() != "upload")
            //{
            //    return;
            //}

            //var fu = bts.Parent.Parent.FindControl("SessionUpload") as FileUpload;
            //if (fu != null)
            //{
            //    if (fu.HasFile)
            //    {
            //        SaveFile(fu);
            //    }
            //}
        }

        private void DeriveSessionIdentifier(GridViewCommandEventArgs e)
        {
            // This fires on the PagerClick also.
            if (e.CommandName == "Page" || e.CommandName == "Sort")
            {
                return;
            }

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            GridView gv = (GridView)(((((System.Web.UI.Control)(e.CommandSource)).Parent).Parent).Parent).Parent;

            string sessionCode = gv.DataKeys[row.RowIndex][SESSION_CODE_COLUMN_INDEX].ToString();

            int sessionId;
            if (!Int32.TryParse(sessionCode, out sessionId))
            {
                //UploadStatusLabel.Text = "Unable to determine Session Id, this is bad.";
                return;
            }
            this.SessionId = sessionId;
        }
    }
}