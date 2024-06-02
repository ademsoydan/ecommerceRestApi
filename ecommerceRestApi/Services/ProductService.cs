using System;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;

namespace ecommerceRestApi.Services
{
	public interface ProductService
	{
		bool AddProduct(AddProductRequest request);
		List<Product> getProductsByAdminId(int adminId);
        List<ProductDto> getAllProducts();
		bool haveStock(int productId);
        List<ProductDto> GetProductsByIds(List<int> ids);
    }
}