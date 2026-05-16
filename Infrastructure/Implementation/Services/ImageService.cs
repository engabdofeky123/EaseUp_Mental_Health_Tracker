using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class ImageService : IImageService
    {
        private readonly IFileService _fileService;

        public ImageService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var folderPath = _fileService.GetUploadPath();

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{fileName}";
        }

    }
}
