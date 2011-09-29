namespace CodeCamp.ASP.Web.UI
{
    using System;
    using System.Web.UI;

    public partial class CodeCampRIA : Page
    {
        protected string InitParams { get; private set; }
        private string Params = "Person={0},Event={1},Action={2}";
        private const string HOME_PAGE = "Home.aspx";
        private const string PERSON_INDEX = "Person";
        private const string EVENT_INDEX = "Event";
        private const string ACTION_INDEX = "Action";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect(HOME_PAGE, true);
            }

            string personId = Session[PERSON_INDEX].ToString();

            if (string.IsNullOrEmpty(personId))
            {
                Response.Redirect(HOME_PAGE, true);
            }

            string person = Session[PERSON_INDEX].ToString();
            string event_ = Session[EVENT_INDEX].ToString();
            string action = Session[ACTION_INDEX].ToString();
            // build the Init Params
            InitParams = string.Format(Params, person, event_, action);
        }
    }
}