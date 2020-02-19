using memiarzeEu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Services
{
    public class LocalMemeFileService : BaseLocalFileService, IMemeFileService
    {
        public LocalMemeFileService() : base()
        {
            directoryName = "memes";
        }

        public LocalMemeFileService(string directoryPath) : base(directoryPath)
        {
            directoryName = "memes";
        }
    }
}
