using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClusterSalesManagementSystem.Controllers
{
    [Authorize]
    public class SalesController :Controller
    {
        private readonly IRepository<Sales> repo;

        public SalesController(IRepository<Sales> repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> SalesList()
        {
            var data = await repo.GetAll();
            return View(data);
        }      
        public IActionResult Create()
        {
            return View();
        }  
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sales model, IFormFile file)
        {
            var path = Path.Combine("wwwroot/Images", file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            model.InvoiceUrl = "/Images/" + file.FileName;

            await repo.Add(model);

            return RedirectToAction("SalesList");
        }
    }
}
