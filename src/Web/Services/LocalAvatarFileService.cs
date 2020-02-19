using memiarzeEu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    public class LocalAvatarFileService : BaseLocalFileService, IAvatarFileService
    {
        public LocalAvatarFileService() : base()
        {
            directoryName = "avatars";
        }

        public LocalAvatarFileService(string directoryPath) : base(directoryPath)
        {
            directoryName = "avatars";
        }
    }
}
