using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DeltaTre.Core.IRepositories;

namespace DeltaTre.Core
{
    public interface IUnitOfWork
    {
        IShortUrlRepository ShortUrlRepository { get; }
        void Dispose();
        void RefreshAll();
        void Add<T>(T entity) where T : class;
        void AddRange<T>(List<T> listOfEntity) where T : class;
        List<T> Search<T>(Expression<Func<T, bool>> predicate) where T : class;
        void DeleteRange<T>(List<T> listOfEntity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Complete();
    }
}