using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using PricesComparationWeb.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly MySqlContext _context;

        public ShopRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<Shop> Create(Shop shop)
        {
            //var address = shop.Address;
            try
            {
                _context.Shop.Add(shop);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return shop;
        }

        public async Task Delete(int id)
        {
            var result = _context.Shop.FirstOrDefault(i => i.Id.Equals(id));
            _context.Shop.Remove(result);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            var result = _context.Shop.Find(id);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Shop>> FindAll()
        {
            var result = await _context.Shop.OrderBy(n => n.Name).ToListAsync();
            return result;
        }

        public async Task<Shop> FindById(int id)
        {
            var result = await _context.Shop.Include(p => p.Products).Include(a => a.Address).FirstOrDefaultAsync(p => p.Id.Equals(id));

            return result;
        }

        public async Task<Shop> Update(Shop shop)
        {
            if (shop != null)
            {
                try
                {
                    _context.Shop.Update(shop);
                    await _context.SaveChangesAsync();
                    return shop;
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
