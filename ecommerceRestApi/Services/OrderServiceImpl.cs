using ecommerceRestApi.Data;
using ecommerceRestApi.Exceptions;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;
using ecommerceRestApi.Services;


public class OrderServiceImpl : OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateOrder(OrderDTO orderDTO)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Order nesnesini başlatma
                var order = new Order
                (
                    orderDTO.Name,
                    orderDTO.Surname,
                    orderDTO.PhoneNumber,
                    orderDTO.Address
                );

                foreach (var productId in orderDTO.ProductIds)
                {
                    var product = await _context.Products.FindAsync(productId);
                    if (product != null)
                    {
                        // Sipariş edilen ürün sayısını azalt
                        if (product.StockQuantity >= 1)
                        {
                            product.StockQuantity -= 1;
                            var orderProduct = new OrderProduct
                            {
                                Order = order,
                                Product = product
                            };
                            _context.OrderProducts.Add(orderProduct);
                        }
                        else
                        {
                            throw new OrderException($"Insufficient stock for product ID {productId}");
                        }
                    }
                    else
                    {
                        throw new OrderException($"Product with ID {productId} not found");
                    }
                }

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}