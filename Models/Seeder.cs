using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Models
{
    public class Seeder
    {
        public static void SeedData(IServiceCollection serviceCollection)
        {
            using (var context = new CarCatalogContext(serviceCollection.BuildServiceProvider().GetRequiredService<DbContextOptions<CarCatalogContext>>()))
            {
                if (context.Manufacturers.Any())
                    return;

                var manufacturer = new Manufacturer
                {
                    Country = "Germany",
                    Name = "Porche"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "Cayenne",
                    Color = "Red",
                    Manufacturer = manufacturer,
                    ProductionYear = 2020,
                    Price = 1000
                });

                context.Cars.Add(new Car
                {
                    Name = "Macan",
                    Color = "Green",
                    Manufacturer = manufacturer,
                    ProductionYear = 2022,
                    Price = 1020
                });

                manufacturer = new Manufacturer
                {
                    Country = "South Korea",
                    Name = "Kia"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "C'eed",
                    Color = "Blue",
                    Manufacturer = manufacturer,
                    ProductionYear = 2022,
                    Price = 2020
                });

                context.Cars.Add(new Car
                {
                    Name = "Sportage",
                    Color = "Yellow",
                    Manufacturer = manufacturer,
                    ProductionYear = 2014,
                    Price = 1030
                });

                manufacturer = new Manufacturer
                {
                    Country = "United States",
                    Name = "Tesla"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "Model 3",
                    Color = "White",
                    Manufacturer = manufacturer,
                    ProductionYear = 2314,
                    Price = 10
                });

                manufacturer = new Manufacturer
                {
                    Country = "Japan",
                    Name = "Mazda"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "3",
                    Color = "Red",
                    Manufacturer = manufacturer,
                    ProductionYear = 2014,
                    Price = 1000
                });

                manufacturer = new Manufacturer
                {
                    Country = "Japan",
                    Name = "Toyota"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "Yaris",
                    Color = "Blue",
                    Manufacturer = manufacturer,
                    ProductionYear = 2004,
                    Price = 1009
                });

                manufacturer = new Manufacturer
                {
                    Country = "France",
                    Name = "Renault"
                };

                context.Manufacturers.Add(manufacturer);

                context.Cars.Add(new Car
                {
                    Name = "Clio",
                    Color = "Blue",
                    Manufacturer = manufacturer,
                    ProductionYear = 2000,
                    Price = 1
                });

                context.Cars.Add(new Car
                {
                    Name = "4",
                    Color = "White",
                    Manufacturer = manufacturer,
                    ProductionYear = 1980,
                    Price = 1
                });

                context.Cars.Add(new Car
                {
                    Name = "5",
                    Color = "Red",
                    Manufacturer = manufacturer,
                    ProductionYear = 1990,
                    Price = 1
                });

                context.SaveChanges();
            }
        }
    }
}
