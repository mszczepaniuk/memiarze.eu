using memiarzeEu.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace memiarzeEu.Services
{
    public class AzureAvatarFileService : BaseAzureFileService, IAvatarFileService
    {
        public AzureAvatarFileService(IConfiguration configuration) : base(configuration)
        {
        }

        public override string DirectoryFullPath => configuration.GetValue<string>("AzureFileContainers:Avatars");

        protected override string ContainerName => DirectoryFullPath.Split("/").Last();
    }
}
