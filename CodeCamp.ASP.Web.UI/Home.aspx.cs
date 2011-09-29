using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodeCamp.RIA.Data.Web;
using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
using Microsoft.Practices.Unity;
using System.Text;
using CodeCamp.ASP.UI.Infrastructure;

namespace CodeCamp.ASP.Web.UI
{

    public partial class Home : Page
    {
        private const string CODE_CAMP_ASP_SL_LANDING_PAGE = "CodeCampRedirect.aspx"; //"CodeCampRIALanding.aspx";
        private const string CODE_CAMP_ASP_SL_PASSWORD_RESET_PAGE = "~/AttendeePasswordReset.aspx";


        private readonly CodeCampDomainService domainService = new CodeCampDomainService();
        private Person Person { get; set; }
        private int EventId;

        protected void PageLoad(object sender, EventArgs e)
        {

        }

        public string SeminoleStateCollegeUrl
        {
            get { return ConfigurationManager.AppSettings["SeminoleStateCollegeUrl"]; }
        }

        protected void Login_Click(object sender, EventArgs e)
        {

            bool IsLoggedIn = false;
            if (!string.IsNullOrEmpty(email.Text) && !string.IsNullOrEmpty(pass.Text))
            {
                ErrorLabel.Visible = true;

                var passwordHash = Encryption.ComputePasswordHash(pass.Text);
                try
                {
                    // to test logging service
                    // throw new CodeCampAuthorizationException("No dice");
                    this.Person = domainService.GetPersonByEmail(email.Text);

                    if (this.Person != null && this.Person.PasswordHash == passwordHash)
                    {
                        bool haveEvent = int.TryParse(ConfigurationManager.AppSettings["CurrentEventId"], out EventId);
                        if (haveEvent)
                        {
                            IsLoggedIn = true;

                        }
                        else
                        {
                            throw new CodeCampConfigurationException("No Available Event to login to.")
                            {
                                ProcessName = "ASP Authentication",
                                ConfigurationItem = "CurrentEventId"
                            };
                        }
                    }
                    else
                    {
                        var message = this.Person == null
                                          ? "Login failed - please retry or register."
                                          : "Invalid Email Address or Password";
                        var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                        loggingService.LogWarning(message);
                        ErrorLabel.Text = message;
                        ErrorLabel.Visible = true;
                    }
                }
                catch (CodeCampConfigurationException cccex)
                {
                    var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                    loggingService.LogWarning(cccex.Message);
                    ErrorLabel.Text = cccex.Message;
                    ErrorLabel.Visible = true;
                }
                catch (CodeCampAuthorizationException ccex)
                {
                    var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();

                    ccex.ProcessName = "ASP Authentication";

                    loggingService.LogException(ccex);
                    ErrorLabel.Text = "Login Failed";
                    ErrorLabel.Visible = true;
                }

                catch (Exception unhandledEx)
                {
                    var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();

                    var ccex = new CodeCampAuthorizationException("Login failed", unhandledEx)
                    {
                        ProcessName = "ASP Authentication Unhandled Exception"
                    };

                    loggingService.LogException(ccex);
                    ErrorLabel.Text = "Login Failed";
                    ErrorLabel.Visible = true;
                }
            }
            if (IsLoggedIn)
            {
                Session["Person"] = this.Person.Id.ToString();
                Session["Event"] = EventId.ToString();
                Session["Action"] = "Agenda";
                Server.Transfer(CODE_CAMP_ASP_SL_LANDING_PAGE);
            }
        }

        protected void ResetPassword_Click(object sender, EventArgs e)
        {
            Server.Transfer(CODE_CAMP_ASP_SL_PASSWORD_RESET_PAGE);
        }
    }
}