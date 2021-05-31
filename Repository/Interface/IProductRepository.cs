using PricesComparationWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product> Create(Product product);
        Task<Product> FindById(int id);
        Task<Product> Update(Product brand);
        Task<List<Product>> FindAll();
        Task Delete(int id);
        bool Exists(int id);
    }
}
