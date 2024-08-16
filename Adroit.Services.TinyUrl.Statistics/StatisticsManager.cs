using Adroit.Services.TinyUrl.Statistics.Interfaces;
using Adroit.Services.TinyUrl.Statistics.Store;
using Adroit.Services.TinyUrl.Statistics.Store.Interfaces;

namespace Adroit.Services.TinyUrl.Statistics
{
    public class StatisticsManager : IStatisticsManager
    {
       private readonly IStatisticsStore statisticsStore;
        public StatisticsManager(IStatisticsStore statisticsStore)
        {
            this.statisticsStore = statisticsStore;
        }
        public int GetUrlClickCount(string shortUrl)
        {
            return this.statisticsStore.GetUrlClickCount(shortUrl);
        }

        public void IncrementUrlClickCount(string shortUrl)
        {
            this.statisticsStore.IncrementUrlClickCount(shortUrl);
        }

        void IStatisticsManager.AddUrlForStats(string shortUrl)
        {
            this.statisticsStore.AddUrlForStats(shortUrl);
        }
    }
}
