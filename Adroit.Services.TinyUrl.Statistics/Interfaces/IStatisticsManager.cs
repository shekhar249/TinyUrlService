using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Statistics.Interfaces
{
    public interface IStatisticsManager
    {
        int GetUrlClickCount(string shortUrl);
        void IncrementUrlClickCount(string shortUrl);
    }
}
