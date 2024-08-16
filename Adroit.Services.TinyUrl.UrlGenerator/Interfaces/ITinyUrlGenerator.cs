using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.UrlGenerator.Interfaces
{
    public interface ITinyUrlGenerator
    {
        string GenerateRandomShortUrl(int length);
    }
}
