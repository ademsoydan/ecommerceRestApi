using System;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;

namespace ecommerceRestApi.Services
{
	public interface AdminService
	{
		List<Admin> GetAdmins();
		Admin GetAdminById(int adminId);
		bool AddAdmin(AdminViewModel admin);
		Admin GetAdminByUsername(string username);
		AdminDto Login(AdminViewModel admin);
		bool ValidateToken(string token);
    }
}

