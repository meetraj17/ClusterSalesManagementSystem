using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;

namespace ClusterSalesManagementSystem.Services
{
    public class UserService
    {
        private readonly IRepository<User> repo;
        private readonly PasswordService passwordService;

        public UserService(IRepository<User> repo,PasswordService passwordService)
        {
            this.repo = repo;
            this.passwordService = passwordService;
        }
        public async Task Register(User user,string password)
        {
            user.PasswordHash= passwordService.HashPassword(password);
            await repo.Add(user);
        }
    }
}
