using DeltaTre.Core.IHelpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeltaTre.Persistence.Helpers
{
    public class Utilities : IUtilities
    {
        public async Task<bool> IsValidUri(string uri)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(uri);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }



        public async Task<string> GetTheResponseFromTheRequestedUrl(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return response.RequestMessage.RequestUri.ToString();
            }

            return null;
        }

        public string GetRandomUrl()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public int MonthsDifferenceBetweenTwoDates(DateTime start, DateTime end)
        {
            return (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
        }

    }
}
