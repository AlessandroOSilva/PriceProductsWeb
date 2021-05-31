using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricesComparationWeb.Models
{
    public class ProductShop
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime PriceDate { get; set; }
        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public ICollection<PriceRecord> Records { get; set; } = new List<PriceRecord>();

        public ProductShop(int id, double price, Shop shop, Product product)
        {
            Id = id;
            Price = price;
            PriceDate = DateTime.Now;
            Shop = shop;
            Product = product;
        }

        public ProductShop()
        {
        }

        public void AddRecord(PriceRecord pr)
        {
            Records.Add(pr);
        }

    }
}