using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Controllers.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionPageController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public AttractionPageController(ApartProjectDbContext context)
        {
            _context = context;
        }
    }
}
