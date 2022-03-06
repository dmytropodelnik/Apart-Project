using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.POCOs.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysSearchingController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public StaysSearchingController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult> Search(StaySearchPoco searchObj)
        {
            try
            {
                int pageHelper = searchObj.Page;

                if (searchObj is null || pageHelper < 1)
                {
                    return Json(new { code = 400 });
                }

                if (searchObj.Page == 1)
                {
                    pageHelper = 0;
                }

                string searchCounty = searchObj.Address.Country.Title ?? "";
                string searchCity = searchObj.Address.City.Title ?? "";
                string searchAddressText = searchObj.Address.AddressText ?? "";

                var resSuggestions = await _context.Suggestions
                       .Include(s => s.Address)
                       .Include(s => s.Images)
                       .Include(s => s.BookingCategory)
                       .Include(s => s.StayBookings)
                       .Include(s => s.Reviews)
                       .Include(s => s.Beds)
                       .Include(s => s.RoomTypes)
                       .Include(s => s.AdditionalServices)
                       .Include(s => s.SuggestionReviewGrades)
                       .Where(s => s.Address.Country.Title.Contains(searchCounty)    ||
                                   s.Address.City.Title.Contains(searchCity)         ||
                                   s.Address.AddressText.Contains(searchAddressText) ||
                                   searchAddressText.Contains(s.Address.AddressText) ||
                                   searchCounty.Contains(s.Address.Country.Title)    ||
                                   searchCity.Contains(s.Address.City.Title)         ||
                                   !s.StayBookings
                                            .All(b => (b.CheckIn  >  Convert.ToDateTime(searchObj.DateIn)    &&
                                                       b.CheckIn  >  Convert.ToDateTime(searchObj.DateOut))  ||
                                                      (b.CheckOut <  Convert.ToDateTime(searchObj.DateIn)    &&
                                                       b.CheckOut <  Convert.ToDateTime(searchObj.DateOut))
                                                       ) &&
                                   s.GuestsAmount >= searchObj.GuestsAmount &&
                                   s.RoomsAmount >= searchObj.RoomsAmount)
                       .ToListAsync();

                List<Suggestion> filteredSuggestions = new();

                if (searchObj.Filter.ToLower().Equals("byLowestPrice".ToLower()))
                {
                    filteredSuggestions = OrderByLowestPrice(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byHighestPrice".ToLower()))
                {
                    filteredSuggestions = OrderByHighestPrice(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byHomesAndApartmentsFirst".ToLower()))
                {
                    filteredSuggestions = FilterByHomesAndApartmentsFirst(resSuggestions);
                }
                else if (searchObj.Filter.ToLower().Equals("byBestReviews".ToLower()))
                {
                    filteredSuggestions = OrderByBestGrades(resSuggestions);
                }

                // PAGINATION
                filteredSuggestions
                    .Skip((pageHelper - 1) * 25)
                    .Take(25);            

                return Json(new
                {
                    code = 200,
                    filteredSuggestions,
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

                return Json(new { code = ex.Message });
            }

            List<Suggestion> OrderByLowestPrice(List<Suggestion> items)
            {
                var res = items
                    .OrderBy(s => s.PriceInUSD)
                    .ToList();

                return res;
            }

            List<Suggestion> OrderByHighestPrice(List<Suggestion> items)
            {
                var res = items
                    .OrderByDescending(s => s.PriceInUSD)
                    .ToList();

                return res;
            }

            List<Suggestion> FilterByHomesAndApartmentsFirst(List<Suggestion> items)
            {
                var res = items
                    .Where(s => s.BookingCategory.Category.Equals("Homes") ||
                                s.BookingCategory.Category.Equals("Apartments"))
                    .ToList();

                return res;
            }

            List<Suggestion> OrderByBestGrades(List<Suggestion> items)
            {
                var res = items
                    .OrderByDescending(s => s.SuggestionReviewGrades
                                        .Average(s => s.Value))
                    .ToList();

                return res;
            }
        }

        [Route("searchbybookingcategory")]
        [HttpGet]
        public async Task<ActionResult> SearchByBookingCategory(string category)
        {
            try
            {
                if (category is null)
                {
                    return Json(new { code = 400 });
                }

                var resSuggestions = await _context.Suggestions
                    .Include(s => s.BookingCategory)
                    .Where(s => s.BookingCategory.Category == category)
                    .ToListAsync();

                return Json(new
                {
                    code = 200,
                    resSuggestions,
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

                return Json(new { code = ex.Message });
            }
        }
    }
}
