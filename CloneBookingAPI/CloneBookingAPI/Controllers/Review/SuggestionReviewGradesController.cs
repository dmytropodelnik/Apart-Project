﻿using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
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
    public class SuggestionReviewGradesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionReviewGradesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getgrades")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionReviewGrade>>> GetGrades(int page = -1, int pageSize = -1)
        {
            try
            {
                List<SuggestionReviewGrade> grades = new();
                if (page == -1 || pageSize == -1)
                {
                    grades = await _context.SuggestionReviewGrades
                    .Include(g => g.ReviewCategory)
                    .Include(g => g.Review)
                        .ThenInclude(r => r.Suggestion)
                    .ToListAsync();
                }
                else
                {
                    grades = await _context.SuggestionReviewGrades
                    .Include(g => g.ReviewCategory)
                    .Include(g => g.Review)
                        .ThenInclude(r => r.Suggestion)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                }

                return Json(new { code = 200, grades = grades.Select(g => new { g.Id, g.Value, g.ReviewCategory, g.Review.Suggestion }) });
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [Route("editgrade")]
        [HttpPut]
        public async Task<IActionResult> EditGrade([FromBody] SuggestionReviewGrade grade)
        {
            try
            {
                if (grade is null)
                {
                    return Json(new { code = 400 });
                }

                var resGrade = await _context.SuggestionReviewGrades.FirstOrDefaultAsync(g => g.Id == grade.Id);
                if (resGrade is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionReviewGrades.Update(resGrade);
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
