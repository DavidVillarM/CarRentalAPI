using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CarRentalAPI.Data;
using CarRentalAPI.Models;

namespace CarRentalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AutoController : ControllerBase
{
    private readonly CarRentalContext _context;

    public AutoController(CarRentalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAutos()
    {
        return Ok(_context.Autos.ToList());
    }

    [HttpPost]
    public IActionResult CreateAuto(Auto auto)
    {
        _context.Autos.Add(auto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAutos), new { id = auto.Id }, auto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuto(int id, Auto updatedAuto)
    {
        var auto = _context.Autos.Find(id);
        if (auto == null) return NotFound();

        auto.Marca = updatedAuto.Marca;
        auto.Modelo = updatedAuto.Modelo;
        auto.Año = updatedAuto.Año;
        auto.Color = updatedAuto.Color;
        auto.CantAsientos = updatedAuto.CantAsientos;
        auto.Chapa = updatedAuto.Chapa;
        auto.CostoDia = updatedAuto.CostoDia;
        auto.Disponible = updatedAuto.Disponible;

        _context.SaveChanges();
        return NoContent();
    }
}
