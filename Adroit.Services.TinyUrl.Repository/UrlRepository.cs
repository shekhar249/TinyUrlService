using Adroit.Services.TinyUrl.Repository.Interfaces;

namespace Adroit.Services.TinyUrl.Repository
{
    public class UrlRepository : IUrlRepository
    {
        public void Create(string longUrl, string customShortUrl)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
