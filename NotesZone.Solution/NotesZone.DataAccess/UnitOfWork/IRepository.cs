using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.UnitOfWork
{
    using NotesZone.Domain;
    using System.Linq.Expressions;
    public interface IRepository<T> where T : EntityBase
    {
        void EnrollUnitOfWork(IUnitOfWork uow);
        Task<T> GetById(params object[] keyValues);
        T Add(T entity);
        T Update(T entity);
        void Delete(object[] keyValues);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        T AddOrUpdate(T entity);
        IQueryable<T> Get { get; }
        IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}
