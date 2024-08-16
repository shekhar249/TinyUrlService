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


        #region "Create Urls"
        [TestMethod]
        public void Test_Create_Short_Urls()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/");
            Assert.IsNotNull(shortUrl);
        }

        [TestMethod]
        public void Test_Create_Short_Urls_With_Custom_Url()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/","45dfgqw");
            Assert.IsNotNull(shortUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
                "Create short url did not work as expected in case of duplicate short url.")]
        public void Test_Create_Short_Urls_With_Custom_Url_Duplicate()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/", "45dfgqw");
            var shortUrl1 = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/", "45dfgqw");            
        }
        [TestMethod]    
        public void Test_Create_Short_Urls_With_Duplicate_Long_Url()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/");
            var shortUrl1 = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/");
            Assert.IsNotNull (shortUrl);
            Assert.IsNotNull(shortUrl1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
                "Create short url did not work as expected in case of invalid short url.")]
        public void Test_Create_Short_Urls_Invlid_Short_Url()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.crd.com/solutions/charles-river-ims/","1q34sf");            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
                "Create short url did not work as expected in case of invalid short url.")]
        public void Test_Create_Short_Urls_Invlid_Short_Url_Length()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.crd.com/solutions/charles-river-ims/", "1q34sfed");            
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
               "Create short url did not work as expected in case of invalid long url.")]
        public void Test_Create_Short_Urls_Invlid_Long_Url()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https:/www.crd.com/charles-river-ims/", "1q34sfd");
        }
        #endregion
        #region "Delete Urls"
        [TestMethod]
        public void Test_Delete_Short_Urls_Exists()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            var result = this.tinyUrlService.DeleteShortUrl(shortUrl);
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
                "Delete short url did not work as expected in case of not existing short url.")]
        public void Test_Delete_Short_Urls_Does_Not_Exists()
        {
            var shortUrl = "345erter";
            var result = this.tinyUrlService.DeleteShortUrl(shortUrl);            
        }
        #endregion
        #region "Get urls"
        [TestMethod]
        public void Test_Get_Short_Urls_Exists()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/");
            var result = this.tinyUrlService.GetLongUrl(shortUrl);            
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
                "Get long url did not work as expected in case of not existing short url.")]
        public void Test_Get_Short_Urls_Does_Not_Exists()
        {
            var shortUrl = "345erter";
            var result = this.tinyUrlService.GetLongUrl(shortUrl);
        }
        #endregion
        #region "Get Count"
        [TestMethod]
        public void Test_Get_Url_Count()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.adroit-tt.com/");
            var result = this.tinyUrlService.GetLongUrl(shortUrl);
            result = this.tinyUrlService.GetLongUrl(shortUrl);
            var urlClickCount = this.tinyUrlService.GetUrlClickCount(shortUrl);
            Assert.AreEqual(2, urlClickCount);
        }
        #endregion
    }
}