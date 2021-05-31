using PricesComparationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    interface IShopRepository
    {
        Task<Shop> Create(Shop shop);
        Task<Shop> FindById(int id);
        Task<Shop> Update(Shop shop);
        Task<List<Shop>> FindAll();
        Task Delete(int id);
        bool Exists(int id);
    }
}
