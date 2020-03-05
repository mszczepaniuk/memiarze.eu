using memiarzeEu.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace memiarzeEu.Services
{
    public abstract class BaseLocalFileService : IFileService
    {
        protected readonly string directoryPath;
        private readonly IConfiguration configuration;
        protected string directoryName;
        public string DirectoryFullPath { get { return Path.Combine(directoryPath, directoryName); } }

        public BaseLocalFileService(IConfiguration configuration)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            directoryPath = Path.Combine(projectDirectory, "src", "Web", "wwwroot", "img");
            this.configuration = configuration;
        }

        // For testing
        public BaseLocalFileService(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        public string Save(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(DirectoryFullPath, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return $"/img/{directoryName}/{Uri.EscapeDataString(uniqueFileName)}";
        }

        public void Delete(string filePath)
        {
            if (filePath != configuration.GetSection("DefaultAvatarPath").Value)
            {
                var fileName = Path.GetFileName(filePath);
                var deletePath = Path.Combine(DirectoryFullPath, fileName);
                File.Delete(deletePath);
            }
        }
    }
}
