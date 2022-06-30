using CloneBookingAPI.Enums;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly SHA256 sha256 = SHA256.Create();

        private string _storagePath = default;

        public FileUploader(
            ApartProjectDbContext context, 
            IWebHostEnvironment appEnvironment,
            IConfiguration configuration)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _configuration = configuration;
        }

        public async Task<bool> UploadSuggestionPhotoAsync(IFormFile[] uploadedFiles, Suggestion suggestion, PathsEnum storePath)
        {
            try
            {
                if (uploadedFiles is null ||
                    suggestion is null)
                {
                    return false;
                }

                ChooseCorrectPath(storePath);

                foreach (var item in uploadedFiles)
                {
                    // extension of file
                    string extension = item.FileName.Substring(item.FileName.LastIndexOf("."));
                    string newFileName = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(item.FileName)));
                    newFileName = newFileName.Replace("/", "")
                                             .Replace("\\", "");

                    string path = _storagePath + newFileName + extension;

                    while (System.IO.File.Exists(_appEnvironment.WebRootPath + path))
                    {
                        newFileName = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(newFileName)));
                        newFileName = newFileName
                             .Replace("/", "")
                             .Replace("\\", "");
                        // path to folder Files
                        path = _storagePath + newFileName + extension;
                    }

                    path = _storagePath + newFileName + extension;
                    // save to files folder in wwwroot directory
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await item.CopyToAsync(fileStream);
                    }
                    FileModel file = new FileModel { Name = newFileName + extension, Path = path };

                    file.Suggestions.Add(suggestion);
                    _context.Files.Add(file);
                }

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

        private string ChooseCorrectPath(PathsEnum path) => path switch
        {
            PathsEnum.WwwRoot => _storagePath = _configuration["StoragePaths:WwwRoot"],
            PathsEnum.RootFiles => _storagePath = _configuration["StoragePaths:RootFiles"],
            PathsEnum.Cities => _storagePath = _configuration["StoragePaths:Location:Cities"],
            PathsEnum.Countries => _storagePath = _configuration["StoragePaths:Location:Countries"],
            PathsEnum.Regions => _storagePath = _configuration["StoragePaths:Location:Regions"],
            PathsEnum.BookingCategories => _storagePath = _configuration["StoragePaths:Categories:BookingCategories"],
            PathsEnum.StaySuggestions => _storagePath = _configuration["StoragePaths:Suggestions:StaySuggestions"],
            PathsEnum.FlightSuggestions => _storagePath = _configuration["StoragePaths:Suggestions:FlightSuggestions"],
            PathsEnum.CarRentalSuggestions => _storagePath = _configuration["StoragePaths:Suggestions:CarRentalSuggestions"],
            PathsEnum.AttractionSuggestions => _storagePath = _configuration["StoragePaths:Suggestions:AttractionSuggestions"],
            PathsEnum.AirportTaxiSuggestions => _storagePath = _configuration["StoragePaths:Suggestions:AirportTaxiSuggestions"],
            _ => _storagePath = _configuration["StoragePaths:RootFiles"],
        };
    }
}
