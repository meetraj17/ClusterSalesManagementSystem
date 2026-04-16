using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ClusterSalesManagementSystem.Controllers
{
    public class ClusterController : Controller
    {
        private readonly IRepository<Cluster> repo;

        public ClusterController(IRepository<Cluster> repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> ClusterList()
        {
            var data=await repo.GetAll();
            return View(data);
        }

    }
}
