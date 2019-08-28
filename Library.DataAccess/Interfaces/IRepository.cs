using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id); 
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
        
    }
}
