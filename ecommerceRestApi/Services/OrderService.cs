using System;
using ecommerceRestApi.Models.Dto;

namespace ecommerceRestApi.Services
{
	public interface OrderService
	{
        Task<int> CreateOrder(OrderDTO orderDTO);
    }
}

