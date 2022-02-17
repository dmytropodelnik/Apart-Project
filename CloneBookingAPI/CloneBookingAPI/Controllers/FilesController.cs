using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public FilesController(
            ApartProjectDbContext context,
            IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [Route("getimages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetFiles()
        {
            try
            {
                var files = await _context.Files.ToListAsync();

                return Json(new { code = 200, files });
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

        [Route("deleteimage")]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromBody] FileModel file)
        {
            try
            {
                if (file is null)
                {
                    return Json(new { code = 400 });
                }

                var resFile = await _context.Files.FirstOrDefaultAsync(f => f.Id == file.Id);
                if (resFile is null)
                {
                    return Json(new { code = 400 });
                }

                if (System.IO.File.Exists(_appEnvironment.WebRootPath + resFile.Path))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + resFile.Path);
                }
                else
                {
                    return Json(new { code = 400 });
                }

                _context.Files.Remove(resFile);
                await _context.SaveChangesAsync();

                return Json(new { code = 200 });
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
    }
}
