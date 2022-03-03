using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CartsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcarts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            try
            {
                var carts = await _context.Carts.ToListAsync();

                return Json(new { code = 200, carts });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("editcart")]
        [HttpPut]
        public async Task<IActionResult> EditCart([FromBody] Cart cart)
        {
            try
            {
                if (cart is null)
                {
                    return Json(new { code = 400 });
                }

                var resCart = await _context.Carts.FirstOrDefaultAsync(n => n.Id == cart.Id);
                if (resCart is null)
                {
                    return Json(new { code = 400 });
                }

                _context.Carts.Update(resCart);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (DbUpdateException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}
