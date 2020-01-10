using System;
using System.Threading.Tasks;

namespace DeltaTre.Core.IHelpers
{
    public interface IUtilities
    {  Task<bool> IsValidUri(string uri);
        string GetRandomUrl();
        int MonthsDifferenceBetweenTwoDates(DateTime start, DateTime end);
        Task<string> GetTheResponseFromTheRequestedUrl(string url);
    }
}