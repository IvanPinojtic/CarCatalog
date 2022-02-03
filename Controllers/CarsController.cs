using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCatalog.Models;

namespace CarCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _repository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public CarsController(ICarRepository repository, IManufacturerRepository manufacturerRepository)
        {
            _repository = repository;
            _manufacturerRepository = manufacturerRepository;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarViewDTO>>> GetCars([FromQuery] CarParameters carParameters)
        {
            return await _repository.GetCars(carParameters).Select(c=> 
            new CarViewDTO 
            { 
                Id = c.Id, 
                Color = c.Color, 
                Name = c.Name, 
                Price = c.Price, 
                ProductionYear = c.ProductionYear, 
                Manufacturer = c.Manufacturer.ToString() 
            })
                .ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarViewDTO>> GetCar(int id)
        {
            var car = await _repository.GetCar(id);

            if (car == null)
            {
                return NotFound();
            }

            return new CarViewDTO
            {
                Id = car.Id,
                Color = car.Color,
                Name = car.Name,
                Price = car.Price,
                ProductionYear = car.ProductionYear,
                Manufacturer = car.Manufacturer.ToString()
            };
        }

        // GET: api/CarsByName/cay
        [HttpGet("[controller]/[action]/{name}")]
        public async Task<ActionResult<IEnumerable<CarViewDTO>>> GetCarsByName(string name, [FromQuery] CarParameters carParameters)
        {
            var cars = await _repository.GetCarsByName(name, carParameters).Select(c =>
            new CarViewDTO
            {
                Id = c.Id,
                Color = c.Color,
                Name = c.Name,
                Price = c.Price,
                ProductionYear = c.ProductionYear,
                Manufacturer = c.Manufacturer.ToString()
            })
                .ToListAsync();

            if (!cars.Any())
            {
                return NotFound();
            }

            return cars;
        }

        // GET: api/CarsByColor/white
        [HttpGet("[controller]/[action]/{color}")]
        public async Task<ActionResult<IEnumerable<CarViewDTO>>> GetCarsByColor(string color, [FromQuery] CarParameters carParameters)
        {
            var cars = await _repository.GetCarsByColor(color, carParameters).Select(c =>
            new CarViewDTO
            {
                Id = c.Id,
                Color = c.Color,
                Name = c.Name,
                Price = c.Price,
                ProductionYear = c.ProductionYear,
                Manufacturer = c.Manufacturer.ToString()
            })
                .ToListAsync();

            if (!cars.Any())
            {
                return NotFound();
            }

            return cars;
        }

        // GET: api/CarsByManufacturerName/maz
        [HttpGet("[controller]/[action]/{manufacturerName}")]
        public async Task<ActionResult<IEnumerable<CarViewDTO>>> GetCarsByManufacturerName(string manufacturerName, [FromQuery] CarParameters carParameters)
        {
            var cars = await _repository.GetCarsByManufacturerName(manufacturerName, carParameters).Select(c =>
            new CarViewDTO
            {
                Id = c.Id,
                Color = c.Color,
                Name = c.Name,
                Price = c.Price,
                ProductionYear = c.ProductionYear,
                Manufacturer = c.Manufacturer.ToString()
            })
                .ToListAsync();

            if (!cars.Any())
            {
                return NotFound();
            }

            return cars;
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarViewDTO>> PostCar(CarEditDTO car)
        {
            var manufacturer = await _manufacturerRepository.GetManufacturersByNameExact(car.Manufacturer);

            if (manufacturer != null)
            {
                var newCar = new Car
                {
                    Color = car.Color,
                    Name = car.Name,
                    Price = car.Price,
                    ProductionYear = car.ProductionYear,
                    ManufacturerId = manufacturer.Id
                };

                var id = await _repository.AddCar(newCar);

                return CreatedAtAction("GetCar", new { id = id }, 
                    new CarViewDTO 
                    { 
                        Id = id, 
                        Manufacturer = newCar.Manufacturer.Name, 
                        Color = newCar.Color, 
                        Name = newCar.Name, 
                        Price = newCar.Price, 
                        ProductionYear = newCar.ProductionYear 
                    });
            }

            return NoContent();
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var status = await _repository.DeleteCar(id);

            if (status == System.Net.HttpStatusCode.NoContent)
                return NoContent();

            return NotFound();
        }
    }
}
