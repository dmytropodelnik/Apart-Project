using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.POCOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionPageController(ApartProjectDbContext context)
        {
            _context = context;
        }

        
    }
}
