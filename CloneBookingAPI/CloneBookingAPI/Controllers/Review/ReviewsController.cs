using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Configurations.Review;
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
                    .Include(r => r.ReviewMessage)
                    .ToListAsync();
                }
                else
                {
                    reviews = await _context.Reviews
                    .Include(r => r.User)
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
                    return Json(new { code = 400, message = "User data is null." });
                }

                var reviews = await _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Grades)
                        .ThenInclude(g => g.ReviewCategory)
                    .Include(r => r.ReviewMessage)
                    .Include(r => r.Reactions)
                    .Include(r => r.Suggestion)
                    .Where(r => r.User.Email.Equals(email))
                    .ToListAsync();
                if (reviews is null)
                {
                    return Json(new { code = 400, message = "Reviews are not found." });
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

        [Route("getsuggestionreviews")]
        [HttpGet]
        public async Task<IActionResult> GetSuggestionReviews(int id, int page)
        {
            try
            {
                if (id < 1 || page < 1)
                {
                    return Json(new { code = 400, message = "Suggestion id cannot be less than 1." });
                }

                var reviews = await _context.Reviews
                    .Include(r => r.User)
                        .ThenInclude(u => u.Profile)
                            .ThenInclude(p => p.Address)
                                .ThenInclude(a => a.Country)
                    .Include(r => r.ReviewMessage)
                    .Include(r => r.Reactions)
                    .Include(r => r.Grades)
                        .ThenInclude(g => g.ReviewCategory)
                    .Where(r => r.SuggestionId == id)
                    .ToListAsync();
                if (reviews is null)
                {
                    return Json(new { code = 400, message = "Suggestion reviews are not found." });
                }

                var reviewCategories = await _context.ReviewCategories.ToListAsync();
                if (reviewCategories is null)
                {
                    return Json(new { code = 400, message = "Review's categories are not found." });
                }

                List<List<ReviewTuple>> grades = new()
                {
                    Capacity = reviewCategories.Count,
                };
                for (int i = 0; i < reviews.Count; i++)
                {
                    for (int j = 0; j < reviewCategories.Count; j++)
                    {
                        if (i == 0)
                        {
                            grades.Add(new List<ReviewTuple>());
                        }
                        grades[j].Add(new ReviewTuple(reviewCategories[j].Id, reviews[i].Grades[j].Value));
                    }
                }

                List<ReviewTuple> categoryGrades = new()
                {
                    Capacity = reviewCategories.Count,
                };
                for (int i = 0; i < reviewCategories.Count; i++)
                {
                    categoryGrades.Add(new ReviewTuple(
                        i + 1,
                        (int)grades[i].Where(g => g.ReviewCategoryId == i + 1).Average(g => g.Grade)
                        ));
                }

                // PAGINATION
                reviews = reviews
                    .Skip((page - 1) * 10)
                    .Take(10)
                    .ToList();

                return Json(new
                {
                    code = 200,
                    reviews,
                    reviewCategories,
                    categoryGrades,
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
        public async Task<IActionResult> AddReview([FromBody] ReviewPoco review)
        {
            try
            {
                if (review is null               ||
                    review.ReviewMessage is null ||
                    review.Grades is null         ||
                    review.SuggestionId < 1      ||
                    string.IsNullOrWhiteSpace(review.UserEmail)          ||
                    string.IsNullOrWhiteSpace(review.ReviewMessage.Title) ||
                    string.IsNullOrWhiteSpace(review.ReviewMessage.Text))
                {
                    return Json(new { code = 400, message = "Review data is null." });
                }

                var resUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(review.UserEmail));
                if (resUser is null)
                {
                    return Json(new { code = 400, message = "User is not found." });
                }

                CloneBookingAPI.Services.Database.Models.Review.Review newReview = new();

                for (int i = 0; i < review.Grades.Count; i++)
                {
                    SuggestionReviewGrade newGrade = new()
                    {
                        Value = review.Grades[i].Grade,
                        ReviewCategoryId = review.Grades[i].ReviewCategoryId,
                    };
                    newReview.Grades.Add(newGrade);
                }

                ReviewMessage newReviewMessage = new();
                newReviewMessage.Title = review.ReviewMessage.Title;
                newReviewMessage.Text = review.ReviewMessage.Text;

                newReview.ReviewMessage = newReviewMessage;
                newReview.UserId = resUser.Id;
                newReview.ReviewedDate = DateTime.UtcNow;
                newReview.SuggestionId = review.SuggestionId;

                _context.Reviews.Add(newReview);
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
        [TypeFilter(typeof(AccessoryReviewFilter))]
        [Route("editreviewbyid")]
        [HttpPut]
        public async Task<IActionResult> EditReviewById([FromBody] ReviewMessage message)
        {
            try
            {
                if (message is null ||
                    string.IsNullOrWhiteSpace(message.Title) ||
                    string.IsNullOrWhiteSpace(message.Text))
                {
                    return Json(new { code = 400, message = "Review data is null." });
                }

                var resReviewMessage = await _context.ReviewMessages.FirstOrDefaultAsync(m => m.Id == message.Id);
                if (resReviewMessage is null)
                {
                    return Json(new { code = 400, message = "Review message is not found." });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == resReviewMessage.ReviewId);
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review is not found." });
                }

                resReviewMessage.Title = message.Title;
                resReviewMessage.Text = message.Text;

                resReview.ReviewedDate = DateTime.UtcNow;

                _context.ReviewMessages.Update(resReviewMessage);
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
        [TypeFilter(typeof(AccessoryReviewFilter))]
        [Route("deletereview")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400, message = "Review id cannot be less than 1." });
                }

                var resReview = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review is not found." });
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
