using Adroit.Services.TinyUrl.Interfaces;
using Adroit.Services.TinyUrl.Repository.Interfaces;
using Adroit.Services.TinyUrl.Statistics.Interfaces;
using Adroit.Services.TinyUrl.UrlGenerator;
using Adroit.Services.TinyUrl.UrlGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace Adroit.Services.TinyUrl
{
    public class TinyUrlService : ITinyUrlService
    {
        private readonly IUrlRepository urlRepository;
        private readonly ITinyUrlGenerator tinyUrlGenerator;
        private readonly int shortUrlLength;
        private readonly IStatisticsManager statisticsManager;

        public TinyUrlService(IUrlRepository urlRepository,
            ITinyUrlGenerator tinyUrlGenerator, 
            IStatisticsManager statisticsManager)
        {
            this.urlRepository = urlRepository;
            this.tinyUrlGenerator = tinyUrlGenerator;
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var objUrlLenght = configuration.GetValue(typeof(int), "AppSettings:ShortUrlLength");
            this.shortUrlLength = objUrlLenght == null ? 9 : (int)objUrlLenght;
            this.statisticsManager = statisticsManager;
        }

        public string CreateShortUrl(string longUrl, string? customShortUrl = null)
        {
            string shortUrl = customShortUrl ?? this.tinyUrlGenerator.GenerateRandomShortUrl(this.shortUrlLength);

            if (this.urlRepository.Contains(shortUrl))
            {
                throw new InvalidOperationException("Short URL already exists. Choose a different custom short URL or generate a random one.");
            }
            this.urlRepository.Create(longUrl, shortUrl);
            return shortUrl;
        }

        public bool DeleteShortUrl(string shortUrl)
        {
            if (!this.urlRepository.Contains(shortUrl))
            {
                throw new InvalidOperationException("Short URL to delete does not exists.");                
            }
            return this.urlRepository.Delete(shortUrl);                       
        }

        public string GetLongUrl(string shortUrl)
        {
            var longUrl = this.urlRepository.Get(shortUrl);
            if (string.IsNullOrEmpty(longUrl)) {
                throw new InvalidOperationException($"Short URL : {shortUrl} does not exists in system.");
            }
            return longUrl;
        }
        public int GetUrlClickCount(string shortUrl)
        {
            throw new NotImplementedException();
        }

    }
}
