using Adroit.Services.TinyUrl.Statistics.Store.Interfaces;
using Adroit.Services.TinyUrl.Statistics.Store.Model;
using log4net;
using log4net.Core;

namespace Adroit.Services.TinyUrl.Statistics.Store
{
    public class StatisticsStore : IStatisticsStore
    {
        private readonly Dictionary<string, UrlStats> urlStatsCache;
        private readonly ILog logger;
        public StatisticsStore(ILog logger)
        {
            this.urlStatsCache = new Dictionary<string, UrlStats>();
            this.logger = logger;
        }
        public int GetUrlClickCount(string shortUrl)
        {
            if (this.urlStatsCache.ContainsKey(shortUrl))
            {
                return this.urlStatsCache[shortUrl].Count;
            }
            else
            {
                this.logger.Warn($"Short Url: {shortUrl} not found while getting the url click count.");
                return 0;
            }
        }

        public void IncrementUrlClickCount(string shortUrl)
        {
            if (this.urlStatsCache.ContainsKey(shortUrl))
            {
                this.urlStatsCache[shortUrl].Count += 1;
            }
            else
            {
                this.logger.Warn($"Short Url: {shortUrl} not found while increment click count.");
            }
        }

        void IStatisticsStore.AddUrlForStats(string shortUrl)
        {
            this.urlStatsCache[shortUrl] = new UrlStats();
        }      
    }
}
