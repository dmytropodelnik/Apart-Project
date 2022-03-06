using CloneBookingAPI.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloneBookingAPI.Database.Configurations.Suggestions
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedsController : ControllerBase
    {
        private readonly ApartProjectDbContext _context;

        public BedsController(ApartProjectDbContext context)
        {
            _context = context;
        }


    }
}
