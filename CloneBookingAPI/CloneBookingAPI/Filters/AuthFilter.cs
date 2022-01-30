using CloneBookingAPI.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CloneBookingAPI.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        private readonly JwtRepository _repository;
        public AuthFilter(JwtRepository repository)
        {
            _repository = repository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string jwtToken = default;
            var keys = context.HttpContext.Session.Keys;
            foreach (var key in keys)
            {
                if (key == "tokenKey")
                {
                    // _repository.IsValueCorrect(email, key);
                    jwtToken = key;

                    return;
                }
            }

            context.Result = new ContentResult { StatusCode = 400, Content = "You are not authorized." };
        }
    }
}
