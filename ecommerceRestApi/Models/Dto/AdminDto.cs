using System;
namespace ecommerceRestApi.Models.Dto
{
	public class AdminDto
	{
        public AdminDto(int id, string username, string token)
        {
            Id = id;
            Username = username;
            Token = token;
        } 

        public int Id { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}