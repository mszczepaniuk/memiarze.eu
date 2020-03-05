using memiarzeEu.Interfaces;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.Services
{
    public class LocalAvatarFileService : BaseLocalFileService, IAvatarFileService
    {
        public LocalAvatarFileService(IConfiguration configuration) : base(configuration)
        {
            directoryName = "avatars";
        }

        public LocalAvatarFileService(string directoryPath) : base(directoryPath)
        {
            directoryName = "avatars";
        }
    }
}
