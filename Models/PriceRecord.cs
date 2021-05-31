using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricesComparationWeb.Models
{
    public class PriceRecord
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ProductShopId")]
        public virtual ProductShop ProductShop { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public PriceRecord()
        {
        }

        public PriceRecord(int id, ProductShop productShop, double price, DateTime date)
        {
            Id = id;
            ProductShop = productShop;
            Price = price;
            Date = date;
        }
    }
}
