namespace CodeCamp.ASP.Web.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.DomainServices.Server;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Configuration;
    using CodeCamp.ASP.UI.Infrastructure;
    using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
    using CodeCamp.RIA.Data.Web;
    using Microsoft.Practices.Unity;

    public partial class SessionView : System.Web.UI.Page
    {
        private int SESSION_CODE_COLUMN_INDEX = 0;
    
        public int EventId { get; private set; }
        private int SessionId { get; set; }

        public string SeminoleStateCollegeUrl
        {
            get { return ConfigurationManager.AppSettings["SeminoleStateCollegeUrl"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EventId = Convert.ToInt32(ConfigurationManager.AppSettings["CurrentEventId"]);

            if (IsPostBack)
            {

            }
        }

        public string GetPresenterName(object session)
        {
            var s = session as CodeCamp.RIA.Data.Web.Session;

            if (s == null)
            {
                return string.Empty;
            }

            if ((session as CodeCamp.RIA.Data.Web.Session).EventPresentation.Presentation != null)
            {
                return (session as CodeCamp.RIA.Data.Web.Session).EventPresentation
                                                                 .Presentation
                                                                 .PresentationSpeakers
                                                                 .FirstOrDefault()
                                                                 .Person
                                                                 .Name;
            }
            return string.Empty;           
        }

        void SaveFile(FileUpload fileUpload)
        {
            string savePath = Server.MapPath(@"~\\Presentations\\\");

            // Get the name of the file to upload.
            string fileName = fileUpload.FileName;

            // Create the path and file name to check for duplicates.
            string pathToCheck = savePath + fileName;

            // Create a temporary file name to use for checking duplicates.
            string tempfileName = "";

            // Check to see if a file already exists with the
            // same name as the file to upload.        
            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName = CreateDuplicateTempfileName(fileName, counter);
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;

                // Notify the user that the file name was changed.
                UploadStatusLabel.Text = 
                    string.Format("A file with the same name already exists. <br />Your file was saved as {0}", fileName);
            }
            else
            {
                // Notify the user that the file was saved successfully.
                UploadStatusLabel.Text = "Your file was uploaded successfully.";
            }

            // Append the name of the file to upload to the path.
            savePath += fileName;

            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            fileUpload.SaveAs(savePath);

            SavePresenterFiles(fileName);
        }

        private void SavePresenterFiles(string fileName)
        {
            var svc = new CodeCampDomainService();

            var session = svc.GetSessions().ToList().Where(s => s.Id == this.SessionId).FirstOrDefault();

            var presentation = session.EventPresentation.Presentation;

            File file = null;

            if (presentation != null)
            {
                svc.WebInitialize(DomainOperationType.Submit);

                if (presentation.Files.Count() == 0)
                {
                    file = new File
                               {
                                   Id = -1, 
                                   Description = fileName, 
                                   PresentationId = presentation.Id, 
                                   EntityKey = null, 
                                   Name = fileName, 
                                   Presentation = presentation, 
                                   PresentationReference = null, 
                                   @Type = "zip"
                               };
                    var changeSet = ConstructChangeSet(file, DomainOperation.Insert);                    
                    SubmitChangeSet(changeSet, file, svc);
                }
                else
                {
                    // If I do have one, update it.
                    file = presentation.Files.First();

                    file.Description = fileName;
                    file.Name = fileName;

                    var changeSet = ConstructChangeSet(file, DomainOperation.Update);
                    SubmitChangeSet(changeSet, file, svc);
                }            
            }
        }

        private string CreateDuplicateTempfileName(string fileName, int counter)
        {
            string newFilename = string.Empty;
            string existingFileNameExtension = string.Empty;
            string existingFileName = string.Empty;

            int fileLength = fileName.Length;
            int separatorIndex = fileName.LastIndexOf('.');
            existingFileNameExtension = fileName.Substring(separatorIndex);

            string newfileNameSuffixAndExtension =
                string.Format("[{0}]{1}", counter, existingFileNameExtension);

            existingFileName = fileName.Substring(0, fileLength - (fileLength - separatorIndex));

            newFilename = string.Format("{0}{1}", existingFileName, newfileNameSuffixAndExtension);

            return newFilename;
        }

        protected void SessionUploadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page" || e.CommandName == "Sort")
            {
                return;
            }
            DeriveSessionIdentifier(e);

            if (this.SessionId == 0)
            {
                this.UploadStatusLabel.Text = "Unable to determine Session Id";
                return;
            }

            Button bts = e.CommandSource as Button;
            if (e.CommandName.ToLower() != "upload")
            {
                return;
            }

            var fu = bts.Parent.Parent.FindControl("SessionUpload") as FileUpload;
            if (fu != null)
            {
                if (fu.HasFile)
                {
                    SaveFile(fu);
                }
            }
        }

        private void DeriveSessionIdentifier(GridViewCommandEventArgs e)
        {
            // This fires on the PagerClick also.
            if (e.CommandName == "Page" || e.CommandName == "Sort")
            {
                return;
            }

            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            GridView gv = (GridView)(((((System.Web.UI.Control)(e.CommandSource)).Parent).Parent).Parent).Parent;

            string sessionCode = gv.DataKeys[row.RowIndex][SESSION_CODE_COLUMN_INDEX].ToString();

            int sessionId;
            if (!Int32.TryParse(sessionCode, out sessionId))
            {
                UploadStatusLabel.Text = "Unable to determine Session Id, this is bad.";
                return;
            }
            this.SessionId = sessionId;
        }

        private static ChangeSet ConstructChangeSet<T>(T changeSetEntity, DomainOperation operation)
        {
            // TODO JAS Make this and extension method
            var changeEntry = new ChangeSetEntry {Entity = changeSetEntity, Operation = operation};
            var changeSetEntries = new List<ChangeSetEntry> { changeEntry };
            return new ChangeSet(changeSetEntries);
        }

        private bool SubmitChangeSet(ChangeSet changeSet, File file, CodeCampDomainService domainService)
        {
            var registered = false;
            try
            {
                domainService.Submit(changeSet);
                if (!changeSet.HasError)
                {
                    var index = 0;
                    int timeout;
                    int.TryParse(ConfigurationManager.AppSettings["ServerTimeoutMultiplier"], out timeout); // 5 second timeout - configurable
                    if (timeout == 0)
                        timeout = 100;
                    while (file.Id <= 0)
                    {
                        Thread.Sleep(50);
                        if (++index > timeout) { break; }
                    }
                    if (index <= timeout) { registered = true; }
                    else
                    {
                        var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                        const string message = "Timeout building File PK.";
                        var ccex = new CodeCampAuthorizationException(message)
                        {
                            ProcessName = "RIA Services.",
                            Source = "SessionView.aspx.cs"
                        };
                        loggingService.LogException(ccex);
                        ShowError(ccex.Message);
                        throw ccex;
                    }
                }
                else
                {
                    var message =
                        changeSet.ChangeSetEntries.SelectMany(error => error.ValidationErrors).Aggregate(
                            string.Empty,
                            (current, valError) => current + (valError.Message + Environment.NewLine));
                    throw new CodeCampAuthorizationException(message);
                }
            }
            catch (DomainException dex)
            {
                var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                loggingService.LogException(dex);
                ShowError(dex.Message);
            }
            catch (CodeCampAuthorizationException ccex)
            {
                var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                ccex.ProcessName = "Authorization Exception";
                loggingService.LogException(ccex);
            }
            catch (Exception ex)
            {
                var loggingService = CodeCampUnityContainer.Container.Resolve<ILoggingService>();
                var ccex = new CodeCampAuthorizationException("Generic Exception", ex)
                {
                    ProcessName = "ASP Authentication Unhandled Exception"
                };
                loggingService.LogException(ccex);
                ShowError(ccex.Message);
            }
            finally
            {
                Server.ClearError();
            }
            return registered;
        }

        private void ShowError(string message)
        {
            UploadStatusLabel.Text = message;
        }
    }
}