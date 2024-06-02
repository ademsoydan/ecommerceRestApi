using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceRestApi.Models
{
    public class Order
    {
        public Order(string name, string surname, string phoneNumber, string address)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Address = address;
            OrderProducts = new List<OrderProduct>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}

