using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Filters
{
    public class OnlyAdminFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ApartProjectDbContext _context;

        public OnlyAdminFilter(ApartProjectDbContext context)
        {
            _context = context;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                if (context.HttpContext.Request.Headers.Authorization.Count == 0)
                {
                    context.Result = new JsonResult(new { code = 403 });
                    return;
                }

                string[] userData = context.HttpContext.Request.Headers.Authorization.ToString().Split(";");
                if (userData.Length < 1)
                {
                    context.Result = new JsonResult(new { code = 403 });
                    return;
                }

                var res = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email.Equals(userData[0]) && u.Role.Name.Equals("admin"));
                if (res is null)
                {
                    context.Result = new JsonResult(new { code = 403 });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                context.Result = new JsonResult(new { code = 500 });
            }
        }
    }
}
