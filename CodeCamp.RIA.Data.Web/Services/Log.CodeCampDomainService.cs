
namespace CodeCamp.RIA.Data.Web
{
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Server;


    public partial class CodeCampDomainService : LinqToEntitiesDomainService<CodeCampModelContainer>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Logs' query.
        [Query(IsDefault = true)]
        public IQueryable<Log> GetLogs()
        {
            return this.ObjectContext.Logs.Include("CategoryLogs").Include("CategoryLogs.Category");
        }

        public Log GetLog(int logId)
        {
            return this.ObjectContext.Logs.Include("CategoryLogs").Include("CategoryLogs.Category").Where(l => l.LogID == logId).SingleOrDefault();
        }
        [Insert]
        public void InsertLog(Log log)
        {
            if ((log.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(log, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Logs.AddObject(log);
            }
        }
        [Update]  // JAS - Not sure we would ever need to update a log entry.
        public void UpdateLog(Log currentLog)
        {
            this.ObjectContext.Logs.AttachAsModified(currentLog, this.ChangeSet.GetOriginal(currentLog));
        }
        [Delete]
        public void DeleteLog(Log log)
        {
            if ((log.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Logs.Attach(log);
            }
            this.ObjectContext.Logs.DeleteObject(log);
        }
    }
}


