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
    public class SurroundingObjectTypesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SurroundingObjectTypesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("gettypes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurroundingObjectType>>> GetTypes()
        {
            try
            {
                var types = await _context.SurroundingObjectTypes.ToListAsync();

                return Json(new { code = 200, types });
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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuggestionRuleType>>> Search(string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    var res = await _context.SurroundingObjectTypes.ToListAsync();

                    return Json(new { code = 200, types = res });
                }

                var types = await _context.SurroundingObjectTypes
                    .Where(t => t.Type.Contains(type))
                    .ToListAsync();

                return Json(new { code = 200, types });
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
    }
}
