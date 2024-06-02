using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceRestApi.Models
{
    public class Product
    {
        public Product(string productCode, string productName, string productDescription, decimal price, int stockQuantity, DateTime saleDate, int adminId)
        {
            ProductCode = productCode;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            StockQuantity = stockQuantity;
            SaleDate = saleDate;
            AdminId = adminId;
            OrderProducts = new List<OrderProduct>();
        }

        [Key]
        public int Id { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime SaleDate { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}

