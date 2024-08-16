using Adroit.Services.TinyUrl.Repository.Interfaces;

namespace Adroit.Services.TinyUrl.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly Dictionary<string, string> urlCache;
        public UrlRepository() {
            urlCache = new Dictionary<string, string>();
        }
        public void Create(string longUrl, string customShortUrl)
        {
            urlCache[customShortUrl] = longUrl;
        }

        public bool Delete(string shortUrl)
        {
            throw new NotImplementedException();
        }

        public string Get(string shortUrl)
        {
            throw new NotImplementedException();
        }

        bool IUrlRepository.Contains(string shortUrl)
        {
            return this.urlCache.ContainsKey(shortUrl);
        }
    }
}
