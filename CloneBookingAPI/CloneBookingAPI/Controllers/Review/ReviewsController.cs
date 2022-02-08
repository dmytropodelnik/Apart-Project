using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Services.Database.Models.Review.Review>>> GetReviews()
        {
            try
            {
                return await _context.Reviews.ToListAsync();
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
        public async Task<IActionResult> AddReview([FromBody] Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                Services.Database.Models.Review.Review newReview = new();
                newReview.ReviewMessage = review.ReviewMessage;
                _context.Reviews.Add(newReview);
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
        public async Task<IActionResult> ChangeReview([FromBody] Services.Database.Models.Review.Review review)
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
        public async Task<IActionResult> ChangeCountry([FromBody] Services.Database.Models.Review.Review review)
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
        public async Task<IActionResult> DeleteReview([FromBody] Services.Database.Models.Review.Review review)
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
