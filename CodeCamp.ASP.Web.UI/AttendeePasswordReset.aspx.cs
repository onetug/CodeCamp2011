using CodeCamp.RIA.Data.Web;

namespace CodeCamp.ASP.Web.UI
{
    using System;
    using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
    using Microsoft.Practices.Unity;
    using System.ServiceModel.DomainServices.Server;
    using System.Net.Mail;
    using System.Configuration;

    public partial class AttendeePasswordReset : System.Web.UI.Page
    {
        private const string CONST_ONETUG_PASSWORD_HASH = "2FB4AB4EBFA4D29E22E5F0246C5EEE7E1F49C5";
        private const string CONST_HOME_PAGE = "~/Home.aspx";
        private const string CONST_MISSING_ATTENDEE_MSG = "Unable to locate your email address.";
        private CodeCampDomainService CodeCampDomainService = new CodeCampDomainService();

        private DomainServiceContext DomainServiceContext { get; set; }

        public string SeminoleStateCollegeUrl
        {
            get { return ConfigurationManager.AppSettings["SeminoleStateCollegeUrl"]; }
        }

        private ICodeCampDataService CodeCampDataService
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControls();
            CodeCampDataService = CodeCampUnityContainer.Container.Resolve<ICodeCampDataService>(); 
        }

        private void InitControls()
        {
            this.MissingAttendee.Visible = false;
        }

        private Person Person { get; set; }

        protected void OkToResetPw_Click(object sender, EventArgs e)
        {
            if (CheckIfCodeCampPersonExists(this.EmailAddress.Text))
            {
                var newPassHash = string.Empty;
                string generatedPassword = string.Empty;
                
                generatedPassword = System.Web.Security.Membership.GeneratePassword(7, 0);
                newPassHash = Encryption.ComputePasswordHash(generatedPassword);                 
                
                this.Person.PasswordHash = newPassHash;
                CodeCampDataService.ResetAttendeePassword(this.Person);

                if(SendNewPassword(generatedPassword))
                { Server.Transfer(CONST_HOME_PAGE, false); }

                
            }
            else
            {
                this.MissingAttendee.Text = CONST_MISSING_ATTENDEE_MSG;
                this.MissingAttendee.Visible = true;                
            }
        }

        private bool CheckIfCodeCampPersonExists(string emailAddress)
        {
            this.Person = CodeCampDataService.FindPersonByEmail(emailAddress);

            if (this.Person != null)
            {
                return true;
            }

            return false;
        }

        bool SendNewPassword(string newPassword)
        {
            string to = this.Person.Email;
            string body = "Hello " + Person.FirstName + "," +
                "\nYou have submitted a request to reset your password.  " +
                "Your new temporary password is as follows:" +
                "\n" + newPassword +
                "\n" + "Please log in with this temporary password and go to your Preferences tab to " +
                "change your password to something that is more permanent." +
                "\nIf you have any problems, questions, or concerns please respond to this email " +
                "for further assistance." +
                "\nRegards," +
                "\nOrlando Code Camp Team";
            
            MailMessage message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.Subject = "Password Reset";
            message.Body = body;
            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;

            try
            {
                client.Send(message);
            }
            catch(Exception e)
            {
                this.MissingAttendee.Text = "There was a failure of our Email server." +
                                            "\nPlease go back to the home page and click" +
                                            "\nthe \"Contact Us\" button to get your password" +
                                            "\nreset properly. Sorry for the inconvenience.";
                MissingAttendee.Visible = true;

                var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                string logWarningMessage = "Failure sending password reset email." +
                                            "\n" + e.ToString();
                loggingService.LogWarning(logWarningMessage);
                loggingService.LogMessage(logWarningMessage);
                loggingService.LogException(e);

                return false;
            }
            return true;
        }
    }
}