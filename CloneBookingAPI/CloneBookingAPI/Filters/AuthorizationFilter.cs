using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using CloneBookingAPI.Services.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly JwtRepository _jwtRepository;

        public AuthorizationFilter(JwtRepository jwtRepository)
        {
            _jwtRepository = jwtRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
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

                bool res = _jwtRepository.IsValueCorrect(userData[0], userData[1]);
                if (res is false)
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
