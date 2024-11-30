namespace CarRentalAPI.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Telefono { get; set; }
        public required string Email { get; set; }
    }
}
