using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Database.Models.UserData;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Configurations.Review;
using CloneBookingAPI.Services.Database.Models;
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
        public async Task<IActionResult> GetUserReviews(string email, int page)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400, message = "User data is null." });
                }

                var reviews = await _context.Reviews
                    .Include(r => r.User)
                        .ThenInclude(u => u.Profile)
                            .ThenInclude(p => p.Address)
                                .ThenInclude(a => a.Country)
                    .Include(r => r.User.Profile.Image)
                    .Include(r => r.ReviewMessage)
                    .Include(r => r.Grades)
                        .ThenInclude(g => g.ReviewCategory)
                    .Where(r => r.User.Email.Equals(email))
                    .Select(r => new
                    {
                        r.Id,
                        Author = r.User.FirstName,
                        Country = r.User.Profile.Address.Country.Title,
                        CountryImage = r.User.Profile.Address.Country.Image,
                        AuthorImage = r.User.Profile.Image,
                        ReviewDate = r.ReviewedDate.ToShortDateString(),
                        r.ReviewMessage.Title,
                        r.Grades,
                    })
                    .ToListAsync();
                if (reviews is null)
                {
                    return Json(new { code = 400, message = "Reviews are not found." });
                }

                List<double> reviewGrades = new();
                for (int i = 0; i < reviews.Count; i++)
                {
                    reviewGrades.Add(reviews[i].Grades.Average(g => g.Value));
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
                    reviewGrades,
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
                    .Include(r => r.User.Profile.Image)
                    .Include(r => r.ReviewMessage)
                    .Include(r => r.Reactions)
                    .Include(r => r.StayBooking)
                        .ThenInclude(b => b.Apartments)
                    .Include(r => r.CustomerInfo)
                    .Include(r => r.Grades)
                        .ThenInclude(g => g.ReviewCategory)
                    .Where(r => r.SuggestionId == id)
                    .Select(r => new
                    {
                        r.Id,
                        Author = r.User.FirstName,
                        Country = r.User.Profile.Address.Country.Title,
                        CountryImage = r.User.Profile.Address.Country.Image,
                        AuthorImage = r.User.Profile.Image,
                        ReviewDate = r.ReviewedDate.ToShortDateString(),
                        PositiveMessage = r.ReviewMessage.PositiveText,
                        NegativeMessage = r.ReviewMessage.NegativeText,
                        r.ReviewMessage.Title,
                        r.Grades,
                        Likes = r.Reactions.Where(r => r.IsLiked).Count(),
                        Dislikes = r.Reactions.Where(r => r.IsDisliked).Count(),
                        CheckIn = r.StayBooking.CheckIn.ToShortDateString(),
                        CheckOut = r.StayBooking.CheckOut.ToShortDateString(),
                        r.StayBooking.Nights,
                        GuestsAmount = r.StayBooking.Guests.Count,
                        Apartments = r.StayBooking.Apartments
                            .Select(a => new
                            {
                                a.Id,
                                a.Name,
                                a.GuestsLimit,
                            }),
                    })
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

                List<double> reviewGrades = new();
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
                    reviewGrades.Add(reviews[i].Grades.Average(g => g.Value));
                }

                List<ReviewTuple> categoryGrades = new()
                {
                    Capacity = reviewCategories.Count,
                };
                for (int i = 0; i < reviewCategories.Count; i++)
                {
                    categoryGrades.Add(new ReviewTuple(
                        i + 1,
                        grades[i].Where(g => g.ReviewCategoryId == i + 1).Average(g => g.Grade)
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
                    reviewGrades,
                    reviewCategories,
                    categoryGrades = categoryGrades.Select(g => g.Grade),
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

        [Route("addreview")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewPoco review)
        {
            try
            {
                if (review is null               ||
                    review.ReviewMessage is null ||
                    review.Grades is null        ||
                    review.SuggestionId < 1      ||
                    string.IsNullOrWhiteSpace(review.Owner)                      ||
                    string.IsNullOrWhiteSpace(review.BookingNumber)              ||
                    string.IsNullOrWhiteSpace(review.BookingPIN)                 ||
                    string.IsNullOrWhiteSpace(review.ReviewMessage.Title)        ||
                    string.IsNullOrWhiteSpace(review.ReviewMessage.PositiveText) ||
                    string.IsNullOrWhiteSpace(review.ReviewMessage.NegativeText))
                {
                    return Json(new { code = 400, message = "Review data is null." });
                }

                CloneBookingAPI.Services.Database.Models.Review.Review newReview = new();
                User resUser = new();

                if (int.TryParse(review.Owner, out int resOwner))
                {
                    resUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == resOwner);
                    if (resUser is null)
                    {
                        return Json(new { code = 400, message = "User is not found." });
                    }

                    var resBooking = await _context.StayBookings
                        .FirstOrDefaultAsync(b => b.UserId == resUser.Id &&
                              b.UniqueNumber.Equals(review.BookingNumber) &&
                              b.PIN.Equals(review.BookingPIN));
                    if (resBooking is null)
                    {
                        return Json(new { code = 400, message = "You don't have access to write a review to this suggestion." });
                    }
                    newReview.StayBooking = resBooking;
                }
                else
                {
                    var resBooking = await _context.StayBookings
                        .Include(b => b.CustomerInfo)
                        .FirstOrDefaultAsync(b => b.CustomerInfo.Email.Equals(review.Owner) &&
                            b.UniqueNumber.Equals(review.BookingNumber) &&
                            b.PIN.Equals(review.BookingPIN));
                    if (resBooking is null)
                    {
                        return Json(new { code = 400, message = "You don't have access to write a review to this suggestion." });
                    }
                    newReview.StayBooking = resBooking;
                }

                List<SuggestionReviewGrade> reviewGrades = new();

                for (int i = 0; i < review.Grades.Count; i++)
                {
                    SuggestionReviewGrade newGrade = new()
                    {
                        Value = review.Grades[i].Grade,
                        ReviewCategoryId = review.Grades[i].ReviewCategoryId,
                    };
                    reviewGrades.Add(newGrade);
                }
                newReview.Grades = reviewGrades;

                ReviewMessage newReviewMessage = new()
                {
                    Title = review.ReviewMessage.Title,
                    PositiveText = review.ReviewMessage.PositiveText,
                    NegativeText = review.ReviewMessage.NegativeText
                };

                newReview.ReviewMessage = newReviewMessage;
                newReview.ReviewedDate = DateTime.UtcNow;
                newReview.SuggestionId = review.SuggestionId;

                if (resOwner != 0)
                {
                    newReview.UserId = resUser.Id;
                }
                else
                {
                    CustomerInfo newCustomerInfo = new()
                    {
                        FirstName = review.OwnerFirstName,
                        LastName = review.OwnerLastName,
                        PhoneNumber = review.OwnerPhoneNumber,
                        AddressText = review.AddressText,
                        City = review.City,
                        Country = review.Country,
                        Email = review.Owner,
                    };

                    newReview.CustomerInfo = newCustomerInfo;
                }

                _context.Reviews.Add(newReview);
                await _context.SaveChangesAsync();

                return Json(new { 
                    code = 200,
                    newReview,
                });
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
                    string.IsNullOrWhiteSpace(message.PositiveText) ||
                    string.IsNullOrWhiteSpace(message.NegativeText))
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
                resReviewMessage.PositiveText = message.PositiveText;
                resReviewMessage.NegativeText = message.NegativeText;

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
        public async Task<IActionResult> LikeReview(int id, string email)
        {
            try
            {
                if (id < 1 ||
                    string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews
                    .Include(r => r.Reactions)
                        .ThenInclude(rc => rc.User)
                    .FirstOrDefaultAsync(r => r.Id == id); ;
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review with such id doesn't exist!" });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is null)
                {
                    return Json(new { code = 400, message = "You are not authorized!" });
                }

                var isReacted = resReview.Reactions
                    .Where(r => r.User.Email.Equals(email))
                    .FirstOrDefault();
                if (isReacted is not null)
                {
                    if (isReacted.IsLiked)
                    {
                        return Json(new { code = 400, message = "You had already liked this review!" });
                    }

                    isReacted.IsDisliked = false;
                    isReacted.IsLiked = true;

                    _context.Reactions.Update(isReacted);
                }
                else
                {
                    Reaction newReaction = new()
                    {
                        IsLiked = true,
                        UserId = user.Id,
                    };
                    resReview.Reactions.Add(newReaction);
                    _context.Reviews.Update(resReview);
                }

                await _context.SaveChangesAsync();

                return Json(new { 
                    code = 200,
                    reviewData = new
                    {
                        resReview.Id,
                        Likes = resReview.Reactions.Where(r => r.IsLiked).Count(),
                        Dislikes = resReview.Reactions.Where(r => r.IsDisliked).Count(),
                    },
                });
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
        public async Task<IActionResult> DislikeReview(int id, string email)
        {
            try
            {
                if (id < 1  || 
                    string.IsNullOrWhiteSpace(email))
                {
                    return Json(new { code = 400 });
                }

                var resReview = await _context.Reviews
                    .Include(r => r.Reactions)
                        .ThenInclude(rc => rc.User)
                    .FirstOrDefaultAsync(r => r.Id == id);
                if (resReview is null)
                {
                    return Json(new { code = 400, message = "Review with such id doesn't exist!" });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
                if (user is null)
                {
                    return Json(new { code = 400, message = "You are not authorized!" });
                }

                var isReacted = resReview.Reactions
                    .Where(r => r.User.Email.Equals(email))
                    .FirstOrDefault();
                if (isReacted is not null)
                {
                    if (isReacted.IsDisliked)
                    {
                        return Json(new { code = 400, message = "You had already disliked this review!" });
                    }

                    isReacted.IsLiked = false;
                    isReacted.IsDisliked = true;

                    _context.Reactions.Update(isReacted);
                }
                else
                {
                    Reaction newReaction = new()
                    {
                        IsDisliked = true,
                        UserId = user.Id,
                    };
                    resReview.Reactions.Add(newReaction);
                    _context.Reviews.Update(resReview);
                }

                await _context.SaveChangesAsync();

                return Json(new { 
                    code = 200,
                    reviewData = new
                    {
                        resReview.Id,
                        Likes = resReview.Reactions.Where(r => r.IsLiked).Count(),
                        Dislikes = resReview.Reactions.Where(r => r.IsDisliked).Count(),
                    },
                });
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
