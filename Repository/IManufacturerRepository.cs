using CarCatalog.Models.Database;
using CarCatalog.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarCatalog.Repository
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> GetManufacturers(QueryParameters queryParameters);
        Task <Manufacturer> GetManufacturer(int id);
        IQueryable<Manufacturer> GetManufacturersByName(string name, QueryParameters queryParameters);
        Task<Manufacturer> GetManufacturersByNameExact(string name);
        IQueryable<Manufacturer> GetManufacturersByCountry(string country, QueryParameters queryParameters);
        Task<int> AddManufacturer(Manufacturer manufacturer);
        Task<HttpStatusCode> EditManufacturer(int id, Manufacturer manufacturer);
    }
}
