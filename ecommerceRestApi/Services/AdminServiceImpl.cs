using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ecommerceRestApi.Data;
using ecommerceRestApi.Exceptions;
using ecommerceRestApi.Models;
using ecommerceRestApi.Models.Dto;
using ecommerceRestApi.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ecommerceRestApi.Services
{
    public class AdminServiceImpl : AdminService
	{
        private readonly ApplicationDbContext applicationDbContext;

        private readonly IConfiguration _configuration;

        public AdminServiceImpl(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            this.applicationDbContext = applicationDbContext;
        }

        public bool AddAdmin(AdminViewModel admin)
        {
            if (String.IsNullOrEmpty(admin.Password) || String.IsNullOrEmpty(admin.Username))
                throw new CanNotBeNullException("Kullancı adı veya şifre boş olamaz");
            if (admin.Password.Length < 8)
                throw new PasswordException("Şifre 8 karakterden kısa olamaz");
            if (!Utils.ContainsLetterAndDigit(admin.Password))
                throw new PasswordException("Şifre rakam veya harf içermelidir");
            if (GetAdminByUsername(admin.Username) != null)
                throw new PasswordException("Bu kullanıcı adı daha önce alınmış");

            try
            {
                string hashedPassword = PasswordHasher.HashPassword(admin.Password);
                applicationDbContext.Admins.Add(new Admin(admin.Username, hashedPassword));
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

        public AdminDto Login(AdminViewModel admin)
        {
            Admin dbAdmin = GetAdminByUsername(admin.Username);
            string password = admin.Password;
            
            if(dbAdmin == null || !PasswordHasher.VerifyHashedPassword(dbAdmin.Password, password))
                throw new AuthException("Şifre veya Kullancı Adı hatalı");
            return new AdminDto(dbAdmin.Id, dbAdmin.Username, GenerateJwtToken(admin));
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            // Token'ı doğrula
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateJwtToken(AdminViewModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}