using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Linq;
using DeltaTre.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeltaTre.Tests.Integration
{
    public class Isolated : Attribute, ITestAction
    {
        //private TransactionScope _transactionScope;
        private DataContext _context;
        public void BeforeTest(ITest test)
        {
            //_transactionScope = new TransactionScope(TransactionScopeOption.Required,
            //    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }, TransactionScopeAsyncFlowOption.Enabled);

            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlite("Data Source=./ShortUrlsDeltaTre.db");

            _context = new DataContext(builder.Options);

        }

        public void AfterTest(ITest test)
        {
            //_transactionScope.Dispose();
            _context.ShortUrls.RemoveRange(_context.ShortUrls.ToList());
            _context.SaveChanges();
        }

        public ActionTargets Targets => ActionTargets.Test;
    }
}
