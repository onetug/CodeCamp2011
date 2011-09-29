
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
    //[EnableClientAccess()]
    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'CategoryLogs' query.
        [Query(IsDefault = true)]
        public IQueryable<CategoryLog> GetCategoryLogs()
        {
            return this.ObjectContext.CategoryLogs.Include("Category");
        }

        [Insert]
        public void InsertCategoryLog(CategoryLog categoryLog)
        {
            if ((categoryLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(categoryLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CategoryLogs.AddObject(categoryLog);
            }
        }
        [Update]
        public void UpdateCategoryLog(CategoryLog currentCategoryLog)
        {
            this.ObjectContext.CategoryLogs.AttachAsModified(currentCategoryLog, this.ChangeSet.GetOriginal(currentCategoryLog));
        }
        [Delete]
        public void DeleteCategoryLog(CategoryLog categoryLog)
        {
            if ((categoryLog.EntityState == EntityState.Detached))
            {
                this.ObjectContext.CategoryLogs.Attach(categoryLog);
            }
            this.ObjectContext.CategoryLogs.DeleteObject(categoryLog);
        }
    }
}


