using Microsoft.EntityFrameworkCore;
using RestaurantApp.Core.Models.Common;
using RestaurantApp.DAL.Data;
using RestaurantApp.DAL.Repositories.Interfaces;

namespace RestaurantApp.DAL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public DbSet<T> Table { get; set; }
        
        public Repository()
        {
            _restaurantDbContext = new();
            Table = _restaurantDbContext.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Table.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Table.AsNoTracking().ToListAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            Table.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            T? entityToDelete = await Table.FindAsync(id);
            if (entityToDelete != null)
                Table.Remove(entityToDelete);
        }

        public async Task SaveChangesAsync()
        {
            await _restaurantDbContext.SaveChangesAsync();
        }
    }
}
