using Adroit.Services.TinyUrl.Repository;
using static Adroit.Services.TinyUrl.Console.TinyUrlServiceOptions;
using Adroit.Services.TinyUrl.UrlGenerator;
using Adroid.Serivces.Common;
using Adroit.Services.TinyUrl.Statistics;
using Adroit.Services.TinyUrl.Statistics.Store;
using log4net;
using Adroit.Services.TinyUrl.Validation;
using System.Runtime.InteropServices;
namespace Adroit.Services.TinyUrl.Console
{
    internal class Program
    {
        private static ILog logger = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            LogHelper.SetUpLoggingEnvironment(nameof(TinyUrlService));
            var urlRepository = new UrlRepository();
            var urlGenerator = new TinyUrlGenerator();
            var statisticsStore = new StatisticsStore(logger);
            var statisticsManager = new StatisticsManager(statisticsStore);
            var shortUrlValidator = new ShortUrlValidator();
            var longUrlValidator = new LongUrlValidator();
            var tinyUrlService = new TinyUrlService(urlRepository, urlGenerator, statisticsManager,shortUrlValidator,longUrlValidator);
            
            while (true)
            {
                System.Console.WriteLine("\nTinyUrl Service:");
                System.Console.WriteLine("1. Create Short URL");
                System.Console.WriteLine("2. Get Long URL");
                System.Console.WriteLine("3. Delete Short URL");
                System.Console.WriteLine("4. Get URL Click Count");
                System.Console.WriteLine("5. Exit");
                System.Console.Write("Choose an option: ");                
                string? optionString = System.Console.ReadLine();
                int optionInt = 0;
                int.TryParse(optionString,out optionInt);
                TinyUrlServiceOptions option = (TinyUrlServiceOptions) Enum.Parse(typeof(TinyUrlServiceOptions),optionString);
                switch (option)
                {
                    case TinyUrlServiceOptions.CREATE:
                        System.Console.Write("Enter the long URL: ");
                        string longUrl = System.Console.ReadLine();
                        System.Console.Write("Enter a custom short URL (or press enter for random): ");
                        string customShortUrl = System.Console.ReadLine();
                        try
                        {
                            string shortUrl = tinyUrlService.CreateShortUrl(longUrl, string.IsNullOrWhiteSpace(customShortUrl) ? null : customShortUrl);
                            System.Console.WriteLine($"Short URL created: {shortUrl}");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case TinyUrlServiceOptions.GET_URL:
                        System.Console.Write("Enter the short URL: ");
                        string getShortUrl = System.Console.ReadLine();
                        try
                        {
                            string longUrlResult = tinyUrlService.GetLongUrl(getShortUrl);
                            System.Console.WriteLine($"Long URL: {longUrlResult}");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case TinyUrlServiceOptions.DELETE_URL:
                        System.Console.Write("Enter the short URL to delete: ");
                        string deleteShortUrl = System.Console.ReadLine();
                        if (tinyUrlService.DeleteShortUrl(deleteShortUrl))
                        {
                            System.Console.WriteLine("Short URL deleted successfully.");
                        }
                        else
                        {
                            System.Console.WriteLine("Short URL not found.");
                        }

                        break;
                    case TinyUrlServiceOptions.GET_CLICK_COUNT:
                        System.Console.Write("Enter the short URL: ");
                        string clickShortUrl = System.Console.ReadLine();
                        try
                        {
                            int clickCount = tinyUrlService.GetUrlClickCount(clickShortUrl);
                            System.Console.WriteLine($"URL has been clicked {clickCount} times.");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case TinyUrlServiceOptions.EXIT:
                        return;
                    default:
                        System.Console.WriteLine("Invalid option. Please try again.");
                        break;
                }               
            }
        }
    }
}