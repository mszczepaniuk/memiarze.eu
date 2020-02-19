﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace memiarzeEu.Interfaces
{
    public interface IFileService
    {
        public string Save(IFormFile file);
        public void Delete(string filePath);
        public string DirectoryFullPath { get; }
    }
}
