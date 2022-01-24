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
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.UserData
{
    [Route("api/[controller]")]
    //[ApiController]
    public class FileUploaderController : Controller
    {
        private readonly ApartProjectDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public FileUploaderController(ApartProjectDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [Route("getallimages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetAllImages()
        {
            return await _context.Files.ToListAsync();
        }

        [Route("uploadfile")]
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFile uploadedFile)
        {
            try
            {
                if (uploadedFile is not null)
                {
                    // путь к папке Files
                    string path = "/files/" + uploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                    _context.Files.Add(file);
                    _context.SaveChanges();

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

        [Route("uploadfiles")]
        [HttpPost]
        public async Task<IActionResult> UploadFiles(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // PUT api/<FileUploaderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileUploaderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
