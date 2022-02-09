using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.UserData
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public RolesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getroles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                return Json(new { code = 200, roles });
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

        [Route("addrole")]
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] Role role)
        {
            try
            {
                if (role is null || string.IsNullOrWhiteSpace(role.Name))
                {
                    return Json(new { code = 400 });
                }

                _context.Roles.Add(role);
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
