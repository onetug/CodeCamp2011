using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeCamp.RIA.UI.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //    if (!IsUserLoggedIn)
            //        Response.Redirect(@"http://localhost:7777/Account/Login.aspx");

            //string actionType = Request.QueryString["action"] as string;

            //if (!string.IsNullOrEmpty(actionType))
            //{
                
            //}
        }

        bool IsUserLoggedIn
        {
            get { return System.Web.HttpContext.Current.User.Identity.IsAuthenticated; }
        }
    }
}