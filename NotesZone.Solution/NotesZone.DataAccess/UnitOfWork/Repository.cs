using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.UnitOfWork
{
    using NotesZone.DataAccess;
    using NotesZone.Domain;
    using System.Data.Entity;
    using System.Linq.Expressions;
    public class Repository<T> : IRepository<T> where T : EntityBase
    {

        NotesZoneDBContext _context = null;
        DbSet<T> _dbSet = null;

        public NotesZoneDBContext Context
        {
            get { return _context; }
        }

        private void ValidateIfContextIsSet()
        {
            if (this._context == null)
            {
                throw new ApplicationException("Context not set. Setup repository by passing Unit of Work using EnrollUnitOfWork");
            }
        }

        public async virtual Task<T> GetById(params object[] keyValues)
        {
            ValidateIfContextIsSet();
            return await Task.Run(() => _dbSet.Find(keyValues));
        }


        public virtual T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual T Update(T entity)
        {

            var entry = _context.Entry(entity);
            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                _dbSet.Attach(entity);
                entry = _context.Entry(entity);
            }
            entry.State = System.Data.Entity.EntityState.Modified;
            return entity;
        }

        public virtual void Delete(object[] keyValues)
        {
            var stub = _context.Load<T>(keyValues);
            _dbSet.Remove(stub);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var item in objects)
            {
                _dbSet.Remove(item);
            }
        }


        public virtual T AddOrUpdate(T entity)
        {
            _context.AddOrUpdate(entity);
            return entity;
        }

        public virtual IQueryable<T> Get
        {
            get { return _dbSet; }
        }

        public virtual IQueryable<T> GetIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void EnrollUnitOfWork(IUnitOfWork uow)
        {
            var unitOfWork = uow as UnitOfWork;
            if (unitOfWork != null) this._context = unitOfWork.Context;
            this._dbSet = _context.Set<T>();
        }
    }
}
