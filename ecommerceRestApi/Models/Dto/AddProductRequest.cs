using System;
namespace ecommerceRestApi.Models.Dto
{
	public class AddProductRequest
	{
        public int AdminId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}

