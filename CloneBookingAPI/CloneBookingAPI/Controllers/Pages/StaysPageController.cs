using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaysPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public StaysPageController(ApartProjectDbContext context)
        {
            _context = context;
        }
    }
}
