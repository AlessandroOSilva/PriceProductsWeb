using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricesComparationWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Typed { get; set; }
        [ForeignKey("BrandId")]
        
        public Brand Brand { get; set; }

        public ICollection<ProductShop> Products = new List<ProductShop>();

        public Product()
        {
        }

        public Product(int id, string name, string typed, Brand brand)
        {
            Id = id;
            Name = name;
            Typed = typed;
            Brand = brand;
        }
    }
}