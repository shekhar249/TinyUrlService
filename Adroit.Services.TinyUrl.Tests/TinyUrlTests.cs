using Adroit.Services.TinyUrl.Interfaces;
using Adroit.Services.TinyUrl.Repository;
using Adroit.Services.TinyUrl.Repository.Interfaces;
using Adroit.Services.TinyUrl.Statistics;
using Adroit.Services.TinyUrl.Statistics.Interfaces;
using Adroit.Services.TinyUrl.UrlGenerator;
using Adroit.Services.TinyUrl.UrlGenerator.Interfaces;
using Castle.Core.Logging;
using Moq;
namespace Adroit.Services.TinyUrl.Tests
{
    [TestClass]
    public class TinyUrlTests
    {
        private  ITinyUrlService tinyUrlService;
        private IUrlRepository urlRepository;
        private ITinyUrlGenerator   urlGenerator;
        private ILogger logger;
        private IStatisticsManager statisticsManager;

        [TestInitialize]
        public void Initialize()
        {
            this.logger = new Mock<ILogger>().Object;
            this.urlRepository = new UrlRepository();
            this.urlGenerator = new TinyUrlGenerator();
            this.statisticsManager = new StatisticsManager();
            this.tinyUrlService = new TinyUrlService(this.urlRepository,this.urlGenerator,this.statisticsManager);
        }
        [TestMethod]
        public void Test_Create_Short_Urls()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            Assert.IsNotNull(shortUrl);
        }
    }
}