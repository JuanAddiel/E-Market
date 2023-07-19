using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T>Add (T entity);
        Task Update (T entity);
        Task Delete (T entity);
        Task<T> GetByIdAsync(int Id);
        Task<List<T>> GetAll ();
        Task<List<T>> GetAllWithInclude (List<string> properties);
    }
}
