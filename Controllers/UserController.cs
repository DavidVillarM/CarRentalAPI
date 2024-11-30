using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CarRentalAPI.Data;
using CarRentalAPI.Models;

namespace CarRentalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CarRentalContext _context;

    public UserController(CarRentalContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginUser)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

        if (user == null) return Unauthorized("Credenciales incorrectas.");

        return Ok(new { Message = "Inicio de sesión exitoso.", Rol = user.Rol });
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_context.Users.ToList());
    }

    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }
}
