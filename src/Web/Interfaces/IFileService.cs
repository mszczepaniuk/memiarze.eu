using Microsoft.AspNetCore.Http;

namespace memiarzeEu.Interfaces
{
    public interface IFileService
    {
        public string Save(IFormFile file);
        public void Delete(string filePath);
        public string DirectoryFullPath { get; }
    }
}
