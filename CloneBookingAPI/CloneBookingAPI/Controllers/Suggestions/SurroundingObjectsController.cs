using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurroundingObjectsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SurroundingObjectsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getobjects")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurroundingObject>>> GetObjects()
        {
            try
            {
                var objects = await _context.SurroundingObjects
                    .Include(o => o.SurroundingObjectType)
                    //.Include(o => o.Suggestion)
                    .ToListAsync();

                return Json(new { code = 200, objects });
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
