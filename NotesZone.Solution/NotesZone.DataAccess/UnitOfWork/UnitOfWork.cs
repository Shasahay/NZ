using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.UnitOfWork
{
    using NotesZone.DataAccess;
    using System.Data.Entity;
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly NotesZoneDBContext context = new NotesZoneDBContext();

        public NotesZoneDBContext Context
        {
            get { return context; }
        }

        public UnitOfWork()
        {
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

        public void Undo()
        {
            this.context.Dispose();
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.context.Set<T>();
        }

        public void Dispose()
        {
            this.context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
