using System;
using ecommerceRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerceRestApi.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}

