using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.UserData
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempUsersController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public TempUsersController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getusers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TempUser>>> GetUsers(int page = -1, int pageSize = -1)
        {
            try
            {
                List<TempUser> users = new();
                if (page == -1 || pageSize == -1)
                {
                    users = await _context.TempUsers.ToListAsync();
                }
                else
                {
                    users = await _context.TempUsers
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, users });
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

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TempUser>>> Search(string user, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user) || page == -1 || pageSize == -1)
                {
                    var res = await _context.TempUsers.ToListAsync();

                    return Json(new { code = 200, users = res });
                }

                var users = await _context.TempUsers
                    .Where(u => u.FirstName.Contains(user) ||
                                u.LastName.Contains(user) ||
                                u.Email.Contains(user) ||
                                u.PhoneNumber.Contains(user))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, users });
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

        [Route("edituser")]
        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] TempUser user)
        {
            try
            {
                if (user is null)
                {
                    return Json(new { code = 400 });
                }

                var resUser = await _context.TempUsers.FirstOrDefaultAsync(n => n.Id == user.Id);
                if (resUser is null)
                {
                    return Json(new { code = 400 });
                }

                _context.TempUsers.Update(resUser);
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
