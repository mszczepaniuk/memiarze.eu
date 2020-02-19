using memiarzeEu.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    public abstract class BaseLocalFileService : IFileService
    {
        protected readonly string directoryPath;
        protected string directoryName;
        public string DirectoryFullPath { get { return Path.Combine(directoryPath, directoryName); } }
        
        public BaseLocalFileService()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            directoryPath = Path.Combine(projectDirectory, "src", "Web", "wwwroot", "img");
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
            var fileName = Path.GetFileName(filePath);
            var deletePath = Path.Combine(DirectoryFullPath, fileName);
            File.Delete(deletePath);
        }
    }
}
