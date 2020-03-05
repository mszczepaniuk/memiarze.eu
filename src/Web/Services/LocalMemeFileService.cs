using memiarzeEu.Interfaces;
using Microsoft.Extensions.Configuration;

namespace memiarzeEu.Services
{
    public class LocalMemeFileService : BaseLocalFileService, IMemeFileService
    {
        public LocalMemeFileService(IConfiguration configuration) : base(configuration)
        {
            directoryName = "memes";
        }

        public LocalMemeFileService(string directoryPath) : base(directoryPath)
        {
            directoryName = "memes";
        }
    }
}
