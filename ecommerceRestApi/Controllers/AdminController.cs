using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceRestApi.Models;
using ecommerceRestApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ecommerceRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }
    // GET: api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

   
    [HttpPost("register")]
    public IActionResult Register([FromBody] AdminViewModel adminViewModel)
    {
        _adminService.AddAdmin(adminViewModel);
        return Ok("işlem başarılı");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] AdminViewModel adminViewModel)
    {
        return Ok(_adminService.Login(adminViewModel));
    }

    [HttpGet("validateToken")]
    public IActionResult ValidateToken()
    {
        // İstek başlıklarından Authorization başlığını al
        string authorizationHeader = HttpContext.Request.Headers["Authorization"];

        if(string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return BadRequest("Token bilgisi alınamadı.");
        }

        string token = authorizationHeader.Substring("Bearer ".Length).Trim();
        bool result = _adminService.ValidateToken(token);
        if(result)
            return Ok();

        return BadRequest("Geçersiz Token");
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

