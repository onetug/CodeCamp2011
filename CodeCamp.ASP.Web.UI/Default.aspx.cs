using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CodeCamp.ASP.Web.UI
{
    public partial class _Default : System.Web.UI.Page
    {
        //int EventId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (IsSilverlightSupported())
            //        Response.Redirect("http://localhost:8888/CodeCamp.RIA.UITestPage.aspx");
            //}
            //EventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventId"]);
        }

        bool IsSilverlightSupported()
        {
            // TODO: 
            return true;
        }
    }
}
