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
        private LocalFileService localFileService;
        private string fileTestingFolderPath;

        public LocalFileServiceTests()
        {
            localFileService = new LocalFileService();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            fileTestingFolderPath = Path.Combine(projectDirectory,"Services","FileService","FileTesting");
            ClearUnnecessaryFiles("Delete");
            ClearUnnecessaryFiles("Save");
        }

        [Fact]
        public void SaveInDirectory_testjpgToSaveFolder()
        {
            Stream file;
            using (file = File.OpenRead(Path.Combine(fileTestingFolderPath, "test.jpg")))
            {
                IFormFile formFile = new FormFile(file, 0, 0, "test", "test.jpg");
                var directoryPath = Path.Combine(fileTestingFolderPath, "Save");

                localFileService.SaveInDirectory(formFile, directoryPath);
            }

            var fileCount = Directory.GetFiles(Path.Combine(fileTestingFolderPath, "Save")).Length;

            Assert.True(fileCount == 2);
            ClearUnnecessaryFiles("Save");
        }

        [Fact]
        public void Delete_ToSaveFolder()
        {
            var mockFileName = "FakeFile.txt";
            var filePath = Path.Combine(fileTestingFolderPath, "Delete", mockFileName);
            File.Create(filePath).Dispose();

            localFileService.Delete(filePath);

            Assert.False(File.Exists(filePath));
            ClearUnnecessaryFiles("Delete");
        }

        private void ClearUnnecessaryFiles(string folderName)
        {
            var files = Directory.GetFiles(Path.Combine(fileTestingFolderPath, folderName));
            var gitFilePath = Path.Combine(fileTestingFolderPath, folderName, "GitNoticeMe.txt");

            foreach (var file in files)
            {
                if (!Equals(file, gitFilePath))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
