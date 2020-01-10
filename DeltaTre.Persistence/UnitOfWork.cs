using DeltaTre.Core.IRepositories;
using DeltaTre.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DeltaTre.Core;

namespace DeltaTre.Persistence
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _ctx;
        public IShortUrlRepository ShortUrlRepository { get; private set; }


        public UnitOfWork( DataContext ctx)
        {
            _ctx = ctx;
            ShortUrlRepository = new ShortUrlRepository(_ctx);
        }




        public void Dispose()
        {
            _ctx.Dispose();
        }
        public void RefreshAll()
        {
            foreach (var entity in _ctx.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        public void Add<T>(T entity) where T : class
        {
            _ctx.Set<T>().Add(entity);
        }


        public void AddRange<T>(List<T> listOfEntity) where T : class
        {
            _ctx.Set<T>().AddRange(listOfEntity);
        }




        public List<T> Search<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate == null)
                return _ctx.Set<T>().ToList();


            return _ctx.Set<T>().Where(predicate).ToList();
        }

        public void DeleteRange<T>(List<T> listOfEntity) where T : class
        {
            _ctx.Set<T>().RemoveRange(listOfEntity);
        }

        
        public void Delete<T>(T entity) where T : class
        {
            _ctx.Set<T>().Remove(entity);
        }

        public void Complete()
        {
            _ctx.SaveChanges();
        }
    }
}
