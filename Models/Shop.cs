using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricesComparationWeb.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public ICollection<ProductShop> Products { get; set; } = new List<ProductShop>();

        public Shop()
        {
        }

        public Shop(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public void AddProduct(ProductShop p)
        {
            Products.Add(p);
        }
    }
}
