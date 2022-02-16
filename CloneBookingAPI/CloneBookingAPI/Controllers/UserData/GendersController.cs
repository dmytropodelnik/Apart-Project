using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.UserData
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public GendersController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getgenders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            try
            {
                var genders = await _context.Genders.ToListAsync();

                return Json(new { code = 200, genders });
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

        [Route("searchgenders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> SearchGenders(string gender)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gender))
                {
                    var res = await _context.Genders.ToListAsync();

                    return Json(new { code = 200, genders = res });
                }

                var genders = await _context.Genders
                    .Where(r => r.Title.Contains(gender))
                    .ToListAsync();

                return Json(new { code = 200, genders });
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

        [Route("addgender")]
        [HttpPost]
        public async Task<IActionResult> AddGender([FromBody] Gender gender)
        {
            try
            {
                if (gender is null || string.IsNullOrWhiteSpace(gender.Title))
                {
                    return Json(new { code = 400 });
                }

                _context.Genders.Add(gender);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("editgender")]
        [HttpPut]
        public async Task<IActionResult> EditGender([FromBody] Gender gender)
        {
            try
            {
                if (gender is null || string.IsNullOrWhiteSpace(gender.Title))
                {
                    return Json(new { code = 400 });
                }

                var resGender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == gender.Id);
                if (resGender is not null)
                {
                    resGender.Title = gender.Title;

                    _context.Genders.Update(resGender);
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

        [Route("deletegender")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGender([FromBody] Gender gender)
        {
            try
            {
                if (gender is null || string.IsNullOrWhiteSpace(gender.Title))
                {
                    return Json(new { code = 400 });
                }

                _context.Genders.Remove(gender);
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
