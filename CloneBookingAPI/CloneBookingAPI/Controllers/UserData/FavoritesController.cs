using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.POCOs.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<Favorite>>> GetFavorites(int page = -1, int pageSize = -1)
        {
            try
            {
                List<Favorite> favorites = new();
                if (page == -1 || pageSize == -1)
                {
                    favorites = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                    .ToListAsync();
                }
                else
                {
                    favorites = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, favorites });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("editfavorite")]
        [HttpPut]
        public async Task<IActionResult> EditFavorite([FromBody] Favorite favorite)
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

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("filter")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> Filter(FavoriteSearchPoco items)
        {
            try
            {
                var suggestions = await _context.Favorites
                    // .Include(f => f.User)
                    .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Images)
                    .Where(f => f.UserId == int.Parse(items.UserId) &&
                                f.Suggestions
                                .All(s => s.Apartments
                                    .All(a => a.RoomsAmount > items.RoomAmount &&
                                          s.StayBookings
                                                .Any(b => (b.CheckIn  > Convert.ToDateTime(items.CheckIn) &&
                                                           b.CheckIn  > Convert.ToDateTime(items.CheckOut)) ||
                                                          (b.CheckOut < Convert.ToDateTime(items.CheckIn) &&
                                                           b.CheckOut < Convert.ToDateTime(items.CheckOut)
                                                          )))))
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    suggestions,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("getuserfavorites")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetUserFavorites(string email)
        {
            try
            {
                List<int> reviewsCount = new();
                List<double> suggestionGrades = new();
                List<decimal> suggestionStartsFrom = new();

                var favorites = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                        .ThenInclude(s => s.SuggestionReviewGrades)
                    .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Images)
                    .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Reviews)
                    .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Address)
                     .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Address.Country)
                     .Include(f => f.Suggestions)
                        .ThenInclude(s => s.Address.City)
                    .FirstOrDefaultAsync(f => f.User.Email.Equals(email));
                if (favorites is null)
                {
                    return Json(new { code = 400 });
                }

                for (int i = 0; i < favorites.Suggestions.Count; i++)
                {
                    var resReviews = await _context.Reviews
                        .Where(r => r.SuggestionId == favorites.Suggestions[i].Id)
                        .ToListAsync();

                    reviewsCount.Add(resReviews.Count);
                }

                for (int i = 0; i < favorites.Suggestions.Count; i++)
                {
                    if (favorites.Suggestions[i].SuggestionReviewGrades.Count == 0)
                    {
                        suggestionGrades.Add(0);
                    }
                    else
                    {
                        suggestionGrades.Add(favorites.Suggestions[i].SuggestionReviewGrades.Average(g => g.Value));
                    }
                    if (favorites.Suggestions[i].Apartments.Count != 0) // favorites.Suggestions[i].Apartments is not null && 
                    {
                        suggestionStartsFrom.Add(favorites.Suggestions[i].Apartments.Min(a => a.PriceInUSD));
                    }
                }

                return Json(new
                {
                    code = 200,
                    favorites = favorites.Suggestions.Select(s => new
                    {
                        s.Id,
                        s.Name,
                        s.Description,
                        country = s.Address.Country.Title,
                        city = s.Address.City.Title,
                        address = s.Address.AddressText,
                        s.StarsRating,
                        reviews = s.Reviews.Count,
                        images = s.Images.Select(i => new { i.Path, i.Name }),
                }),
                    suggestionGrades,
                    reviewsCount,
                    suggestionStartsFrom,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("addsuggestion")]
        [HttpPost]
        public async Task<IActionResult> AddSuggestion([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resFavorite = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                    .FirstOrDefaultAsync(f => f.User.Email.Equals(suggestion.Login));
                if (resFavorite is null)
                {
                    return Json(new { code = 400 });
                }

                resFavorite.Suggestions.Add(resSuggestion);

                _context.Favorites.Update(resFavorite);
                await _context.SaveChangesAsync();

                return Json(new { code = 200, resSuggestion });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [Route("removesuggestion")]
        [HttpDelete]
        public async Task<IActionResult> RemoveSuggestion([FromBody] SuggestionPoco suggestion)
        {
            try
            {
                if (suggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestion = await _context.Suggestions.FirstOrDefaultAsync(s => s.Id == suggestion.Id);
                if (resSuggestion is null)
                {
                    return Json(new { code = 400 });
                }

                var resFavorite = await _context.Favorites
                    .Include(f => f.User)
                    .Include(f => f.Suggestions)
                    .FirstOrDefaultAsync(f => f.User.Email.Equals(suggestion.Login));
                if (resFavorite is null)
                {
                    return Json(new { code = 400 });
                }

                resFavorite.Suggestions.Remove(resSuggestion);

                _context.Favorites.Update(resFavorite);
                await _context.SaveChangesAsync();

                return Json(new { code = 200, resSuggestion });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
