using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerceRestApi.Models
{
	public class Admin
	{
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ICollection<Product> Products { get; set; }

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }


    }
}

