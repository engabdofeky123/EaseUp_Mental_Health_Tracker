using Application.Interfaces;

namespace API.Helper
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetUploadPath()
        {
            var rootPath = _env.ContentRootPath; 
            var uploadsPath = Path.Combine(rootPath, "wwwroot", "uploads");

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            return uploadsPath;

        }
    }
}
