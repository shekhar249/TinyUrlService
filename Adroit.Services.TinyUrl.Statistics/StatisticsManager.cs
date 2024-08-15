using Adroit.Services.TinyUrl.Statistics.Interfaces;

namespace Adroit.Services.TinyUrl.Statistics
{
    public class StatisticsManager : IStatisticsManager
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
