using CarCatalog.Models;
using CarCatalog.Models.Database;
using CarCatalog.Models.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarCatalog.Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly CarCatalogContext _context;
        public ManufacturerRepository(CarCatalogContext context)
        {
            this._context = context;
        }

        public IQueryable<Manufacturer> GetManufacturers(QueryParameters queryParameters)
        {
            return _context.Manufacturers
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);
        }

        public async Task<Manufacturer> GetManufacturer(int id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public IQueryable<Manufacturer> GetManufacturersByCountry(string country, QueryParameters queryParameters)
        {
            return _context.Manufacturers
                .Where(m => m.Country.ToLower().Contains(country.ToLower()))
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

        }

        public IQueryable<Manufacturer> GetManufacturersByName(string name, QueryParameters queryParameters)
        {
            return _context.Manufacturers
                .Where(m => m.Name.ToLower().Contains(name.ToLower()))
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);
        }

        public async Task<int> AddManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            return manufacturer.Id;
        }

        public async Task<HttpStatusCode> EditManufacturer(int id, Manufacturer manufacturer)
        {
            _context.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Manufacturers.Any(e => e.Id == id))
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    throw;
                }
            }

            return HttpStatusCode.NoContent;
        }

        public async Task<Manufacturer> GetManufacturersByNameExact(string name)
        {
            return await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name.ToLower() == name.ToLower());
        }
    }
}
