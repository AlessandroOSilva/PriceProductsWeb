using Microsoft.EntityFrameworkCore;
using PricesComparationWeb.Models;
using PricesComparationWeb.Models.Context;
using PricesComparationWeb.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MySqlContext _context;

        public BrandRepository(MySqlContext context)
        {
            _context = context;
        }

        public async Task<Brand> Create(Brand brand)
        {
            var result = await _context.Brand.SingleOrDefaultAsync(i => i.Id.Equals(brand.Id) && i.Name.Equals(brand.Name));

            if (result == null)
            {
                try
                {
                    _context.Brand.Add(brand);
                    _context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }

            return brand;
        }

        public async Task Delete(int id)
        {
            var result = await _context.Brand.Include(p => p.Products).FirstOrDefaultAsync(i => i.Id == id);

            if (result != null)
            {
                if (result.Products != null)
                {
                    foreach (var p in result.Products)
                    {
                        _context.Product.Remove(p);
                    }
                }
                _context.Brand.Remove(result);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Brand>> FindAll()
        {
            var result = await _context.Brand.Include(p => p.Products).OrderBy(n => n.Name).ToListAsync();

            return result;
        }


        public async Task<Brand> FindById(int id)
        {
            var result = await _context.Brand.Include(p => p.Products).FirstOrDefaultAsync(i => i.Id == id);

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Brand> Update(Brand brand)
        {

            if (brand != null)
            {
                try
                {
                    _context.Update(brand);
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

        public bool Exists(int id)
        {
            var result = _context.Brand.Find(id);
            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
