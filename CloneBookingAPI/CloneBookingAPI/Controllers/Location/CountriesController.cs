using CloneBookingAPI.Filters;
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
    public class CountriesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CountriesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcountries")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Country> countries = new();
                if (page == -1 || pageSize == -1)
                {
                    countries = await _context.Countries
                    .Include(c => c.Image)
                    .OrderBy(c => c.Title)
                    .ToListAsync();
                }
                else
                {
                    countries = await _context.Countries
                    .Include(c => c.Image)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(c => c.Title)
                    .ToListAsync();
                }

                return Json(new { code = 200, countries });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Search(string country, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country) || page == -1 || pageSize == -1)
                {
                    var res = await _context.Countries.ToListAsync();

                    return Json(new { code = 200, countries = res });
                }

                var countries = await _context.Countries
                    .Where(c => c.Title.Contains(country))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, countries });
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("addcountry")]
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] Country country, IFormFile uploadedFile)
        {
            try
            {
                if (country is null || string.IsNullOrWhiteSpace(country.Title))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country.Title);
                if (res is null)
                {
                    _context.Countries.Add(country);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("changecountrybyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeCountryByName(string country, string newName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country);
                if (resCountry is null)
                {
                    return Json(new { code = 400 });
                }
                resCountry.Title = newName;

                _context.Countries.Update(resCountry);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("changecountry")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeCountry(int id, string newName)
        {
            try
            {
                if (id < 1 || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (resCountry is null)
                {
                    return Json(new { code = 400 });
                }
                resCountry.Title = newName;

                _context.Countries.Update(resCountry);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deletecountrybyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCountryByName(string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country))
                {
                    return Json(new { code = 400 });
                }

                var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country);
                if (resCountry is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Countries.Remove(resCountry);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deletecountry")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (resCountry is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Countries.Remove(resCountry);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
