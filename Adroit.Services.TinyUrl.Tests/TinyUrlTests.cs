using Adroit.Services.TinyUrl.Interfaces;

namespace Adroit.Services.TinyUrl.Tests
{
    [TestClass]
    public class TinyUrlTests
    {
        private  ITinyUrlService tinyUrlService;

        [TestInitialize]
        public void Initialize()
        {
            this.tinyUrlService = new TinyUrlService();
        }
        [TestMethod]
        public void Test_Create_Short_Urls()
        {
            var shortUrl = this.tinyUrlService.CreateShortUrl("https://www.google.com");
            Assert.IsNotNull(shortUrl);
        }
    }
}