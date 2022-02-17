using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Configurations.Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Review
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewMessagesController : Controller
    {
        private readonly ApartProjectDbContext _context;

        public ReviewMessagesController(ApartProjectDbContext context)
        {
            _context = context;
        }

        [Route("getmessages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewMessage>>> GetMessages()
        {
            try
            {
                var messages = await _context.ReviewMessages.ToListAsync();

                return Json(new { code = 200, messages });
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

        [Route("addreviewmessage")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewMessage message)
        {
            try
            {
                if (message is null)
                {
                    return Json(new { code = 400 });
                }

                ReviewMessage newMessage = new();
                newMessage.Text = message.Text;
                _context.ReviewMessages.Add(newMessage);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changereviewbyname")]
        [HttpPut]
        public async Task<IActionResult> ChangeReview([FromBody] ReviewMessage message)
        {
            try
            {
                if (message is null)
                {
                    return Json(new { code = 400 });
                }

                var resMessage = await _context.ReviewMessages.FirstOrDefaultAsync(m => m.Text == message.Text);
                if (resMessage is null)
                {
                    return Json(new { code = 400 });
                }
                resMessage.Text = message.Text;

                _context.ReviewMessages.Update(resMessage);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("changereview")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeCountry([FromBody] ReviewMessage message)
        {
            try
            {
                if (message is null)
                {
                    return Json(new { code = 400 });
                }

                var resMessage = await _context.ReviewMessages.FirstOrDefaultAsync(r => r.Id == message.Id);
                if (resMessage is null)
                {
                    return Json(new { code = 400 });
                }
                resMessage.Text = message.Text;

                _context.ReviewMessages.Update(resMessage);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 400 });
            }
        }

        [Route("deletereview")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([FromBody] ReviewMessage message)
        {
            try
            {
                if (message is null)
                {
                    return Json(new { code = 400 });
                }

                var resMessage = await _context.ReviewMessages.FirstOrDefaultAsync(r => r.Id == message.Id);
                if (resMessage is null)
                {
                    return Json(new { code = 400 });
                }

                _context.ReviewMessages.Remove(resMessage);
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
