using Adroit.Services.TinyUrl.Repository.Interfaces;
using Adroit.Services.TinyUrl.UrlGenerator.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace Adroit.Services.TinyUrl.UrlGenerator
{
    public class TinyUrlGenerator : ITinyUrlGenerator
    {
        private readonly Random random;
        public TinyUrlGenerator()
        {
            this.random = new Random();          
        }
        public string GenerateRandomShortUrl(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var shortUrlChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                shortUrlChars[i] = chars[this.random.Next(chars.Length)];
            }

            return new string(shortUrlChars);
        }
    }
}
