using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
using Microsoft.Practices.Unity;
using System.Text;
using CodeCamp.ASP.UI.Infrastructure;

namespace CodeCamp.ASP.Web.UI
{


    public partial class AgendaView : System.Web.UI.Page
    {

        int EventId;
        protected void Page_Load(object sender, EventArgs e)
        {
            var haveEvent = int.TryParse(ConfigurationManager.AppSettings["CurrentEventId"], out EventId);
        }
    }
}