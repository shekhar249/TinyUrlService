using Adroit.Services.TinyUrl.Validation.Model;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Adroit.Services.TinyUrl.Validation
{
    public class ShortUrlValidator
    {
        private readonly int shortUrlLength;
        public ShortUrlValidator()
        {
            var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var objUrlLenght = configuration.GetValue(typeof(int), "AppSettings:ShortUrlLength");
            this.shortUrlLength = objUrlLenght == null ? 7 : (int)objUrlLenght;
        }
        /// <summary>
        /// TODO - Adding more validations for short url like type of characters supported
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public List<ValidationResult> Validate(string shortUrl)
        {
            var result = new List<ValidationResult>();
            if (string.IsNullOrEmpty(shortUrl))
            {
                result.Add(new ValidationResult() { Message = "Short Url can not be empty.", Type = ValidationResultType.ERROR });
            }
            if (!string.IsNullOrEmpty(shortUrl) && shortUrl.Length != this.shortUrlLength)
            {
                result.Add(new ValidationResult() { Message = $"Short Url length : {shortUrl.Length} not equal to recommended length {this.shortUrlLength}.", Type = ValidationResultType.ERROR });
            }
            return result;
        }
    }
}
