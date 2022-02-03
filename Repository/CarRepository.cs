using CarCatalog.Models.Database;
using CarCatalog.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarCatalog.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarCatalogContext _context;
        public CarRepository(CarCatalogContext context)
        {
            this._context = context;
        }
        public async Task<int> AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car.Id;
        }

        public async Task<HttpStatusCode> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return HttpStatusCode.NoContent;
        }

        public async Task<Car> GetCar(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public IQueryable<Car> GetCars(CarParameters carParameters)
        {
            var query = _context.Cars
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize);

            if (carParameters.OrderByName)
            {
                query = query.OrderBy(c => c.Name);
            }
            else if (carParameters.OrderByProductionYear)
            {
                query = query.OrderBy(c => c.ProductionYear);
            }

            return query;
        }

        public IQueryable<Car> GetCarsByColor(string color, CarParameters carParameters)
        {
            var query = _context.Cars
                .Where(c => c.Color.ToLower().Contains(color.ToLower()))
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize);

            if (carParameters.OrderByName)
            {
                query = query.OrderBy(c => c.Name);
            }
            else if(carParameters.OrderByProductionYear)
            {
                query = query.OrderBy(c => c.ProductionYear);
            }

            return query;
        }

        public IQueryable<Car> GetCarsByManufacturerName(string manufacturerName, CarParameters carParameters)
        {
            var query = _context.Cars
                .Where(c => c.Manufacturer.Name.ToLower().Contains(manufacturerName.ToLower()))
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize);

            if (carParameters.OrderByName)
            {
                query = query.OrderBy(c => c.Name);
            }
            else if (carParameters.OrderByProductionYear)
            {
                query = query.OrderBy(c => c.ProductionYear);
            }

            return query;
        }

        public IQueryable<Car> GetCarsByName(string name, CarParameters carParameters)
        {
            var query = _context.Cars
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .Skip((carParameters.PageNumber - 1) * carParameters.PageSize)
                .Take(carParameters.PageSize);

            if (carParameters.OrderByName)
            {
                query = query.OrderBy(c => c.Name);
            }
            else if (carParameters.OrderByProductionYear)
            {
                query = query.OrderBy(c => c.ProductionYear);
            }

            return query;
        }
    }
}
