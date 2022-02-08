using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using CloneBookingAPI.Services.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CloneBookingAPI.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        //private readonly JwtRepository _repository;
        //public AuthFilter(JwtRepository repository)
        //{
        //    _repository = repository;
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // string jwtToken = default;

            // var res = context.HttpContext.Session.Get<TokenModel>("tokenKey");
            // var res1 = SessionExtensions.Get<TokenModel>(context.HttpContext.Session, "tokenKey");
            // _repository.IsValueCorrect(email, key);

            // context.Result = new ContentResult { StatusCode = 400, Content = "You are not authorized." };
        }
    }
}
