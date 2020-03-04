using memiarzeEu.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    public class AzureMemeFileService : BaseAzureFileService, IMemeFileService
    {
        public AzureMemeFileService(IConfiguration configuration) : base(configuration)
        {
        }

        public override string DirectoryFullPath => configuration.GetValue<string>("AzureFileContainers:Memes");

        protected override string ContainerName => DirectoryFullPath.Split("/").Last();
    }
}
