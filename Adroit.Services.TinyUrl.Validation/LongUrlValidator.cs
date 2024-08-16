using Adroit.Services.TinyUrl.Validation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adroit.Services.TinyUrl.Validation
{
    public class LongUrlValidator
    {
        /// <summary>
        /// TODO - Adding more validations for long url like type of characters supported, max length of the url
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        public List<ValidationResult> Validate(string longUrl)
        {
            var result = new List<ValidationResult>();
            if (string.IsNullOrEmpty(longUrl))
            {
                result.Add(new ValidationResult() { Message = "Long Url can not be empty.", Type = ValidationResultType.ERROR });
            }
            if (!Uri.IsWellFormedUriString(longUrl, UriKind.RelativeOrAbsolute))
            {
                result.Add(new ValidationResult() { Message = $"Long Url not in valid format.", Type = ValidationResultType.ERROR });
            }
            return result;
        }
    }
}
