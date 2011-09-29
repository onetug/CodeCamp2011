
namespace CodeCamp.RIA.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using CodeCamp.RIA.Data.Web;


    // Implements application logic using the CodeCampModelContainer context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    // [EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Files' query.
        [Query(IsDefault = true)]
        public IQueryable<File> GetFiles()
        {
            return this.ObjectContext.Files;
        }

        [Query]
        public IQueryable<File> GetFilesForPresentation(int id)
        {
            return this.ObjectContext.Files.Where(f=>f.Presentation.Id == id);
        }

        public File GetFile(int id)
        {
            return this.ObjectContext.Files.Where(f => f.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertFile(File file)
        {
            if ((file.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(file, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Files.AddObject(file);
            }
        }
        [Update]
        public void UpdateFile(File currentFile)
        {
            this.ObjectContext.Files.AttachAsModified(currentFile, this.ChangeSet.GetOriginal(currentFile));
        }
        [Delete]
        public void DeleteFile(File file)
        {
            if ((file.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Files.Attach(file);
            }
            this.ObjectContext.Files.DeleteObject(file);
        }
    }
}


