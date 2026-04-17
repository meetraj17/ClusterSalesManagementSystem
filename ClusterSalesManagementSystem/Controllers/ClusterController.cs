using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClusterSalesManagementSystem.Controllers
{
    [Authorize]
    public class ClusterController : Controller
    {
        private readonly IRepository<Cluster> repo;

        public ClusterController(IRepository<Cluster> repo, SalesDbContext context)
        {
            this.repo = repo;
            Context = context;
        }

        public SalesDbContext Context { get; }

        public async Task<IActionResult> ClusterList()
        {
            var data = await repo.GetAllWithUser();
            return View(data);
        }
        public async Task<IActionResult> CreateCluster(int id)
        {
            if (id == 0)
                return View(new Cluster());

            var data = await Context.Clusters.Include(x => x.User).FirstOrDefaultAsync(x => x.CluId == id);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCluster(Cluster Clu)
        {
            if (Clu.CluId == 0)
            {
                await repo.Add(Clu);
            }
            else
            {
                await repo.Update(Clu);
            }
            return RedirectToAction("ClusterList");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await repo.Delete(id);
            TempData["DeleteMessage"] = "Record Delete Successfully";
            return RedirectToAction("ClusterList");
        }
        [HttpGet]
        public JsonResult SearchUsers(string term)
        {
            var users = Context.Users
                .Where(x => x.UserRole == "User" && x.UserName.Contains(term))
                .Select(x => new
                {
                    label = x.UserName,  // shown in dropdown
                    value = x.UserName,
                    id = x.UserId// stored
                })
                .ToList();

            return Json(users);
        }

    }
}
