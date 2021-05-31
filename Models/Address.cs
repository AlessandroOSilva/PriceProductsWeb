using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PricesComparationWeb.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Address()
        {
        }

        public Address(int id, string street, string district, string city, string state)
        {
            Id = id;
            Street = street;
            District = district;
            City = city;
            State = state;
        }
    }
}