using System;
using System.Collections.Generic;

namespace ecommerceRestApi.Models.Dto
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            ProductIds = new List<int>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<int> ProductIds { get; set; }
    }
}