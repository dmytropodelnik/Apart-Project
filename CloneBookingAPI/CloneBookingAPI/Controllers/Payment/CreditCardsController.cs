using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public CreditCardsController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getcreditcards")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards(int page = -1, int pageSize = -1)
        {
            try
            {
                List<CreditCard> cards = new();
                if (page == -1 || pageSize == -1)
                {
                    cards = await _context.CreditCards.ToListAsync();
                }
                else
                {
                    cards = await _context.CreditCards
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, cards });
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

        [Route("addcreditcard")]
        [HttpPost]
        public async Task<IActionResult> AddCreditCard([FromBody] CreditCard card)
        {
            try
            {
                if (card is null)
                {
                    return Json(new { code = 400 });
                }

                var res = await _context.CreditCards.FirstOrDefaultAsync(c => c.CardNumber == card.CardNumber);
                if (res is null)
                {
                    _context.CreditCards.Add(card);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
                return Json(new { code = 400 });
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

        [Route("deletecardbynumber")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCardByName(string cardNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    return Json(new { code = 400 });
                }

                var resCard = await _context.CreditCards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
                if (resCard is null)
                {
                    return Json(new { code = 400 });
                }

                _context.CreditCards.Remove(resCard);
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

        [Route("deletecard")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                if (id < 1)
                {
                    return Json(new { code = 400 });
                }

                var resCard = await _context.CreditCards.FirstOrDefaultAsync(c => c.Id == id);
                if (resCard is null)
                {
                    return Json(new { code = 400 });
                }

                _context.CreditCards.Remove(resCard);
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
