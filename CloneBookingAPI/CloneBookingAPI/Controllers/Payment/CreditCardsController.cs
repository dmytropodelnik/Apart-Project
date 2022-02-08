﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards()
        {
            try
            {
                return await _context.CreditCards.ToListAsync();
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
                    CreditCard newCard = new();
                    newCard.CardNumber = card.CardNumber;
                    _context.CreditCards.Add(newCard);
                    await _context.SaveChangesAsync();

                    return Json(new { code = 200 });
                }
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletecard")]
        [HttpDelete("{id}")]
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }
    }
}