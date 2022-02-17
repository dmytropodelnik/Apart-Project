using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Review
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ReviewsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getreviews")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CloneBookingAPI.Services.Database.Models.Review.Review>>> GetReviews()
        {
            try
            {
                var reviews = await _context.Reviews.ToListAsync();

                return Json(new { code = 200, reviews });
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

        [Route("addreview")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changereviewbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewMessage == review.ReviewMessage &&
                                                                                r.SuggestionId == review.SuggestionId   &&
                                                                                r.UserId == review.UserId);
                if (resReview is null)
                {
                    return Json(new { code = 400 });
                }
                resReview.ReviewMessage = review.ReviewMessage; 

                _context.Reviews.Update(resReview);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changereview")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeCountry([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
                if (resReview is null)
                {
                    return Json(new { code = 400 });
                }
                resReview.ReviewMessage = review.ReviewMessage;

                _context.Reviews.Update(resReview);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletereview")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == review.Id);
                if (resReview is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Reviews.Remove(resReview);
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
