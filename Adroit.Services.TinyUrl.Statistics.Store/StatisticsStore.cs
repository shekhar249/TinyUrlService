using Adroit.Services.TinyUrl.Statistics.Store.Interfaces;

namespace Adroit.Services.TinyUrl.Statistics.Store
{
    public class StatisticsStore : IStatisticsStore
    {
        public int GetUrlClickCount(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public void IncrementUrlClickCount(string shortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
