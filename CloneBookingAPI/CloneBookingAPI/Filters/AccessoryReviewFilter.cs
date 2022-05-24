using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Filters
{
    public class AccessoryReviewFilter : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ApartProjectDbContext _context;

        public AccessoryReviewFilter(ApartProjectDbContext context)
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
                if (userData.Length < 2)
                {
                    context.Result = new JsonResult(new { code = 403 });
                    return;
                }

                var res = await _context.Suggestions
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.User.Email.Equals(userData[0]) && s.UniqueCode.Equals(userData[1]));
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
