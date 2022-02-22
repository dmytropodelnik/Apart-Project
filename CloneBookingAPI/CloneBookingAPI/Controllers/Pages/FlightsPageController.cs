using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public FlightsPageController(ApartProjectDbContext context)
        {
            _context = context;
        }
    }
}
