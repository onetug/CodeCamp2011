
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
        // To support paging you will need to add ordering to the 'PreferenceValues' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<PreferenceValue> GetPreferenceValues()
        {
            return this.ObjectContext.PreferenceValues;
        }

        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<PreferenceValue> GetPreferenceValuesForPreference(int prevId)
        {
            return this.ObjectContext.PreferenceValues.Where(pv => pv.PreferenceId == prevId);
        }

        public PreferenceValue GetPreferenceValue(int id)
        {
            return this.ObjectContext.PreferenceValues.Where(pv => pv.Id == id).FirstOrDefault();
        }
        [Insert]
        public void InsertPreferenceValue(PreferenceValue preferenceValue)
        {
            if ((preferenceValue.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preferenceValue, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PreferenceValues.AddObject(preferenceValue);
            }
        }
        [Update]
        public void UpdatePreferenceValue(PreferenceValue currentPreferenceValue)
        {
            this.ObjectContext.PreferenceValues.AttachAsModified(currentPreferenceValue, this.ChangeSet.GetOriginal(currentPreferenceValue));
        }
        [Delete]
        public void DeletePreferenceValue(PreferenceValue preferenceValue)
        {
            if ((preferenceValue.EntityState == EntityState.Detached))
            {
                this.ObjectContext.PreferenceValues.Attach(preferenceValue);
            }
            this.ObjectContext.PreferenceValues.DeleteObject(preferenceValue);
        }
    }
}


