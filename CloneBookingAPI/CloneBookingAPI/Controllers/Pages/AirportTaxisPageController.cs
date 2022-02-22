using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportTaxisPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public AirportTaxisPageController(ApartProjectDbContext context)
        {
            _context = context;
        }
    }
}
