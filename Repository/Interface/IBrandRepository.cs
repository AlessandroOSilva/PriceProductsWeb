using PricesComparationWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PricesComparationWeb.Repository.Interface
{
    public interface IBrandRepository
    {
        Task<Brand> Create(Brand brand);
        Task<Brand> FindById(int id);
        Task<Brand> Update(Brand brand);
        Task<List<Brand>> FindAll();
        Task Delete(int id);
        bool Exists(int id);
    }
}
