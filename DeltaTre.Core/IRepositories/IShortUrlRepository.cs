using System.Collections.Generic;
using DeltaTre.Core.DomainModels;

namespace DeltaTre.Core.IRepositories
{
    public interface IShortUrlRepository
    {
        List<ShortUrl> GetValidUrls(int numberOfValidMonths);
        bool IsShortUrlExists(string shortUrl);
        ShortUrl GetByShortKey(string shortUrl);
    }
}