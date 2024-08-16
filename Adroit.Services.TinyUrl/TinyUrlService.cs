using Adroit.Services.TinyUrl.Interfaces;
using Adroit.Services.TinyUrl.Repository.Interfaces;
using Adroit.Services.TinyUrl.Statistics.Interfaces;
using Adroit.Services.TinyUrl.UrlGenerator;
using Adroit.Services.TinyUrl.UrlGenerator.Interfaces;
using Adroit.Services.TinyUrl.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Xml.Serialization;

namespace Adroit.Services.TinyUrl
{
    public class TinyUrlService : ITinyUrlService
    {
        private readonly IUrlRepository urlRepository;
        private readonly ITinyUrlGenerator tinyUrlGenerator;
        public int ShortUrlLength { get; private set; }
        private readonly IStatisticsManager statisticsManager;
        private readonly ShortUrlValidator shortUrlValidator;
        private readonly LongUrlValidator longUrlValidator;

        public TinyUrlService(IUrlRepository urlRepository,
            ITinyUrlGenerator tinyUrlGenerator,
            IStatisticsManager statisticsManager,
            ShortUrlValidator shortUrlValidator,
            LongUrlValidator longUrlValidator)
        {
            this.urlRepository = urlRepository;
            this.tinyUrlGenerator = tinyUrlGenerator;
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var objUrlLenght = configuration.GetValue(typeof(int), "AppSettings:ShortUrlLength");
            this.ShortUrlLength = objUrlLenght == null ? 7 : (int)objUrlLenght;
            this.statisticsManager = statisticsManager;
            this.shortUrlValidator = shortUrlValidator;
            this.longUrlValidator = longUrlValidator;
        }

        public string CreateShortUrl(string longUrl, string? customShortUrl = null)
        {
            string shortUrl = customShortUrl ?? this.tinyUrlGenerator.GenerateRandomShortUrl(this.ShortUrlLength);

            if (this.urlRepository.Contains(shortUrl))
            {
                throw new InvalidOperationException("Short URL already exists. Choose a different custom short URL or generate a random one.");
            }

            var shortUrlValidationResult = this.shortUrlValidator.Validate(shortUrl);
            if (shortUrlValidationResult != null && shortUrlValidationResult.Count > 0)
            {
                var errorResults = shortUrlValidationResult.Where(r => r.Type == Validation.Model.ValidationResultType.ERROR).ToList();
                if (errorResults.Count > 0)
                {
                    var errorMessage = string.Join(",", errorResults.Select(r => r.Message).ToList());
                    throw new ArgumentException($"Short url is not in required format. Error : {errorMessage}");
                }
            }
            var longUrlValidationResult = this.longUrlValidator.Validate(longUrl);
            if (longUrlValidationResult != null && longUrlValidationResult.Count > 0)
            {
                var errorResults = longUrlValidationResult.Where(r => r.Type == Validation.Model.ValidationResultType.ERROR).ToList();
                if (errorResults.Count > 0)
                {
                    var errorMessage = string.Join(",", errorResults.Select(r => r.Message).ToList());
                    throw new ArgumentException($"Long url is not in required format. Error : {errorMessage}");
                }
            }
            this.urlRepository.Create(longUrl, shortUrl);
            this.statisticsManager.AddUrlForStats(shortUrl);
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
            if (string.IsNullOrEmpty(longUrl))
            {
                throw new InvalidOperationException($"Short URL : {shortUrl} does not exists in system.");
            }
            this.statisticsManager.IncrementUrlClickCount(shortUrl);
            return longUrl;
        }
        public int GetUrlClickCount(string shortUrl)
        {
            return this.statisticsManager.GetUrlClickCount(shortUrl);
        }

    }
}
