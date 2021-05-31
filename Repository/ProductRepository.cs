using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;


        public ProductRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            var result = _context.Brand.FirstOrDefault(i => i.Id.Equals(product.Brand.Id));
            try
            {
                result.Products.Add(product);
                await _context.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
            return product;
        }

        public async Task Delete(int id)
        {
            var result = _context.Product.FirstOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Product.Remove(result);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool Exists(int id)
        {
            var result = _context.Product.Find(id);
            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<Product>> FindAll()
        {
            var result = await _context.Product.Include(b => b.Brand).OrderBy(n => n.Name).ToListAsync();
            return result;
        }

        public async Task<Product> FindById(int id)
        {
            var result = await _context.Product.Include(p => p.Brand).FirstOrDefaultAsync(i => i.Id.Equals(id));
            return result;
        }

        public async Task<Product> Update(Product brand)
        {
            if (brand != null)
            {
                try
                {
                    _context.Product.Update(brand);
                    await _context.SaveChangesAsync();
                    return brand;
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
