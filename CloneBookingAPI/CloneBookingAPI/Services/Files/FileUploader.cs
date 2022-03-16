﻿using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Files
{
    public class FileUploader
    {
        private readonly ApartProjectDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly string _storagePath = "/files/";
        private readonly SHA256 sha256 = SHA256.Create();

        public FileUploader(ApartProjectDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<bool> UploadSuggestionPhotoAsync(IFormFile uploadedFile, Suggestion suggestion)
        {
            try
            {
                if (uploadedFile is null ||
                    suggestion is null)
                {
                    return false;
                }

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

                file.Suggestions.Add(suggestion);
                _context.Files.Add(file);

                return true;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}