using System;
namespace ecommerceRestApi.Exceptions
{
	public class AuthException : Exception
	{
        public AuthException(string message) : base(message)
        {
        }
    }
}

