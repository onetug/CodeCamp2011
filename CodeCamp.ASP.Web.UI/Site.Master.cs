using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeCamp.ASP.Web.UI.Auth;
using System.Configuration;
using CodeCamp.RIA.Data.Web;
using Microsoft.Web.UI.WebControls;

namespace CodeCamp.ASP.Web.UI
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private readonly CodeCampDomainService domainService = new CodeCampDomainService();
        private Person Person { get; set; }

        private int EventId;

        public string SemStateCollegeUrl
        {
            get { return ConfigurationManager.AppSettings["SeminoleStateCollegeUrl"]; }
        }

        protected void PageLoad(object sender, EventArgs e)
        {
            EventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventId"]);
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(pass.Text))
            {
                var passwordHash = Encryption.ComputePasswordHash(pass.Text);
                Person = domainService.GetPersonByEmail(email.Text);
                if (Person != null && Person.PasswordHash == passwordHash)
                {
                    EventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventId"]);
                    var destinationUrl = "CodeCampRIALanding.aspx"
                        + "?p=" + Person.Id.ToString()
                        + "&e=" + EventId.ToString()
                        + "&a=agenda";
                    Response.Redirect(destinationUrl);
                }
            }
        }

        protected void Contact_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactView.aspx", true);
        }
        //protected void SignUp_Click(object sender, EventArgs e)
        //{
        //    //string destinationUrl = ConfigurationManager.AppSettings["SilverlightRegistrationUrl"];

        //    //if (!String.IsNullOrEmpty(destinationUrl))
        //    //{
        //    //    Response.Redirect(destinationUrl);
        //    //}

        //    //var s = destinationUrl;
        //    Response.Redirect("RegisterView.aspx", true);
        //}

        //protected void Contact_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("ContactView.aspx", true);
        //}
    }
}
