using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Database.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var std = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(std);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAllAsyncById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateASync(T entity)
        {
            _context.Update(entity);
            await SaveAsync();
        }
    }
}
