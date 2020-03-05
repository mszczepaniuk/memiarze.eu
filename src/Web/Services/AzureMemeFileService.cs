using memiarzeEu.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;

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
