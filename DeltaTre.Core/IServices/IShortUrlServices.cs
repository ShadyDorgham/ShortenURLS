using System.Collections.Generic;
using DeltaTre.Core.DomainModels;
using DeltaTre.Core.VM;

namespace DeltaTre.Core.IServices
{
    public interface IShortUrlServices
    {
        string SaveUrl(string originalUrl);
        List<ShortUrl> GetValidUrls(int numberOfValidMonths);
        ResponseResultVm GetTheResponseUrl(string shortenUrl, int numberOfMonthsValid);
    }
}