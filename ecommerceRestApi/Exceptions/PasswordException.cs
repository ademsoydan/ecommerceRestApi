using System;
namespace ecommerceRestApi.Exceptions
{
	public class PasswordException : Exception
	{
        public PasswordException(string message) : base(message)
        {
        }
    }
}

