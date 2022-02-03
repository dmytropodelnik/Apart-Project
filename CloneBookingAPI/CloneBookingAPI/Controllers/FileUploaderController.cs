﻿using CloneBookingAPI.Services.Database;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloneBookingAPI.Controllers.UserData
{
    // [Authorize]
    [Route("api/[controller]")]
    //[ApiController]
    public class FileUploaderController : Controller
    {
        private const int STATUS_200 = 200;
        private const int STATUS_400 = 400;

        private readonly ApartProjectDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly string _storagePath = "/files/";
        private readonly SHA256 sha256 = SHA256.Create();

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
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            try
            {
                if (uploadedFile is not null)
                {
                    // extension of file
                    string extension = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf("."));
                    string newFileName = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(uploadedFile.FileName)));
                    newFileName = newFileName.Replace("/", "")
                                             .Replace("\\", "");

                    string path = _storagePath + newFileName + extension;

                    while (System.IO.File.Exists(_appEnvironment.WebRootPath + path))
                    {
                        newFileName = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(newFileName)));
                        // path to folder Files
                        path = _storagePath + newFileName + extension;
                    }

                    path = _storagePath + newFileName + extension;
                    // save to files folder in wwwroot directory
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    FileModel file = new FileModel { Name = newFileName + extension, Path = path };
                    _context.Files.Add(file);
                    await _context.SaveChangesAsync();

                    return Json(new { code = STATUS_200 });
                }
                return Json(new { code = STATUS_400 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return Json(new { code = STATUS_400 });
            }
        }

        //[Route("uploadfiles")]
        //[HttpPost]
        //public async Task<IActionResult> UploadFiles(IFormFileCollection uploads)
        //{
        //    foreach (var uploadedFile in uploads)
        //    {
        //        // путь к папке Files
        //        string path = "/Files/" + uploadedFile.FileName;
        //        // сохраняем файл в папку Files в каталоге wwwroot
        //        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //        {
        //            await uploadedFile.CopyToAsync(fileStream);
        //        }
        //        FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
        //        _context.Files.Add(file);
        //    }
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

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