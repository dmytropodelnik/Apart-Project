﻿using CloneBookingAPI.Filters;
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
    public class AddressesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public AddressesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getaddresses")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Address> addresses = new();
                if (page == -1 || pageSize == -1)
                {
                    addresses = await _context.Addresses
                    .Include(a => a.Country)
                    .ToListAsync();
                }
                else
                {
                    addresses = await _context.Addresses
                    .Include(a => a.Country)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, addresses });
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
        public async Task<ActionResult<IEnumerable<Address>>> Search(string address, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(address) || page == -1 || pageSize == -1)
                {
                    var res = await _context.Addresses.ToListAsync();

                    return Json(new { code = 200, addresses = res });
                }

                var addresses = await _context.Addresses
                    .Include(a => a.Country)
                    .Where(a => a.AddressText.Contains(address)     ||
                                a.Country.Title.Contains(address)   ||
                                a.City.Title.Contains(address)      ||
                                a.District.Title.Contains(address)  ||
                                a.Region.Title.Contains(address))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, addresses });
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

        [Route("getaddress")]
        [HttpGet]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
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
        [Route("addaddress")]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] Address address)
        {
            try
            {
                if (address is null)
                {
                    return Json(new { code = 400 });
                }
                if (string.IsNullOrWhiteSpace(address.AddressText))
                {
                    return Json(new { code = 400 });
                }

                //Address newAddress = new();
                //newAddress.CountryId = address.CountryId;
                //newAddress.CityId = address.CityId;
                //newAddress.DistrictId = address.DistrictId;
                //newAddress.RegionId = address.RegionId;
                //newAddress.AddressText = address.AddressText;
                //newAddress.ZipCode = address.ZipCode;
                //newAddress.PhoneNumber = address.PhoneNumber;
                //newAddress.AddressText = address.AddressText;

                _context.Addresses.Add(address);
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
        [Route("changeaddress")]
        [HttpPut]
        public async Task<IActionResult> ChangeAddress([FromBody] Address newAddress)
        {
            try
            {
                if (newAddress is null)
                {
                    return Json(new { code = 400 });
                }

                var resAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == newAddress.Id);
                if (resAddress is null)
                {
                    return Json(new { code = 400 });
                }
                if (resAddress.CountryId is not null)
                {
                    resAddress.CountryId = newAddress.CountryId;
                }
                if (resAddress.CityId is not null)
                {
                    resAddress.CityId = newAddress.CityId;
                }
                if (resAddress.DistrictId is not null)
                {
                    resAddress.DistrictId = newAddress.DistrictId;
                }
                if (resAddress.RegionId is not null)
                {
                    resAddress.RegionId = newAddress.RegionId;
                }
                if (!string.IsNullOrWhiteSpace(resAddress.ZipCode))
                {
                    resAddress.ZipCode = newAddress.ZipCode;
                }
                if (!string.IsNullOrWhiteSpace(resAddress.PhoneNumber))
                {
                    resAddress.PhoneNumber = newAddress.PhoneNumber;
                }
                if (!string.IsNullOrWhiteSpace(resAddress.AddressText))
                {
                    resAddress.AddressText = newAddress.AddressText;
                }

                _context.Addresses.Update(resAddress);
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
        [Route("deleteaddress")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
                if (resAddress is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Addresses.Remove(resAddress);
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
