using PricesComparationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    interface IProductShopRepository
    {
        Task<ProductShop> Create(ProductShop ps);
        Task<ProductShop> FindById(int id);
        Task<ProductShop> Update(ProductShop ps);
        Task<List<ProductShop>> FindAll();
        Task Delete(int id);
        bool Exists(int id);
    }
}
