﻿using CloneBookingAPI.Services.Database;
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
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await _context.Reviews
                    .Include(r => r.User)
                    .Include(r => r.Suggestion)
                        .ThenInclude(s => s.StayBookings)
                    .Include(r => r.ReviewMessage)
                    .ToListAsync();

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

                return Json(new { 
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

        // [Authorize]
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
                    .Include(r => r.Suggestion)
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
