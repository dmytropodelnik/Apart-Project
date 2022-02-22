using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRentalsPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CarRentalsPageController(ApartProjectDbContext context)
        {
            _context = context;
        }
    }
}
