using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using PricesComparationWeb.Repository.Interface;

namespace PricesComparationWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _repository;
        private readonly MySqlContext _context;

        public ProductsController(ProductRepository repository, MySqlContext context)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Products
        public async Task<IActionResult> Index(string name)
        {
            var result = await _repository.FindAll();

            if (!String.IsNullOrEmpty(name))
            {
                result = result.FindAll(p => p.Name.ToUpper().Contains(name.ToUpper())).OrderBy(n => n.Name).ToList();
            }

            return View(result);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brand.Select(c => new SelectListItem()
            { Text = c.Name, Value = c.Id.ToString()}).OrderBy(n => n.Text);

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Typed,Brand")] Product product)
        {

            if (ModelState.IsValid)
            {

                await _repository.Create(product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Brands = _context.Brand.Select(c => new SelectListItem()
            { Text = c.Name, Value = c.Name }).OrderBy(n => n.Value).ToList();

            var product = await _repository.FindById(id);
            if (product != null)
            {
                await _repository.Update(product);
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Typed")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
