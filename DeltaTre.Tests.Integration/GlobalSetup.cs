using DeltaTre.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DeltaTre.Tests.Integration
{
    [SetUpFixture]
    public class GlobalSetup 
    {
        private DataContext _db;

        [OneTimeSetUp]
        public void SetUp()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlite("Data Source=./ShortUrlsDeltaTre.db");

            _db = new DataContext(builder.Options);
            _db.Database.Migrate();
        }

    }
}
