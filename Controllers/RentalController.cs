using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CarRentalAPI.Data;
using CarRentalAPI.Models;
using System;

namespace CarRentalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly CarRentalContext _context;

    public RentalController(CarRentalContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetRentals()
    {
        var rentals = _context.Rentals.Include(r => r.Auto).Include(r => r.Client).ToList();
        return Ok(rentals);
    }

    [HttpPost]
    public IActionResult CreateRental(Rental rental)
    {
        var auto = _context.Autos.Find(rental.AutoId);
        if (auto == null || !auto.Disponible) return BadRequest("El vehículo no está disponible.");

        auto.Disponible = false;
        _context.Rentals.Add(rental);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetRentals), new { id = rental.Id }, rental);
    }

    [HttpPut("{id}/devolver")]
    public IActionResult ReturnRental(int id)
    {
        var rental = _context.Rentals.Find(id);
        if (rental == null) return NotFound();

        if (rental.Devuelto) return BadRequest("El alquiler ya fue devuelto.");

        var auto = _context.Autos.Find(rental.AutoId);
        if (auto == null) return NotFound();

        auto.Disponible = true;
        rental.Devuelto = true;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpGet("informes")]
    public IActionResult GetRentalReports(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Rentals.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(r => r.FechaInicio >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(r => r.FechaFin <= endDate.Value);

        return Ok(query.ToList());
    }
}
