using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Models.Database
{
    public class CarCatalogContext : DbContext
    {
        public CarCatalogContext(DbContextOptions<CarCatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public DbSet<Car> Cars { get; set; } = null!;
    }
}
