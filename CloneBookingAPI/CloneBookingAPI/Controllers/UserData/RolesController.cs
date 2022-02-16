using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        [Route("searchroles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> SearchRoles(string role)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(role))
                {
                    var res = await _context.Roles.ToListAsync();

                    return Json(new { code = 200, roles = res });
                }

                var roles = await _context.Roles
                    .Where(r => r.Name.Contains(role))
                    .ToListAsync();

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

        [Route("editrole")]
        [HttpPut]
        public async Task<IActionResult> EditRole([FromBody] Role role)
        {
            try
            {
                if (role is null || string.IsNullOrWhiteSpace(role.Name))
                {
                    return Json(new { code = 400 });
                }

                var resRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == role.Id);
                if (resRole is not null)
                {
                    resRole.Name = role.Name;

                    _context.Roles.Update(resRole);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deleterole")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] Role role)
        {
            try
            {
                if (role is null || string.IsNullOrWhiteSpace(role.Name))
                {
                    return Json(new { code = 400 });
                }

                _context.Roles.Remove(role);
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
