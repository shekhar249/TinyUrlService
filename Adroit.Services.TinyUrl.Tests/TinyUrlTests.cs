using Adroit.Services.TinyUrl.Interfaces;
using Adroit.Services.TinyUrl.Repository;
using Adroit.Services.TinyUrl.Repository.Interfaces;
using Adroit.Services.TinyUrl.Statistics;
using Adroit.Services.TinyUrl.Statistics.Interfaces;
using Adroit.Services.TinyUrl.Statistics.Store;
using Adroit.Services.TinyUrl.Statistics.Store.Interfaces;
using Adroit.Services.TinyUrl.UrlGenerator;
using Adroit.Services.TinyUrl.UrlGenerator.Interfaces;
using Adroit.Services.TinyUrl.Validation;
using Castle.Core.Logging;
using log4net;
using Moq;
namespace Adroit.Services.TinyUrl.Tests
{
    [TestClass]
    public class TinyUrlTests
    {
        private ITinyUrlService tinyUrlService;
        private IUrlRepository urlRepository;
        private ITinyUrlGenerator urlGenerator;
        private ILog logger;
        private IStatisticsManager statisticsManager;
        private IStatisticsStore statisticsStore;
        private ShortUrlValidator shortUrlValidator;
        private LongUrlValidator longUrlValidator;

        [TestInitialize]
        public void Initialize()
        {
            this.logger = new Mock<ILog>().Object;
            this.urlRepository = new UrlRepository();
            this.urlGenerator = new TinyUrlGenerator();
            this.statisticsStore = new StatisticsStore(this.logger);
            this.statisticsManager = new StatisticsManager(this.statisticsStore);
            this.shortUrlValidator = new ShortUrlValidator();
            this.longUrlValidator = new LongUrlValidator();
            this.tinyUrlService = new TinyUrlService(this.urlRepository, this.urlGenerator, this.statisticsManager, this.shortUrlValidator, this.longUrlValidator);
        }
        [TestMethod]
        public void Test_Create_Short_Urls()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            Assert.IsNotNull(shortUrl);
        }

        [TestMethod]
        public void Test_Delete_Short_Urls_Exists()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            var result = this.tinyUrlService.DeleteShortUrl(shortUrl);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
                "Delete short url does not worked as expected in case of not existing short url.")]
        public void Test_Delete_Short_Urls_Does_Not_Exists()
        {
            var shortUrl = "345erter";
            var result = this.tinyUrlService.DeleteShortUrl(shortUrl);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Get_Short_Urls_Exists()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            var result = this.tinyUrlService.GetLongUrl(shortUrl);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
                "Get long url does not worked as expected in case of not existing short url.")]
        public void Test_Get_Short_Urls_Does_Not_Exists()
        {
            var shortUrl = "345erter";
            var result = this.tinyUrlService.GetLongUrl(shortUrl);
        }

    }
}