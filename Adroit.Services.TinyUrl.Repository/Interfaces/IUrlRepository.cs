using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Repository.Interfaces
{
    public interface IUrlRepository
    {
        void Create(string longUrl, string customShortUrl);        
        string Get(string shortUrl);
        bool Contains(string shortUrl);
        bool Delete(string shortUrl);
    }
}
