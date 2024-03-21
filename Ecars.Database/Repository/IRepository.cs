using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Database.Repository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> GetAllAsyncById(int id);
        Task SaveAsync();
        Task UpdateASync(T entity);
    }
}
