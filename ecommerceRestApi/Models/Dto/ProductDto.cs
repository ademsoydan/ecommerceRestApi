using System;
namespace ecommerceRestApi.Models.Dto
{
	public class ProductDto
	{
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime SaleDate { get; set; }
        public string AdminName { get; set; }

    }
}

