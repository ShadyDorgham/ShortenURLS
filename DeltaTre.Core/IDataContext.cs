using DeltaTre.Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DeltaTre.Core
{
    public interface IDataContext
    {
        DbSet<ShortUrl> ShortUrls { get; set; }
    }
}