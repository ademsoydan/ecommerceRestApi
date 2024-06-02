using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceRestApi.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}

