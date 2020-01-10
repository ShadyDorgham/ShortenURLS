using DeltaTre.Core;
using DeltaTre.Core.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DeltaTre.Persistence
{

    public class DataContext: DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ShortUrl>()
                .HasIndex(u => u.ShortenUrl)
                .IsUnique();
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
