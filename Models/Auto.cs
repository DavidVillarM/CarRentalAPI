namespace CarRentalAPI.Models;

public class Auto
{
    public int Id { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public required int Año { get; set; }
    public required string Color { get; set; }
    public required int CantAsientos { get; set; }
    public required string Chapa { get; set; }
    public required decimal CostoDia { get; set; }
    public required bool Disponible { get; set; }
}
