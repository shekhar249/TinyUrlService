using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Statistics.Store.Interfaces
{
    public interface IStatisticsStore
    {
        void AddUrlForStats(string shortUrl);
        int GetUrlClickCount(string shortUrl);
        void IncrementUrlClickCount(string shortUrl);
    }
}
