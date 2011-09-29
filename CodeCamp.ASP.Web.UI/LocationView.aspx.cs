using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CodeCamp.ASP.Web.UI
{
    public partial class LocationView : System.Web.UI.Page
    {
        int EventId;
        protected void Page_Load(object sender, EventArgs e)
        {
            EventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventId"]);
        }
    }
}