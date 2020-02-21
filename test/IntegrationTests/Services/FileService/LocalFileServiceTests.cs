using memiarzeEu.Data;
using memiarzeEu.Interfaces;
using memiarzeEu.Models;
using memiarzeEu.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Services.FileService
{
    public class LocalFileServiceTests
    {
        private string fileTestingFolderPath;
        private LocalMemeFileService memeFileService;
        private LocalAvatarFileService avatarFileService;

        public LocalFileServiceTests()
        {
            var directoryPath = "PUT LOCAL PATH HERE memiarzeEu\\src\\Web\\wwwroot\\img";
            fileTestingFolderPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,"Services","FileService","test.jpg");
            memeFileService = new LocalMemeFileService(directoryPath);
            avatarFileService = new LocalAvatarFileService(directoryPath);
        }

        [Fact]
        public void MemeFileService_Save()
        {
            SaveTest(memeFileService);
        }

        [Fact]
        public void AvatarFileService_Save()
        {
            SaveTest(avatarFileService);
        }

        [Fact]
        public void MemeFileService_Delete()
        {
            DeleteTest(memeFileService);
        }

        [Fact]
        public void AvatarFileService_Delete()
        {
            DeleteTest(avatarFileService);
        }

        private void SaveTest(IFileService fileService)
        {
            string fileName;
            Stream file;
            using (file = File.OpenRead(fileTestingFolderPath))
            {
                IFormFile formFile = new FormFile(file, 0, 0, "test", "test.jpg");
                fileName = Path.GetFileName(fileService.Save(formFile));
            }
            bool doesFileExists = File.Exists(Path.Combine(fileService.DirectoryFullPath, fileName));

            Assert.True(doesFileExists);

            if (doesFileExists)
            {
                File.Delete(Path.Combine(fileService.DirectoryFullPath, fileName));
            }
        }

        private void DeleteTest(BaseLocalFileService fileService)
        {
            string filePath;
            Stream file;
            using (file = File.OpenRead(fileTestingFolderPath))
            {
                IFormFile formFile = new FormFile(file, 0, 0, "test", "test.jpg");
                filePath = fileService.Save(formFile);
            }

            fileService.Delete(filePath);
            bool doesFileExists = File.Exists(Path.Combine(fileService.DirectoryFullPath, Path.GetFileName(filePath)));

            Assert.False(doesFileExists);

            if (doesFileExists)
            {
                File.Delete(Path.Combine(fileService.DirectoryFullPath, Path.GetFileName(filePath)));
            }
        }
    }
}
