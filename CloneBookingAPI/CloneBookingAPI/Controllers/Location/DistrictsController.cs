using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
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
    public class DistrictsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public DistrictsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getdistricts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDistricts()
        {
            try
            {
                var districts = await _context.Districts
                    .Include(d => d.Address)
                    //.Include(d => d.Address.City)
                    //.Include(d => d.Address.Region)
                    //.Include(d => d.Image)
                    .ToListAsync();

                return Json(new { code = 200, districts });
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
        public async Task<ActionResult<IEnumerable<District>>> Search(string district)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(district))
                {
                    var res = await _context.Districts.ToListAsync();

                    return Json(new { code = 200, districts = res });
                }

                var districts = await _context.Districts
                    .Include(a => a.Address)
                        //.ThenInclude(addr => addr.Country)
                    // .Include(a => a.Address.City)
                    // .Include(a => a.Address.District)
                    // .Include(a => a.Address.Region)
                    .Where(a => a.Address.AddressText.Contains(district) ||
                                a.Address.Country.Title.Contains(district) ||
                                a.Address.City.Title.Contains(district) ||
                                a.Address.District.Title.Contains(district) ||
                                a.Address.Region.Title.Contains(district) ||
                                a.Title.Contains(district))
                    .ToListAsync();

                return Json(new { code = 200, districts });
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

        [Route("adddistrict")]
        [HttpPost]
        public async Task<IActionResult> AddDistrict([FromBody] District district, IFormFile uploadedFile)
        {
            try
            {
                if (district is null || string.IsNullOrWhiteSpace(district.Title))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Districts.FirstOrDefaultAsync(d => d.Title == district.Title);
                if (res is null)
                {
                    _context.Districts.Add(district);
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

        [Route("changedistrictbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeDistrictByName(string district, string newName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(district) || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resDistrict = await _context.Districts.FirstOrDefaultAsync(d => d.Title == district);
                if (resDistrict is null)
                {
                    return Json(new { code = 400 });
                }
                resDistrict.Title = newName;

                _context.Districts.Update(resDistrict);
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

        [Route("changedistrict")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeDistrict(int id, string newName)
        {
            try
            {
                if (id < 1 || string.IsNullOrWhiteSpace(newName))
                {
                    return Json(new { code = 400 });
                }

                var resDistrict = await _context.Districts.FirstOrDefaultAsync(d => d.Id == id);
                if (resDistrict is null)
                {
                    return Json(new { code = 400 });
                }
                resDistrict.Title = newName;

                _context.Districts.Update(resDistrict);
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

        [Route("deletedistrictbyname")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDistrictByName(string district)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(district))
                {
                    return Json(new { code = 400 });
                }

                var resDistrict = await _context.Districts.FirstOrDefaultAsync(d => d.Title == district);
                if (resDistrict is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Districts.Remove(resDistrict);
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

        [Route("deletedistrict")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resDistrict = await _context.Districts.FirstOrDefaultAsync(d => d.Id == id);
                if (resDistrict is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Districts.Remove(resDistrict);
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
