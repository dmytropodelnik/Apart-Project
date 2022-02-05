﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            try
            {
                return await _context.Countries.ToListAsync();
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

        [Route("addcountry")]
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] string country, IFormFile uploadedFile)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country))
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.Countries.FirstOrDefaultAsync(c => c.Title == country);
                if (res is null)
                {
                    Country newCountry = new();
                    newCountry.Title = country;
                    _context.Countries.Add(newCountry);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecountry")]
        [HttpDelete("{id}")]
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
