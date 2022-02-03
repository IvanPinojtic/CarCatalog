using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarCatalog.Models
{
    public interface ICarRepository
    {
        IQueryable<Car> GetCars(CarParameters carParameters);
        Task<Car> GetCar(int id);
        IQueryable<Car> GetCarsByName(string name, CarParameters carParameters);
        IQueryable<Car> GetCarsByColor(string color, CarParameters carParameters);
        IQueryable<Car> GetCarsByManufacturerName(string manufacturerName, CarParameters carParameters);
        Task<int> AddCar(Car car);
        Task<HttpStatusCode> DeleteCar(int id);
    }
}
