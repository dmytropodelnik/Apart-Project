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
    public class AirportsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public AirportsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getairports")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirports()
        {
            try
            {
                var airports = await _context.Airports
                    .Include(a => a.Address)
                    .Include(a => a.Address.City)
                    .Include(a => a.Address.District)
                    .Include(a => a.Address.Region)
                    .Include(a => a.Image)
                    .ToListAsync();

                return Json(new { code = 200, airports });
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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> Search(string airport)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(airport))
                {
                    var res = await _context.Airports.ToListAsync();

                    return Json(new { code = 200, airports = res });
                }

                var airports = await _context.Airports
                    .Include(a => a.Address)
                        .ThenInclude(addr => addr.Country)
                    .Include(a => a.Address.City)
                    .Include(a => a.Address.District)
                    .Include(a => a.Address.Region)
                    .Include(a => a.Image)
                    .Where(a => a.Address.AddressText.Contains(airport)     ||
                                a.Address.Country.Title.Contains(airport)   ||
                                a.Address.City.Title.Contains(airport)      ||
                                a.Address.District.Title.Contains(airport)  ||
                                a.Address.Region.Title.Contains(airport)    ||
                                a.Title.Contains(airport)                   ||
                                a.Abbreviation.Contains(airport))
                    .ToListAsync();

                return Json(new { code = 200, airports });
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

        [Route("addairport")]
        [HttpPost]
        public async Task<IActionResult> AddAirport([FromBody] string airport, IFormFile uploadedFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(airport))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Countries.FirstOrDefaultAsync(a => a.Title == airport);
                if (res is null)
                {
                    Airport newAirport = new();
                    newAirport.Title = airport;
                    _context.Airports.Add(newAirport);
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

        [Route("changeairportbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeAirportByName(string airport, string newName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(airport) || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Title == airport);
                if (resAirport is null)
                {
                    return Json(new { code = 400 });
                }
                resAirport.Title = newName;

                _context.Airports.Update(resAirport);
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

        [Route("changeairportbyname")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeAirport(int id, string newName)
        {
            try
            {
                if (id < 1 || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Id == id);
                if (resAirport is null)
                {
                    return Json(new { code = 400 });
                }
                resAirport.Title = newName;

                _context.Airports.Update(resAirport);
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

        [Route("deleteairportbyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAirportByName(string airport)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(airport))
                {
                    return Json(new { code = 400 });
                }

                var resAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Title == airport);
                if (resAirport is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Airports.Remove(resAirport);
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

        [Route("deleteairport")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resAirport = await _context.Airports.FirstOrDefaultAsync(a => a.Id == id);
                if (resAirport is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Airports.Remove(resAirport);
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
