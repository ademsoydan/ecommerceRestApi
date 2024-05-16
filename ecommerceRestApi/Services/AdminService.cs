using System;
using ecommerceRestApi.Models;

namespace ecommerceRestApi.Services
{
	public interface AdminService
	{
		List<Admin> GetAdmins();
		Admin GetAdminById(int adminId);
		bool AddAdmin(AdminViewModel admin);
		Admin GetAdminByUsername(string username);

    }
}

