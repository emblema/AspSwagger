using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AspSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private static List<Country> Countries = new()
        {
            new Country()
            {
                Id = 1,
                Name = "Canada",
                Alfa2Code = "CA",
                Alfa3Code = "CAN",
                NumericCode = "124",
                Subdivisions = new()
                {
                    new Subdivision() { Id = 1, Code = "CA-AB", Name = "Alberta" },
                    new Subdivision() { Id = 2, Code = "CA-ON", Name = "Ontario" },
                    new Subdivision() { Id = 3, Code = "CA-QC", Name = "Quebec" },
                }
            },
            new Country()
            {
                Id = 2,
                Name = "Costa Rica",
                Alfa2Code = "CR",
                Alfa3Code = "CRI",
                NumericCode = "188",
                Subdivisions = new()
                {
                    new Subdivision() { Id = 1, Code = "CR-A", Name = "Alajuela" },
                    new Subdivision() { Id = 2, Code = "CR-C", Name = "Cartago" },
                    new Subdivision() { Id = 3, Code = "CR-SJ", Name = "San Jose" },
                }
            },
            new Country()
            {
                Id = 3,
                Name = "Australia",
                Alfa2Code = "AU",
                Alfa3Code = "AUS",
                NumericCode = "036",
                Subdivisions = new()
                {
                    new Subdivision() { Id = 1, Code = "AU-NSW", Name = "New South Wales" },
                    new Subdivision() { Id = 2, Code = "AU-QLD", Name = "Queensland" },
                    new Subdivision() { Id = 3, Code = "AU-VIC", Name = "Victoria" },
                }
            },
        };

        [HttpGet]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<Country>))]
        public IEnumerable<Country> GetCountries()
        {
            return Countries;
        }

        [HttpGet("{id}", Name = "GetCountry")]
        [SwaggerResponse(200, "Success", typeof(Country))]
        [SwaggerResponse(404, "The country was not found")]
        public ActionResult<Country> GetCountry(int id)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            return country;
        }

        [HttpPost]
        [SwaggerResponse(201, "The country was created", typeof(Country))]
        [SwaggerResponse(400, "The country data is invalid")]
        public ActionResult<Country> CreateCountry([FromBody]Country country)
        {
            country.Id = Countries.Count + 1;

            Countries.Add(country);

            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(204, "The country was updated")]
        [SwaggerResponse(400, "The country data is invalid")]
        [SwaggerResponse(404, "The country was not found")]
        public IActionResult UpdateCountry(int id, [FromBody]Country country)
        {
            var countryInList = Countries.SingleOrDefault(c => c.Id == id);
            if (countryInList == null) return NotFound();

            countryInList.Name = country.Name;
            countryInList.Alfa2Code = country.Alfa2Code;
            countryInList.Alfa3Code = country.Alfa3Code;
            countryInList.NumericCode = country.NumericCode;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "The country was deleted")]
        [SwaggerResponse(404, "The country was not found")]
        public IActionResult DeleteCountry(int id)
        {
            var countryInList = Countries.SingleOrDefault(c => c.Id == id);
            if (countryInList == null) return NotFound();

            Countries.Remove(countryInList);

            return NoContent();
        }


        // Subdivisions

        [HttpGet("{id}/subdivisions", Name = "GetCountrySubdivisions")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<Subdivision>))]
        [SwaggerResponse(404, "The country was not found")]
        public ActionResult<IEnumerable<Subdivision>> GetCountrySubdivisions(int id)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            return country.Subdivisions;
        }

        [HttpGet("{id}/subdivisions/{subdivisionId}", Name = "GetSubdivision")]
        [SwaggerResponse(200, "Success", typeof(Subdivision))]
        [SwaggerResponse(404, "The country or subdivision was not found")]
        public ActionResult<Subdivision> GetSubdivision(int id, int subdivisionId)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            var subdivision = country.Subdivisions.SingleOrDefault(s => s.Id == subdivisionId);
            if(subdivision == null) return NotFound();

            return subdivision;
        }

        [HttpPost("{id}/subdivisions")]
        [SwaggerResponse(201, "The subdivision was created", typeof(Subdivision))]
        [SwaggerResponse(400, "The subdivision data is invalid")]
        [SwaggerResponse(404, "The country was not found")]
        public ActionResult<Subdivision> CreateSubdivision(int id, [FromBody]Subdivision subdivision)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            if (country.Subdivisions == null)
                country.Subdivisions = new();

            subdivision.Id = country.Subdivisions.Count + 1;

            country.Subdivisions.Add(subdivision);

            return CreatedAtRoute("GetSubdivision", new { id = country.Id, subdivisionId = subdivision.Id }, subdivision);
        }

        [HttpPut("{id}/subdivisions/{subdivisionId}")]
        [SwaggerResponse(204, "The subdivision was updated")]
        [SwaggerResponse(400, "The subdivision data is invalid")]
        [SwaggerResponse(404, "The country or subdivision was not found")]
        public IActionResult UpdateSubdivision(int id, int subdivisionId, [FromBody] Subdivision subdivision)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            var subdivisionInList = country.Subdivisions.SingleOrDefault(s => s.Id == subdivisionId);
            if (subdivisionInList == null) return NotFound();

            subdivisionInList.Name = subdivision.Name;
            subdivisionInList.Code = subdivision.Code;

            return NoContent();
        }

        [HttpDelete("{id}/subdivisions/{subdivisionId}")]
        [SwaggerResponse(204, "The country was deleted")]
        [SwaggerResponse(404, "The country was not found")]
        public IActionResult DeleteSubdivision(int id, int subdivisionId)
        {
            var country = Countries.SingleOrDefault(c => c.Id == id);
            if (country == null) return NotFound();

            var subdivisionInList = country.Subdivisions.SingleOrDefault(s => s.Id == subdivisionId);
            if (subdivisionInList == null) return NotFound();

            country.Subdivisions.Remove(subdivisionInList);

            return NoContent();
        }
    }

    public class Country
    {
        [SwaggerSchema("The country identifier", ReadOnly = true)]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [MinLength(3 , ErrorMessage = "Name length can't be less than 3.")]
        public string Name { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Alfa2Code length can't be more than 2.")]
        [MinLength(2, ErrorMessage = "Alfa2Code length can't be less than 2.")]
        public string Alfa2Code { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Alfa3Code length can't be more than 3.")]
        [MinLength(3, ErrorMessage = "Alfa3Code length can't be less than 3.")]
        public string Alfa3Code { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "NumericCode length can't be more than 3.")]
        [MinLength(3, ErrorMessage = "NumericCode length can't be less than 3.")]
        public string NumericCode { get; set; }

        [SwaggerSchema("The country subdivisions", ReadOnly = true)]
        public List<Subdivision> Subdivisions { get; set; }
    }

    public class Subdivision
    {
        [SwaggerSchema("The subdivision identifier", ReadOnly = true)]
        public int Id { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Code length can't be more than 6.")]
        [MinLength(4, ErrorMessage = "Code length can't be less than 4.")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        [MinLength(3, ErrorMessage = "Name length can't be less than 3.")]
        public string Name { get; set; }
    }
}