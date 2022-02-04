using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCatalog.Models
{
    public class CarEditDTO
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int ProductionYear { get; set; }
        public string Manufacturer { get; set; }
    }
}
