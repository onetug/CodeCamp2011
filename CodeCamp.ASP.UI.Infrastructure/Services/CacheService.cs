using System.Data.Objects.DataClasses;

namespace CodeCamp.ASP.UI.Infrastructure.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Caching;
    using CodeCamp.ASP.UI.Infrastructure.Resources;
    using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
    using CodeCamp.RIA.Data.Web;

    public class CacheService : ICacheService
    {
        ICodeCampDataService CodeCampDataService { get; set; }

        public bool IsInitialized { get; private set; }
        
        public CacheService(ICodeCampDataService codeCampDataService)
        {
            if (codeCampDataService == null)
            {
                throw new ArgumentNullException("codeCampDataService", CodeCampResources.CacheService_CacheService_CodeCampService_is_null_);
            }
            this.CodeCampDataService = codeCampDataService;
            
            this.Initialize();           
        }

        internal void Initialize()
        {
            string currentEventString = ConfigurationManager.AppSettings["CurrentEventId"];
            long currentEventId;
            if (!Int64.TryParse(currentEventString, out currentEventId))
            {
                throw new InvalidCastException("Unable to parse current event Id - check configuration value.");
            }
            this.CodeCampDataService.ConfigureCurrentEvent(currentEventId);

            HttpRuntime.Cache.Insert(CodeCampResources.AnnouncementCacheKey,
                                     CodeCampDataService.CurrentEvent.Announcements,
                                     null,
                                     DateTime.Now.AddMinutes(GetCacheSecondsFromConfig(CodeCampResources.AnnouncementCacheKey)),
                                     Cache.NoSlidingExpiration);

            HttpRuntime.Cache.Insert(CodeCampResources.PresentationsCacheKey,
                                     CodeCampDataService.CurrentEvent.EventPresentations,
                                     null,
                                     DateTime.Now.AddMinutes(GetCacheSecondsFromConfig(CodeCampResources.PresentationsCacheKey)),
                                     Cache.NoSlidingExpiration);

            HttpRuntime.Cache.Insert(CodeCampResources.SessionsCacheKey,
                                    CodeCampDataService.FindAllSessions(),
                                    null,
                                    DateTime.Now.AddMinutes(GetCacheSecondsFromConfig(CodeCampResources.PresentationsCacheKey)),
                                    Cache.NoSlidingExpiration);

            HttpRuntime.Cache.Insert(CodeCampResources.SponsorsCacheKey,
                                     CodeCampDataService.CurrentEvent.Sponsors,
                                     null,
                                     DateTime.Now.AddMinutes(GetCacheSecondsFromConfig(CodeCampResources.SponsorsCacheKey)),
                                     Cache.NoSlidingExpiration);
            
            this.IsInitialized = true;
        }

        public IList<Session> FindSessions()
        {
            IList<Session> sessionCacheItem = RetrieveFromCache <IList<Session>>(CodeCampResources.SessionsCacheKey);

            if (sessionCacheItem == null)
            {
                sessionCacheItem = this.CodeCampDataService.FindAllSessions();

                AddToCache(CodeCampResources.SessionsCacheKey, sessionCacheItem);
            }
            
            return sessionCacheItem;
        }

        public EntityCollection<Announcement> FindAllAnnouncements()
        {
            var announcementCacheItem = RetrieveFromCache<EntityCollection<Announcement>>(CodeCampResources.AnnouncementCacheKey);

            if (announcementCacheItem == null)
            {
                announcementCacheItem = this.CodeCampDataService.CurrentEvent.Announcements;

                AddToCache(CodeCampResources.AnnouncementCacheKey, announcementCacheItem);
            }

            return announcementCacheItem;
        }

        public EntityCollection<EventPresentation> FindPresentations()
        {
            var sessionCacheItem = this.RetrieveFromCache<EntityCollection<EventPresentation>>(CodeCampResources.PresentationsCacheKey);

            if (sessionCacheItem == null)
            {
                sessionCacheItem = this.CodeCampDataService.CurrentEvent.EventPresentations;
                
                AddToCache(CodeCampResources.PresentationsCacheKey, sessionCacheItem);    
            }         
            return sessionCacheItem;
        }

        public EntityCollection<Sponsor> FindSponsors()
        {
            var sessionCacheItem = this.RetrieveFromCache<EntityCollection<Sponsor>>(CodeCampResources.SponsorsCacheKey);

            if (sessionCacheItem == null)
            {
                //var sponsorLevel = SponsorshipLevel.CreateSponsorshipLevel(2, string.Empty, string.Empty, string.Empty);

                sessionCacheItem = this.CodeCampDataService.CurrentEvent.Sponsors;

                AddToCache<EntityCollection<Sponsor>>(CodeCampResources.SponsorsCacheKey, sessionCacheItem);                
            }       

            return sessionCacheItem;   
        }
        
        private void AddToCache<T>(string cacheKey, T cacheItem)
        {
            HttpRuntime.Cache.Insert(
                cacheKey, cacheItem, null,
                DateTime.Now.AddSeconds(GetCacheSecondsFromConfig(cacheKey)), TimeSpan.Zero);
        }

        private T RetrieveFromCache<T>(string cacheKey)
        {
            if (HttpRuntime.Cache[cacheKey] == null)
            {
                return default(T);
            }

            T cacheItem = (T) HttpRuntime.Cache[cacheKey];

            return cacheItem;
        }

        internal double GetCacheSecondsFromConfig(string cacheKey)
        {
            var appSetting = ConfigurationManager.AppSettings[cacheKey];

            double cacheDuration;

            if (!Double.TryParse(appSetting, out cacheDuration))
            {
                cacheDuration = 10d;
            }

            return cacheDuration;
        }
    }
}