using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

public class CarDbContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public CarDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connecionString;
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("AppCar.json");
        var config = builder.Build();
        connecionString = config.GetConnectionString("DefaultConnection")!;
        optionsBuilder.UseSqlServer(connecionString);


    //    "ConnectionStrings": {
    //        "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Cars;Integrated Security=True;"
    //}

    }
}