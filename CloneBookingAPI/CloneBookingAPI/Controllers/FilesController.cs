using CloneBookingAPI.Filters;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.AspNetCore.Authorization;
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

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("getimages")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetFiles(int page = -1, int pageSize = -1)
        {
            try
            {
                List<FileModel> files = new();
                if (page == -1 || pageSize == -1)
                {
                    files = await _context.Files.ToListAsync();
                }
                else
                {
                    files = await _context.Files
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
                }

                return Json(new { code = 200, files });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileModel>>> Search(string file, int page = -1, int pageSize = -1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(file) || page == -1 || pageSize == -1)
                {
                    var res = await _context.Files.ToListAsync();

                    return Json(new { code = 200, files = res });
                }

                var files = await _context.Files
                    .Where(f => f.Name.Contains(file))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { code = 200, files });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
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

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }

        [TypeFilter(typeof(AuthorizationFilter))]
        [TypeFilter(typeof(OnlyAdminFilter))]
        [Route("readfilecontent")]
        [HttpPost]
        public ActionResult ReadFileContent(IFormFile uploadedFile)
        {
            try
            {
                string letterText = default;

                using (StreamReader sr = new(uploadedFile.OpenReadStream()))
                {
                    letterText = sr.ReadToEnd();
                }

                return Json(new {
                    code = 200,
                    letterText,
                });
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = 500 });
            }
        }
    }
}
