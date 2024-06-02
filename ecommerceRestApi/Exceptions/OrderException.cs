using System;
namespace ecommerceRestApi.Exceptions
{
	public class OrderException : Exception
	{
        public OrderException(string message) : base(message)
        {
        }
    }
}

