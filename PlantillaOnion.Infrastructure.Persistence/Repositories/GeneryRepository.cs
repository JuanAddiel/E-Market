using Microsoft.EntityFrameworkCore;
using PlantillaOnion.Core.Application.Interface.Repository;
using PlantillaOnion.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaOnion.Infrastructure.Persistence.Repositories
{
    public class GeneryRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PlantillaOnionContext _context;
        public GeneryRepository(PlantillaOnionContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id));
            await _context.SaveChangesAsync();
            return _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var propertie in properties)
            {
                query = query.Include(propertie);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);  
        }

        public async Task<T> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
