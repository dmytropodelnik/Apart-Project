using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.UserData
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FavoritesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getfavorites")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites()
        {
            try
            {
                var favorites = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                    .ToListAsync();

                return Json(new { code = 200, favorites });
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

        [Route("editfavorite")]
        [HttpPut]
        public async Task<IActionResult> EditFacility([FromBody] Favorite favorite)
        {
            try
            {
                if (favorite is null)
                {
                    return Json(new { code = 400 });
                }

                var resFavorite = await _context.Favorites.FirstOrDefaultAsync(f => f.Id == favorite.Id);
                if (resFavorite is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Favorites.Update(resFavorite);
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
