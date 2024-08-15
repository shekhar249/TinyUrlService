using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.UrlGenerator.Interfaces
{
    internal interface ITinyUrlGenerator
    {
        string GenerateRandomShortUrl(int length);
    }
}
