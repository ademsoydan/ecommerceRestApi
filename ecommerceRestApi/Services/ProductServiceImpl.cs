using System;
using ecommerceRestApi.Data;
using ecommerceRestApi.Exceptions;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace ecommerceRestApi.Services
{
    public class ProductServiceImpl : ProductService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductServiceImpl(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public bool AddProduct(AddProductRequest request)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(x => x.Id == request.AdminId);
            if (admin == null)
                throw new NotFoundException("Bu id ile bir admin yok");

            Product product = new Product(request.ProductCode, request.ProductName, request.ProductDescription, request.Price, request.StockQuantity, request.SaleDate, request.AdminId);
            try
            {
                applicationDbContext.Products.Add(product);
                applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<ProductDto> getAllProducts()
        {
            DateTime today = DateTime.Today;
            List<ProductDto> productDtos = applicationDbContext.Products
            .Where(p => p.SaleDate < today) 
            .Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SaleDate = p.SaleDate,
                AdminName = p.Admin.Username
            }).ToList();
            return productDtos;
        }

        public List<Product> getProductsByAdminId(int adminId)
        {
            List<Product> products = applicationDbContext.Products.Where(product => product.AdminId == adminId).ToList();
            return products;
        }

        public List<ProductDto> GetProductsByIds(List<int> ids)
        {
            List<Product> products = applicationDbContext.Products
        .Include(p => p.Admin) // Admin ilişkisel özelliğini yükle
        .Where(p => ids.Contains(p.Id))
        .ToList();

            List<ProductDto> productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                SaleDate = p.SaleDate,
                AdminName = p.Admin != null ? p.Admin.Username : null
            }).ToList();

            return productDtos;
        }

        public bool haveStock(int productId)
        {
            Product product = applicationDbContext.Products.FirstOrDefault(product => product.Id == productId);
            if (product != null && product.StockQuantity > 0)
                return true;

            return false;
        }
    }
}

