using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using PricesComparationWeb.Repository;

namespace PricesComparationWeb.Controllers
{
    public class ShopsController : Controller
    {
        private readonly MySqlContext _context;
        private readonly ShopRepository _repository;

        public ShopsController(MySqlContext context, ShopRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Shops
        public async Task<IActionResult> Index()
        {
            return View(await _repository.FindAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var shop = await _repository.FindById(id);

            if (shop == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(ProductShopsController.Index));
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, Address")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(shop);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(shop);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shop = await _repository.FindById(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Shop shop)
        {
            if (id != shop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(shop);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopExists(shop.Id))
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
            return View(shop);
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var shop = await _repository.FindById(id);
            if (shop == null)
            {
                return NotFound();
            }

            return View(shop);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ShopExists(int id)
        {
            return _context.Shop.Any(e => e.Id == id);
        }
    }
}
