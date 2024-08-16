using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Interfaces
{
    public interface ITinyUrlService
    {
        string CreateShortUrl(string longUrl, string? customShortUrl = null);
        string GetLongUrl(string shortUrl);
        bool DeleteShortUrl(string shortUrl);
        int GetUrlClickCount(string shortUrl);
    }
}
