using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PricesComparationWeb.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public Brand()
        {
        }

        public Brand(int id, string name, List<Product> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }


    }

}
