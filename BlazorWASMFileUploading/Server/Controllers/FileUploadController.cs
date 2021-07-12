using BlazorWASMFileUploading.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWASMFileUploading.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment env;

        public FileUploadController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [HttpPost("PostFile")]
        public async Task<ActionResult> PostFile(UploadedFile uploadedFile)
        {
            var path = $"{env.WebRootPath}\\{uploadedFile.FileName}";
            await using var fs = new FileStream(path, FileMode.Create);
            fs.Write(uploadedFile.FileContent, 0, uploadedFile.FileContent.Length);
            return new CreatedResult(env.WebRootPath, uploadedFile.FileName);
        }
    }
}
