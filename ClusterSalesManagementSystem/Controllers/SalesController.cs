using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClusterSalesManagementSystem.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly IRepository<Sales> repo;

        public SalesDbContext Context { get; }

        public SalesController(IRepository<Sales> repo, SalesDbContext context)
        {
            this.repo = repo;
            Context = context;
        }

        public async Task<IActionResult> SalesList()
        {
            var data = await repo.GetAllWithCluster();
            return View(data);
        }
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.ClustersList = Context.Clusters.ToList();
            if (id == 0)
                return View(new Sales());

            var data = await Context.Sales.Include(x => x.cluster).FirstOrDefaultAsync(x => x.SalesId == id);

            return View(data);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sales model, IFormFile file)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");

            if (model.SalesId != 0) 
            {
                //var existingData = await repo.GetById(model.SalesId);

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    if (!string.IsNullOrEmpty(model.InvoiceUrl))
                    {
                        var oldPath = Path.Combine("wwwroot/Images", model.InvoiceUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.InvoiceUrl = "/Images/" + fileName;
                }

                await repo.Update(model); 
            }
            else
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.InvoiceUrl = "/Images/" + fileName;
                }

                await repo.Add(model);
            }

            return RedirectToAction("SalesList");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await repo.Delete(id);
            TempData["DeleteMessage"] = "Record Delete Successfully";
            return RedirectToAction("SalesList");
        }
    }
}
