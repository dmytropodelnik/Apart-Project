using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CitiesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcities")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            try
            {
                var cities = await _context.Cities.ToListAsync();

                return Json(new { code = 200, cities });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Search(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    var res = await _context.Cities.ToListAsync();

                    return Json(new { code = 200, cities = res });
                }

                var cities = await _context.Cities
                    //.Include(c => c.Country)
                    .Where(c => c.Country.Title.Contains(city) ||
                                c.Title.Contains(city))
                    .ToListAsync();

                return Json(new { code = 200, cities });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("getcountrycities")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCountryCities(string country)
        {
            try
            {
                var res =  await _context.Cities
                    .Where(c => c.Country.Title == country)
                    .ToListAsync();
                if (res is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new { code = 200, cities = res });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("addcity")]
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] City city, IFormFile uploadedFile)
        {
            try
            {

                if (city is null || string.IsNullOrWhiteSpace(city.Title))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Cities.FirstOrDefaultAsync(c => c.Title == city.Title);
                if (res is null)
                {
                    _context.Cities.Add(city);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changecitybynameandcountry")]
        [HttpPut]
        public async Task<IActionResult> ChangeCityByNameAndCountry(string country, string city, string newName)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(country) ||
                    string.IsNullOrWhiteSpace(city) ||
                    string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resCity = await _context.Cities.FirstOrDefaultAsync(c => c.Title == city);
                if (resCity is null)
                {
                    return Json(new { code = 400 });
                }
                resCity.Title = newName;

                _context.Cities.Update(resCity);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changecity")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeCity(int id, string newName)
        {
            try
            {
                if (id < 1 ||
                    string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
                if (resCity is null)
                {
                    return Json(new { code = 400 });
                }
                resCity.Title = newName;

                _context.Cities.Update(resCity);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecitybyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCityByName(string city)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(city))
                {
                    return Json(new { code = 400 });
                }

                var resCity = await _context.Cities.FirstOrDefaultAsync(c => c.Title == city);
                if (resCity is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Cities.Remove(resCity);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecity")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
                if (resCity is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Cities.Remove(resCity);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
