using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Repository;
using PricesComparationWeb.Repository.Interface;

namespace PricesComparationWeb.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _repository;

        public BrandsController(BrandRepository repository)
        {
            _repository = repository;
        }

        // GET: Brands
        public async Task<IActionResult> Index(string name)
        {
            var result = await _repository.FindAll();

            if (!String.IsNullOrEmpty(name))
            {
                result = result.FindAll(p => p.Name.ToUpper().Contains(name.ToUpper())).OrderBy(n => n.Name).ToList();
            }

            return View(result);

        }

        // GET: Brands/Details/5
        [Route("Brands/Details/{id?}")]
        public async Task<IActionResult> Details(int id)
        {
            var brand = await _repository.FindById(id);

            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandId,Name")] Brand brand)
        {
            var result = _repository.Equals(brand);

            if (ModelState.IsValid && result != true)
            {
                await _repository.Create(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var brand = await _repository.FindById(id);

            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Brand brand)
        {

            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(brand);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(BrandsController.Index));
            }
            return RedirectToAction(nameof(BrandsController.Index));

        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _repository.FindById(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
