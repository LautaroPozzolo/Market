using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Market.Model;
using Market.Services.Interfaces;
using System.Data.Entity;

namespace Market.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _service;

        public ProductsController(IProductServices service)
        {
            this._service = service;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return await _service.GetAll() != null ? 
                          View(await _service.GetAll()) :
                          Problem("Entity is null.");
        }

        // GET: Products/Detail/5
        public async Task<IActionResult> Detail(long? id)
        {
            if (id == null)
            {
                throw new Exception("The Id is not valid, be sure that is a numeric value");
            }

            var product = await _service.ProductGetById(id);

            if (product == null)
            {
                throw new Exception("The ID: " + id + "was not found");
            }

            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Pictures,State")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _service.Create(product);
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Description,Pictures,State")] Product product)
        {
            if (id != product.Id)
            {
                throw new Exception("The ID: " + id + "was not found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(product);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                throw new Exception("The Id is not valid, be sure that is a numeric value");
            }

            var product = await _service.ProductGetById(id);

            if (product == null)
            {
                throw new Exception("The ID: " + id + "was not found");
            }

            try
            {
                await _service.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
