using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageWebApp.Models;
using StorageWebApp.Repositories;

namespace StorageWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileShareController : ControllerBase
    {
        private readonly IFileShareRepository repository;
        public FileShareController(IFileShareRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("Retrieve")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var file = await repository.DownloadFile(fileName);
            if (file == null)
            {
                return NotFound();
            }
            return File(file, "application/octet-stream", fileName);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> UploadFile([FromForm] FileModel file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await repository.UploadFile(file.File);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            var result = await repository.DeleteFile(fileName);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
