using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Stores file in selected directory.
        /// </summary>
        /// <param name="file">File from webpage form</param>
        /// <param name="directoryPath">Path to directory</param>
        /// <returns>Path to the file</returns>
        public string SaveInDirectory(IFormFile file, string directoryPath);
        /// <summary>
        /// Deletes file in selected path.
        /// </summary>
        /// <param name="filePath">Path to the file</param>
        public void Delete(string filePath);
    }
}
