using CarRentalAPI.Data;
using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<CarRentalContext>(options =>
    options.UseInMemoryDatabase("CarRentalDB"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod()  
              .AllowAnyHeader(); 
    });
});

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarRentalContext>();

    // Datos 
    db.Autos.AddRange(
        new Auto { Id = 1, Marca = "Toyota", Modelo = "Corolla", Año = 2020, Color = "Rojo", CantAsientos = 5, Chapa = "ABC-123", CostoDia = 50, Disponible = true },
        new Auto { Id = 2, Marca = "Honda", Modelo = "Civic", Año = 2019, Color = "Azul", CantAsientos = 5, Chapa = "DEF-456", CostoDia = 45, Disponible = true }
    );

    db.Clients.AddRange(
        new Client { Id = 1, Nombre = "Juan Pérez", Telefono = "123456789", Email = "juan@example.com" },
        new Client { Id = 2, Nombre = "María Gómez", Telefono = "987654321", Email = "maria@example.com" }
    );

    db.Users.Add(new User { Id = 1, Username = "admin", Password = "1234", Rol = "admin" });

    db.SaveChanges();
}

app.UseRouting();
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();
