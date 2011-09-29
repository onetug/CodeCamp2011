namespace CodeCamp.ASP.Web.UI.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Caching;
    using CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts;
    using CodeCamp.ASP.Web.UI.Resources;
    using CodeCamp.RIA.Data.Web;

    public class CacheService : ICacheService
    {
        private const string AnnouncementCacheKey = "ANNOUNCEMENTS_CACHE";
        private const string SessionsCacheKey = "SESSIONS_CACHE";
        private const string PresentationsCacheKey = "PRESENTATIONS_CACHE";
        private const string SponsorsCacheKey = "SPONSORS_CACHE";


        Cache CodeCampCache { get; set; }

        ICodeCampDataService CodeCampDataService { get; set; }
        IList<Announcement> ICacheService.FindAllAnnouncements()
        {
            return FindAllAnnouncements();
        }

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

        internal void Create()
        {
            if (this.CodeCampCache == null)
            {
                this.CodeCampCache = new Cache();
            }
        }

        internal void Initialize()
        {
            this.Create();


            string currentEventString = ConfigurationManager.AppSettings["CurrentEventId"];
            long currentEventId;
            if (!Int64.TryParse(currentEventString, out currentEventId))
            {
                throw new InvalidCastException("Unable to parse current event Id - check configuration value.");
            }
            this.CodeCampDataService.ConfigureCurrentEvent(currentEventId);

            this.IsInitialized = true;
        }

        public IList<Session> FindSessions()
        {
            IList<Session> sessionCacheItem;
            sessionCacheItem = this.CodeCampDataService.FindAllSessions();
            AddToCache(SessionsCacheKey, sessionCacheItem);
            return sessionCacheItem;
        }

        public static IList<Announcement> FindAllAnnouncements()
        {
            var newsItems = new List<Announcement>();

            newsItems.Add(new Announcement {Title = "News Item One Title", Text = "News Item One!"});
            newsItems.Add(new Announcement {Title = "News Item Two Title", Text = "News Item Two!"});
            newsItems.Add(new Announcement { Title = "News Item Three Title", Text = "News Item Three!" });
            newsItems.Add(new Announcement { Title = "News Item Four Title", Text = "News Item Four!" });
            newsItems.Add(new Announcement { Title = "News Item Five Title", Text = "News Item Five!" });
            
            return new List<Announcement>();
        }

        public IList<Announcement> FindAnnouncements()
        {
            IList<Announcement> announcementCacheItem;
            announcementCacheItem = this.CodeCampDataService.FindAllAnnouncements() as List<Announcement>;
            AddToCache(AnnouncementCacheKey, announcementCacheItem);

            return announcementCacheItem;
        }

        public IList<Presentation> FindPresentations()
        {
            IList<Presentation> sessionCacheItem;
            sessionCacheItem = this.RetrieveFromCache<IList<Presentation>>(PresentationsCacheKey);

            if (sessionCacheItem == null)
            {
                sessionCacheItem = this.CodeCampDataService.FindAllPresentations();
                AddToCache(PresentationsCacheKey, sessionCacheItem);    
            }         
            return sessionCacheItem;
        }

        public IList<Sponsor> FindSponsors()
        {
            IList<Sponsor> sessionCacheItem = this.RetrieveFromCache<IList<Sponsor>>(SponsorsCacheKey);

            if (sessionCacheItem == null)
            {
                var sponsorLevel = SponsorshipLevel.CreateSponsorshipLevel(2, string.Empty, string.Empty, string.Empty);

                sessionCacheItem = this.CodeCampDataService.FindAllSponsors(sponsorLevel);

                AddToCache<IList<Sponsor>>(SponsorsCacheKey, sessionCacheItem);                
            }       
            return sessionCacheItem;   
        }
        
        private void AddToCache<T>(string cacheKey, T cacheItem)
        {
            CodeCampCache.Insert(
                cacheKey, cacheItem, null,
                DateTime.Now.AddSeconds(GetCacheSecondsFromConfig(cacheKey)), TimeSpan.Zero);
        }

        private T RetrieveFromCache<T>(string cacheKey)
        {
            T cacheItem = default(T);

            cacheItem = (T) CodeCampCache.Get(cacheKey);
            
            return cacheItem;
        }

        internal double GetCacheSecondsFromConfig(string cacheKey)
        {
            var appSetting = ConfigurationManager.AppSettings[cacheKey];

            double cacheDuration;

            if (!Double.TryParse(appSetting, out cacheDuration))
            {
                cacheDuration = 30000d;
            }

            return cacheDuration;
        }
    }
}