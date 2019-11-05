using System.Collections.Generic;
using System.Linq;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Library.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected MagazineContext _context;
        protected DbSet<T> _entities;

        public Repository(MagazineContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(int Id)
        {
            return _entities.SingleOrDefault(s => s.Id == Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public void Insert(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }     

    }
}
