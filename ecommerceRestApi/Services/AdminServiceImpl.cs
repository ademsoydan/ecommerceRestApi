using System;
using ecommerceRestApi.Data;
using ecommerceRestApi.Models;

namespace ecommerceRestApi.Services
{
    public class AdminServiceImpl : AdminService
	{
        ApplicationDbContext applicationDbContext;

        public AdminServiceImpl(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public bool AddAdmin(AdminViewModel admin)
        {
            try
            {
                applicationDbContext.Admins.Add(new Admin(admin.Username, admin.Password));
                applicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Admin GetAdminById(int adminId)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(a => a.Id == adminId);
            return admin;
        }

        public Admin GetAdminByUsername(string username)
        {
            Admin admin = applicationDbContext.Admins.FirstOrDefault(a => a.Username == username);
            return admin;
        }

        public List<Admin> GetAdmins()
        {
            return applicationDbContext.Admins.ToList();
        }
    }
}

