using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCatalog.Models;
using Newtonsoft.Json;
using CarCatalog.Repository;
using CarCatalog.Models.Parameters;
using CarCatalog.Models.DTO;
using CarCatalog.Models.Database;

namespace CarCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;

        //db/2testa

        public ManufacturersController(IManufacturerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerViewDTO>>> GetManufacturers([FromQuery] QueryParameters queryParameters)
        {
            return await _repository.GetManufacturers(queryParameters).Select(m =>
            new ManufacturerViewDTO
            {
                Id = m.Id,
                Name = m.Name,
                Country = m.Country
            })
                .ToListAsync();
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerViewDTO>> GetManufacturer(int id)
        {
            var manufacturer = await _repository.GetManufacturer(id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return new ManufacturerViewDTO
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country
            };
        }

        // GET: api/ManufacturersByName/toy
        [HttpGet("[controller]/[action]/{name}")]
        public async Task<ActionResult<IEnumerable<ManufacturerViewDTO>>> GetManufacturersByName(string name, [FromQuery] QueryParameters queryParameters)
        {
            var manufacturers = await _repository.GetManufacturersByName(name, queryParameters).Select(m =>
            new ManufacturerViewDTO
            {
                Id = m.Id,
                Name = m.Name,
                Country = m.Country
            })
                .ToListAsync();

            if (!manufacturers.Any())
            {
                return NotFound();
            }

            return manufacturers;
        }

        // GET: api/ManufacturersByCountry/jap
        [HttpGet("[controller]/[action]/{country}")]
        public async Task<ActionResult<IEnumerable<ManufacturerViewDTO>>> GetManufacturersByCountry(string country, [FromQuery] QueryParameters queryParameters)
        {
            var manufacturers = await _repository.GetManufacturersByCountry(country, queryParameters).Select(m =>
            new ManufacturerViewDTO
            {
                Id = m.Id,
                Name = m.Name,
                Country = m.Country
            })
                .ToListAsync();

            if (!manufacturers.Any())
            {
                return NotFound();
            }

            return manufacturers;
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(int id, ManufacturerEditDTO manufacturer)
        {
            var status = await _repository.EditManufacturer(id, new Manufacturer
            {
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Id = id
            });

            if (status == System.Net.HttpStatusCode.NoContent)
                return NoContent();

            return NotFound();
        }

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManufacturerViewDTO>> PostManufacturer(ManufacturerEditDTO manufacturer)
        {
            var newManufacturer = new Manufacturer
            {
                Country = manufacturer.Country,
                Name = manufacturer.Name
            };

            var id = await _repository.AddManufacturer(newManufacturer);

            return CreatedAtAction("GetManufacturer", new { id = id }, new ManufacturerViewDTO 
            {
                Id = id,
                Name = newManufacturer.Name,
                Country = newManufacturer.Country
            });
        }

        // GET: api/ManufacturersByNameExact/toyota
        [HttpGet("[controller]/[action]/{name}")]
        public async Task<ActionResult<Manufacturer>> GetManufacturersByNameExact(string name)
        {
            var manufacturer = await _repository.GetManufacturersByNameExact(name);

            return manufacturer;
        }
    }
}
