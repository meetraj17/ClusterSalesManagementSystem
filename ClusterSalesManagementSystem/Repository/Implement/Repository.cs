using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ClusterSalesManagementSystem.Repository.Implement
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SalesDbContext context;

        public Repository(SalesDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            var data = await context.Set<T>().FindAsync(id);
            return data;
        }
        public async Task Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var data = await GetById(id);
            context.Set<T>().Remove(data);
            await context.SaveChangesAsync();
        }
        public async Task<List<Cluster>> GetAllWithUser()
        {
            return await context.Clusters
                .Include(x => x.User)
                .ToListAsync();
        }
        public async Task<List<Sales>> GetAllWithCluster()
        {
            return await context.Sales
                .Include(x => x.cluster)
                .ToListAsync();
        }
    }
}
