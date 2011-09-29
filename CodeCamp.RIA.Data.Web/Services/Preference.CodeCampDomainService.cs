
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
        // To support paging you will need to add ordering to the 'Preferences' query.
        [Query(IsDefault = true)]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Preference> GetPreferences()
        {
            return this.ObjectContext.Preferences;
        }

        [Query]
        [OutputCache(OutputCacheLocation.Server, duration: 60 * 60)] // one hour of caching
        public IQueryable<Preference> GetPreferencesForEvent(int eventId)
        {
            return this.ObjectContext.Preferences.Include("PreferenceValues").Where(p => p.EventId == eventId);
        }

        public Preference GetPreference(int id)
        {
            return this.ObjectContext.Preferences.Where(p => p.Id == id).FirstOrDefault();
        }

        public int GetPreferencesForEventCount(int eventId)
        {
            return this.ObjectContext.Preferences.Where(p => p.EventId == eventId).Count();
        }

        [Insert]
        public void InsertPreference(Preference preference)
        {
            if ((preference.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(preference, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Preferences.AddObject(preference);
            }
        }
        [Update]
        public void UpdatePreference(Preference currentPreference)
        {
            this.ObjectContext.Preferences.AttachAsModified(currentPreference, this.ChangeSet.GetOriginal(currentPreference));
        }
        [Delete]
        public void DeletePreference(Preference preference)
        {
            if ((preference.EntityState == EntityState.Detached))
            {
                this.ObjectContext.Preferences.Attach(preference);
            }
            this.ObjectContext.Preferences.DeleteObject(preference);
        }
    }
}


