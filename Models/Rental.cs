using System;

namespace CarRentalAPI.Models;

public class Rental
{
    public int Id { get; set; }
    public required int AutoId { get; set; }
    public required Auto Auto { get; set; } 
    public required int ClientId { get; set; }
    public required Client Client { get; set; } 
    public required DateTime FechaInicio { get; set; }
    public required DateTime FechaFin { get; set; }
    public required bool Devuelto { get; set; }
}
