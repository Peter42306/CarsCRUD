using CarsCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsCRUD.Data
{
    public class CarsContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarsContext(DbContextOptions<CarsContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                Cars?.Add(new Car { Manufacturer = "Toyota", Model = "Corolla", Year = 2020, Price = 20000, Description = "Reliable compact sedan", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Honda", Model = "Civic", Year = 2019, Price = 22000, Description = "Sporty and efficient", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Ford", Model = "Mustang", Year = 2021, Price = 35000, Description = "Classic American muscle car", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Chevrolet", Model = "Camaro", Year = 2018, Price = 30000, Description = "High performance sports car", IsAvailable = false });
                Cars?.Add(new Car { Manufacturer = "Tesla", Model = "Model 3", Year = 2022, Price = 45000, Description = "Electric car with autopilot", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "BMW", Model = "3 Series", Year = 2021, Price = 40000, Description = "Luxury sports sedan", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Audi", Model = "A4", Year = 2020, Price = 38000, Description = "German engineering excellence", IsAvailable = false });
                Cars?.Add(new Car { Manufacturer = "Mercedes-Benz", Model = "C-Class", Year = 2019, Price = 42000, Description = "Comfort and style combined", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Hyundai", Model = "Elantra", Year = 2017, Price = 15000, Description = "Affordable and reliable", IsAvailable = true });
                Cars?.Add(new Car { Manufacturer = "Nissan", Model = "Altima", Year = 2021, Price = 24000, Description = "Comfortable and spacious", IsAvailable = true });

                SaveChanges();
            }
        }
    }
}
