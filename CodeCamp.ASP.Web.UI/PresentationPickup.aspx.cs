namespace CodeCamp.ASP.Web.UI
{
    using System;
    using System.Configuration;

    public partial class PresentationPickup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SeminoleStateCollegeUrl
        {
            get { return ConfigurationManager.AppSettings["SeminoleStateCollegeUrl"]; }
        }
    }
}