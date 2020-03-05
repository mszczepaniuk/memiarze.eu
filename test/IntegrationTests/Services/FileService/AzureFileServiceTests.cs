using memiarzeEu.Interfaces;
using memiarzeEu.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Services.FileService
{
    public class AzureFileServiceTests
    {
        private string fileTestingFolderPath;
        private readonly IConfiguration config;
        private readonly AzureMemeFileService memeService;
        private readonly AzureAvatarFileService avatarService;

        public AzureFileServiceTests()
        {
            fileTestingFolderPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, "Services", "FileService", "test.jpg");
            config = TestHelper.InitConfiguration();
            memeService = new AzureMemeFileService(config);
            avatarService = new AzureAvatarFileService(config);
        }

        [Fact]
        public void SaveAndDeleteMeme()
        {
            SaveAndDeleteElement(memeService, "memes");
        }

        [Fact]
        public void SaveAndDeleteAvatar()
        {
            SaveAndDeleteElement(avatarService, "avatars");
        }

        private void SaveAndDeleteElement(IFileService fileService, string containerName)
        {
            var container = GetCloudBlobContainer(containerName);
            string fileName;
            string filePath;
            Stream file;
            using (file = File.OpenRead(fileTestingFolderPath))
            {
                IFormFile formFile = new FormFile(file, 0, file.Length, "nazwa ze spacjami", "test.jpg");
                filePath = fileService.Save(formFile);
                fileName = filePath.Split("/").Last();
            }
            var blop = container.GetBlockBlobReference(fileName);
            var blopExistsTask = blop.ExistsAsync();
            Task.WaitAll(blopExistsTask);
            Assert.True(blopExistsTask.Result);

            fileService.Delete(filePath);
            blop = container.GetBlockBlobReference(fileName);
            blopExistsTask = blop.ExistsAsync();
            Task.WaitAll(blopExistsTask);
            Assert.False(blopExistsTask.Result);
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(config.GetValue<string>("Storage:AccountName"), config.GetValue<string>("Storage:AccountKey"));

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            return container;
        }
    }
}
