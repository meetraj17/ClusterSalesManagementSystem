using ClusterSalesManagementSystem.Models;

namespace ClusterSalesManagementSystem.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<List<Cluster>> GetAllWithUser();

    }
}
