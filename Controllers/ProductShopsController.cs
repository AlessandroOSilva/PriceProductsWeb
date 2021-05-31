using System;
using System.Collections.Generic;
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
    public class ProductShopsController : Controller
    {
        private readonly MySqlContext _context;
        private readonly ProductShopRepository _repository;

        public ProductShopsController(MySqlContext context, ProductShopRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: ProductShops
        [Route("shops/{id}/productshops")]
        public async Task<IActionResult> Index(int id)
        {
            var result = await _context.ProductShops.Include(b => b.Product).Include(s => s.Product.Brand).Where(p => p.Shop.Id.Equals(id)).ToListAsync();

            return View(result);
        }

        [Route("/productshops/details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var productShop = await _repository.FindById(id);
            if (productShop == null)
            {
                return NotFound();
            }

            return View(productShop);
        }

        // GET: ProductShops/Create
        //[Route("shops/{id}/productshops/Create")]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Product.Select(c => new SelectListItem()
            { Text = c.Name + "- " + c.Brand.Name, Value = c.Id.ToString() }).OrderBy(n => n.Text);


            ViewBag.Shops = _context.Shop.Select(s => new SelectListItem()
            { Text = s.Name + "- " + s.Address.District, Value = s.Id.ToString() }).OrderBy(n => n.Text);


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("shops/{id}/productshops/Create")]
        public async Task<IActionResult> Create([Bind("Id,Price,Product,Shop")] ProductShop productShop)
        {

            if (ModelState.IsValid)
            {
                await _repository.Create(productShop);

                return RedirectToAction(nameof(productShop));
            }

            return View(productShop);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Products = _context.Product.Include(b => b.Brand).Select(c => new SelectListItem()
            { Text = c.Name + c.Brand.Name, Value = c.Id.ToString() }).OrderBy(n => n.Text);

            ViewBag.Shops = _context.Shop.Select(s => new SelectListItem()
            { Text = s.Name, Value = s.Id.ToString() }).OrderBy(n => n.Text);

            var productShop = await _repository.FindById(id);

            if (productShop == null)
            {
                return NotFound();
            }
            return View(productShop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/productshops/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Shop,Product")] ProductShop productShop)
        {

            if (id != productShop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _repository.Update(productShop);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductShopExists(productShop.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute(new { controller = "Shops", action = "Details", id = productShop.Id });
            }
            return View(productShop);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var productShop = await _repository.FindById(id);

            if (productShop == null)
            {
                return NotFound();
            }

            return View(productShop);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productShop = await _repository.FindById(id);
            await _repository.Delete(productShop.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductShopExists(int id)
        {
            return _context.ProductShops.Any(e => e.Id == id);
        }
    }
}
