﻿using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionRulesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionRulesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getrules")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionRule>>> GetRules(int page = -1, int pageSize = -1)
        {
            try
            {
                List<SuggestionRule> res = new();
                if (page == -1 || pageSize == -1)
                {
                    res = await _context.SuggestionRules
                    .Include(r => r.SuggestionRuleType)
                    .ToListAsync();
                }
                else
                {
                    res = await _context.SuggestionRules
                        .Include(r => r.SuggestionRuleType)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, rules = res });
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
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionRule>>> Search(string rule, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(rule) || page == -1 || pageSize == -1)
                {
                    var res = await _context.SuggestionRules.ToListAsync();

                    return Json(new { code = 200, rules = res });
                }

                var rules = await _context.SuggestionRules
                    .Include(r => r.SuggestionRuleType)
                    .Where(r => r.Title.Contains(rule) ||
                                r.Text.Contains(rule)  ||
                                r.SuggestionRuleType.Type.Contains(rule))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, rules });
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
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("addrule")]
        [HttpPost]
        public async Task<IActionResult> AddRule([FromBody] SuggestionRule rule)
        {
            try
            {
                if (rule is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionRules.Add(rule);
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
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deleterule")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRule([FromBody] SuggestionRule rule)
        {
            try
            {
                if (rule is null)
                {
                    return Json(new { code = 400 });
                }

                var resRule = await _context.SuggestionRules.FirstOrDefaultAsync(r => r.Text == rule.Title);
                if (resRule is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionRules.Remove(resRule);
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
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("deletetype")]
        [HttpDelete]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resType = await _context.SuggestionRuleTypes.FirstOrDefaultAsync(t => t.Id == id);
                if (resType is null)
                {
                    return Json(new { code = 400 });
                }

                _context.SuggestionRuleTypes.Remove(resType);
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
