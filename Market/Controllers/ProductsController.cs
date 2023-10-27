using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Market.Model;
using Market.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Market.Helpers;
using Market.Providers;

namespace Market.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductServices _service;
        private readonly HelperUploadFiles _HelperUploadFiles;
        public ProductsController(IProductServices service, HelperUploadFiles helperUploadFiles)
        {
            this._service = service;
            this._HelperUploadFiles= helperUploadFiles;
        }

        // GET: Products
        //Note: the name 'image' and 'location' must be the same of the names in the html
        // eg: in this case Edit.html in the form for edit Pictures we have the same names for both
        public async Task<IActionResult> Index()
        {
            return await _service.GetAll() != null ? 
                          View(await _service.GetAll()) :
                          Problem("Entity is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(long? id)
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

        public IActionResult Create()
        {
            return View();
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

        public async Task<IActionResult> Edit(long id)
        {

            var product = await _service.ProductGetById(id);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Description,Pictures,State")] Product product, IFormFile image)
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

        public async Task<IActionResult> Delete(long? id)
        {
            var product = await _service.ProductGetById(id);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
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
