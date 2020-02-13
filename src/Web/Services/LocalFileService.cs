using memiarzeEu.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    public class LocalFileService : IFileService
    {
        public string SaveInDirectory(IFormFile file, string directoryPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(directoryPath, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return filePath;
        }

        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
