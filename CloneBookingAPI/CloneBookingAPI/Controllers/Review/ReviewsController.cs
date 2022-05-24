using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<IActionResult> GetReviews(int page = -1, int pageSize = -1)
        {
            try
            {
                List<CloneBookingAPI.Services.Database.Models.Review.Review> reviews = new();
                if (page == -1 || pageSize == -1)
                {
                    reviews = await _context.Reviews
                    .Include(r => r.User)
                    //.Include(r => r.Suggestion)
                    //.ThenInclude(s => s.StayBookings)
                    .Include(r => r.ReviewMessage)
                    .ToListAsync();
                }
                else
                {
                    reviews = await _context.Reviews
                    .Include(r => r.User)
                    //.Include(r => r.Suggestion)
                    //.ThenInclude(s => s.StayBookings)
                    .Include(r => r.ReviewMessage)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, reviews });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getuserreviews")]
        [HttpGet]
        public async Task<IActionResult> GetUserReviews(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var reviews = await _context.Reviews
                    .Include(r => r.User)
                    .Where(r => r.User.Email.Equals(email))
                    .ToListAsync();
                if (reviews is null)
                {
                    return Json(new { code = 400 });
                }

                return Json(new
                {
                    code = 200,
                    reviews,
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("getuserpropertyreviews")]
        [HttpGet]
        public async Task<IActionResult> GetUserPropertyReviews(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return NotFound();
                }

                List<List<CloneBookingAPI.Services.Database.Models.Review.Review>> reviewsList = new();

                var suggestions = await _context.Suggestions
                    .Include(s => s.User)
                    .Where(s => s.User.Email.Equals(email))
                    .ToListAsync();

                var reviews = await _context.Reviews
                    .Include(r => r.User)
                    .Where(r => r.User.Email.Equals(email))
                    .ToListAsync();

                for (int i = 0; i < reviewsList.Count; i++)
                {
                    var exactReviews = reviews
                    .Where(r => r.SuggestionId == suggestions[i].Id)
                    .ToList();

                    reviewsList.Add(exactReviews);
                }

                return Json(new
                {
                    code = 200,
                    reviews,
                    reviewsList,
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

        [Route("getsuggestionreviews")]
        [HttpGet]
        public async Task<IActionResult> GetSuggestionReviews(int id, int page)
        {
            try
            {

                if (id < 1 || page < 1)
                {
                    return NotFound();
                }

                var reviews = await _context.Reviews
                    .Include(r => r.ReviewMessage)
                    .Include(r => r.Grade)
                    .Include(r => r.User)
                        .ThenInclude(u => u.Profile)
                            .ThenInclude(p => p.Address)
                                .ThenInclude(a => a.Country)
                    .Include(r => r.User.Profile.Image)
                    .Where(r => r.SuggestionId == id)
                    .ToListAsync();

                // PAGINATION
                reviews = reviews
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList();

                return Json(new
                {
                    code = 200,
                    reviews,
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

        [TypeFilter(typeof(AuthorizationFilter))]
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("editreview")]
        [HttpPut]
        public async Task<IActionResult> EditReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null)
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.ReviewMessage == review.ReviewMessage &&
                                                                                r.SuggestionId == review.SuggestionId &&
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("editreviewbyid")]
        [HttpPut]
        public async Task<IActionResult> EditReviewById([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
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
        [Route("deletereview")]
        [HttpDelete]
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

        [Route("likereview")]
        [HttpPut]
        public async Task<IActionResult> LikeReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null ||
                    review.Id < 1 ||
                    string.IsNullOrWhiteSpace(review.User.Email))
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews
                    .Include(r => r.Reactions)
                        .ThenInclude(rc => rc.User)
                    .FirstOrDefaultAsync(r => r.Id == review.Id); ;
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review with such id doesn't exist!" });
                }

                var isReacted = resReview.Reactions
                    .Where(r => r.User.Email.Equals(review.User.Email) && r.IsLiked)
                    .FirstOrDefault();
                if (isReacted is not null)
                {
                    return Json(new { code = 400, message = "You had already liked this review!" });
                }

                Reaction newReaction = new();
                newReaction.IsLiked = true;
                newReaction.UserId = resReview.UserId;

                resReview.Reactions.Add(newReaction);

                _context.Reviews.Update(resReview);
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

        [Route("dislikereview")]
        [HttpPut]
        public async Task<IActionResult> DislikeReview([FromBody] CloneBookingAPI.Services.Database.Models.Review.Review review)
        {
            try
            {
                if (review is null || 
                    review.Id < 1  || 
                    string.IsNullOrWhiteSpace(review.User.Email))
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews
                    .Include(r => r.Reactions)
                        .ThenInclude(rc => rc.User)
                    .FirstOrDefaultAsync(r => r.Id == review.Id);
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review with such id doesn't exist!" });
                }

                var isReacted = resReview.Reactions
                    .Where(r => r.User.Email.Equals(review.User.Email) && r.IsDisliked)
                    .FirstOrDefault();
                if (isReacted is not null)
                {
                    return Json(new { code = 400, message = "You had already disliked this review!" });
                }

                Reaction newReaction = new();
                newReaction.IsDisliked = true;
                newReaction.UserId = resReview.UserId;

                resReview.Reactions.Add(newReaction);

                _context.Reviews.Update(resReview);
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
    }
}
