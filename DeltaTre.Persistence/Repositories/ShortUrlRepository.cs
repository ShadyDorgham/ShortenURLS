using DeltaTre.Core;
using DeltaTre.Core.DomainModels;
using DeltaTre.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaTre.Persistence.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
   {
        private readonly IDataContext _ctx;
        public ShortUrlRepository(IDataContext ctx)
        {
            _ctx = ctx;
        }

        
        public List<ShortUrl> GetValidUrls(int numberOfValidMonths)
        {
            var validUrls = (from shUrls in _ctx.ShortUrls
                where shUrls.InsertDate >= DateTime.Now.Date.AddMonths(-numberOfValidMonths)
                select shUrls).OrderByDescending(x => x.Id).ToList();
            return validUrls;
        }


        public bool IsShortUrlExists(string shortUrl)
        {
            return _ctx.ShortUrls.Any(ur => ur.ShortenUrl == shortUrl);
        }

        public ShortUrl GetByShortKey(string shortUrl)
        {
            return _ctx.ShortUrls.SingleOrDefault(x => x.ShortenUrl == shortUrl);
        }
    }
}
