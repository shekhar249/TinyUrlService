using Adroit.Services.TinyUrl.Interfaces;
using Adroit.Services.TinyUrl.Repository.Interfaces;

namespace Adroit.Services.TinyUrl
{
    public class TinyUrlService : ITinyUrlService
    {
        private readonly IUrlRepository urlRepository;
        public TinyUrlService(IUrlRepository urlRepository) { 
            this.urlRepository = urlRepository;
        }

        public string CreateShortUrl(string longUrl, string? customShortUrl = null)
        {
            throw new NotImplementedException();
        }

        public bool DeleteShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public string GetLongUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
