using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CarRentalAPI.Data;
using CarRentalAPI.Models;

namespace CarRentalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly CarRentalContext _context;

    public ClientController(CarRentalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetClients()
    {
        return Ok(_context.Clients.ToList());
    }

    [HttpPost]
    public IActionResult CreateClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetClients), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateClient(int id, Client updatedClient)
    {
        var client = _context.Clients.Find(id);
        if (client == null) return NotFound();

        client.Nombre = updatedClient.Nombre;
        client.Telefono = updatedClient.Telefono;
        client.Email = updatedClient.Email;

        _context.SaveChanges();
        return NoContent();
    }
}

