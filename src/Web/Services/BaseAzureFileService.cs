using memiarzeEu.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    abstract public class BaseAzureFileService : IFileService
    {
        protected readonly IConfiguration configuration;

        public BaseAzureFileService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public abstract string DirectoryFullPath { get; }
        protected abstract string ContainerName { get; }

        //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-upload-process-images?tabs=dotnet
        public string Save(IFormFile file)
        {
            var container = GetCloudBlobContainer();
            string uniqueFileName = Uri.EscapeDataString(Guid.NewGuid().ToString() + "_" + file.FileName);
            // Get the reference to the block blob from the container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(uniqueFileName);
            // Upload the file
            Task.WaitAll(blockBlob.UploadFromStreamAsync(file.OpenReadStream()));

            return DirectoryFullPath + $"/{uniqueFileName}";
        }

        public void Delete(string filePath)
        {
            var fileName = filePath.Split("/").Last();
            if(fileName != "default.png")
            {
                var container = GetCloudBlobContainer();
                var blockBlob = container.GetBlockBlobReference(fileName);
                Task.WaitAll(blockBlob.DeleteIfExistsAsync());
            }
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            // Create storagecredentials object by reading the values from the configuration (appsettings.json)
            StorageCredentials storageCredentials = new StorageCredentials(configuration.GetValue<string>("Storage:AccountName"), configuration.GetValue<string>("Storage:AccountKey"));

            // Create cloudstorage account by passing the storagecredentials
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);
            return container;
        }
    }
}
