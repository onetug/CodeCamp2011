using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.ServiceModel.DomainServices.Server;
using System.Threading;
using System.Web.UI;
using CodeCamp.RIA.Data.Web;
using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
using Microsoft.Practices.Unity;
using CodeCamp.ASP.UI.Infrastructure;

namespace CodeCamp.ASP.Web.UI
{

    public partial class RegisterView : Page
    {
        //#region Fields

        //private const string CODE_CAMP_ASP_SL_LANDING_PAGE = "CodeCampRedirect.aspx"; //"CodeCampRIALanding.aspx";
        //private readonly CodeCampDomainService domainService = new CodeCampDomainService();
        //private Person Person { get; set; }
        //int EventId;

        //#endregion

        //#region Event Handlers

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    int.TryParse(ConfigurationManager.AppSettings["CurrentEventId"], out EventId);
        //}

        //protected void Save_Click(object sender, EventArgs e)
        //{
        //    ErrorPanel.Visible = false;
        //    this.Person = domainService.GetPersonByEmail(EmailAddress1.Text);
        //    if (this.Person == null)
        //    {
        //        Person person = BuildNewPerson();
        //        if (int.TryParse(ConfigurationManager.AppSettings["CurrentEventId"], out EventId))
        //        {
        //            var ev = new EventAttendee { Id = -1, EventId = EventId, PersonId = person.Id, CheckedIn = false, RaffleTicket = null };
        //            person.EventAttendees.Add(ev);
        //            var changeSet = BuildChangeSet(person);
        //            domainService.WebInitialize(DomainOperationType.Submit);
        //            if (TrySubmitNewRegistration(changeSet, person))
        //            {
        //                Session["Person"] = person.Id.ToString();
        //                Session["Event"] = EventId.ToString();
        //                Session["Action"] = "Agenda";
        //                Server.Transfer(CODE_CAMP_ASP_SL_LANDING_PAGE);
        //            }
        //            else { ShowError("Problem with Registration"); }
        //        }
        //        else { ShowError("Configuration Error - No Event"); }
        //    }
        //    else { ShowError("That email is already registered."); }
        //}

        //#endregion

        //#region Methods

        //private Person BuildNewPerson()
        //{
        //    // Id = -1
        //    // reference Colin Blair regarding not null using RIA for an Insert from ASP.Net: 
        //    // http://forums.silverlight.net/forums/p/116668/266040.aspx
        //    var newPerson = new Person
        //    {
        //        Id = -1,
        //        Email = EmailAddress1.Text,
        //        Name = FirstName.Text + " " + LastName.Text,
        //        FirstName = FirstName.Text,
        //        LastName = LastName.Text,
        //        PasswordHash = Encryption.ComputePasswordHash(Password1.Text),
        //        Title = JobTitle.Text,
        //        Blog = Blog.Text,
        //        Website = WebSite.Text,
        //        Bio = Bio.Text,
        //        Twitter = Twitter.Text,
        //        ImageUrl = ImageUrl.Text
        //    };
        //    return newPerson;
        //}

        //private void ShowError(string message)
        //{
        //    LabelError.Text = message;
        //    ErrorPanel.Visible = true;
        //}

        //private static ChangeSet BuildChangeSet(Person person)
        //{
        //    var changeEntry = new ChangeSetEntry { Entity = person, Operation = DomainOperation.Insert };
        //    var changeSetEntries = new List<ChangeSetEntry> { changeEntry };
        //    return new ChangeSet(changeSetEntries);
        //}

        //private bool TrySubmitNewRegistration(ChangeSet changeSet, Person person)
        //{
        //    var registered = false;
        //    try
        //    {
        //        domainService.Submit(changeSet);
        //        if (!changeSet.HasError)
        //        {
        //            var index = 0;
        //            int timeout;
        //            int.TryParse(ConfigurationManager.AppSettings["ServerTimeoutMultiplier"], out timeout); // 5 second timeout - configurable
        //            if (timeout == 0)
        //                timeout = 100;
        //            while (person.Id <= 0)
        //            {
        //                Thread.Sleep(50);
        //                if (++index > timeout) { break; }
        //            }
        //            if (index <= timeout) { registered = true; }
        //            else
        //            {
        //                var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
        //                const string message = "Timeout verifying registration.";
        //                var ccex = new CodeCampAuthorizationException(message)
        //                {
        //                    ProcessName = "RIA Services.",
        //                    Source = "RegisterView.aspx.cs"
        //                };
        //                loggingService.LogException(ccex);
        //                ShowError(ccex.Message);
        //                throw ccex;
        //            }
        //        }
        //        else
        //        {
        //            var message =
        //                changeSet.ChangeSetEntries.SelectMany(error => error.ValidationErrors).Aggregate(
        //                    string.Empty,
        //                    (current, valError) => current + (valError.Message + Environment.NewLine));
        //            throw new CodeCampAuthorizationException(message);
        //        }
        //    }
        //    catch (DomainException dex)
        //    {
        //        var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
        //        loggingService.LogException(dex);
        //        ShowError(dex.Message);
        //    }
        //    catch (CodeCampAuthorizationException ccex)
        //    {
        //        var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
        //        ccex.ProcessName = "ASP Authentication";
        //        loggingService.LogException(ccex);
        //    }
        //    catch (Exception ex)
        //    {
        //        var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
        //        var ccex = new CodeCampAuthorizationException("Login failed", ex)
        //        {
        //            ProcessName = "ASP Authentication Unhandled Exception"
        //        };
        //        loggingService.LogException(ccex);
        //        ShowError(ccex.Message);
        //    }
        //    finally
        //    {
        //        Server.ClearError();
        //    }
        //    return registered;
        //}

        //#endregion

    }
}