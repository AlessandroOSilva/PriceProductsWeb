using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    public class ProductShopRepository : IProductShopRepository
    {

        private readonly MySqlContext _context;

        public ProductShopRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<ProductShop> Create(ProductShop ps)
        {
            var product = await _context.Product.Include(b => b.Brand).SingleOrDefaultAsync(p => p.Id.Equals(ps.Product.Id));

            try
            {
                product.Products.Add(ps);

                ps.Shop = _context.Shop.Include(a => a.Address).SingleOrDefault(s => s.Id.Equals(ps.Shop.Id));

                PriceRecord record = new()
                {
                    ProductShop = ps,
                    Price = ps.Price,
                    Date = DateTime.Now
                };

                ps.AddRecord(record);

                _context.SaveChanges();

                return ps;
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var result = await _context.ProductShops.FindAsync(id);

            if (result != null)
            {
                _context.ProductShops.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            var result = _context.ProductShops.FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (result != null)
            {
                return true;
            }
            else return false;
        }

        public async Task<List<ProductShop>> FindAll()
        {
            var result = _context.ProductShops.Include(b => b.Product.Brand).Include(s => s.Records).Include(b => b.Product).ToListAsync();

            return await result;
        }

        public async Task<ProductShop> FindById(int id)
        {
            var result = _context.ProductShops.Include(p => p.Product).Include(s => s.Records).Include(b => b.Product.Brand).FirstOrDefaultAsync(i => i.Id.Equals(id));

            return await result;
        }

        public async Task<ProductShop> Update(ProductShop ps)
        {
            if (ps != null)
            {
                PriceRecord record = new()
                {
                    ProductShop = ps,
                    Price = ps.Price,
                    Date = DateTime.Now
                };

                ps.Records.Add(record);

                _context.ProductShops.Update(ps);

                try
                {
                    await _context.SaveChangesAsync();
                    return ps;
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
