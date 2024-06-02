using System;
namespace ecommerceRestApi.Exceptions
{
	public class CanNotBeNullException : Exception
	{
        public CanNotBeNullException(string message) : base(message)
        {
        }
    }
}

