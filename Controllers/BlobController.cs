using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageWebApp.Models;
using StorageWebApp.Repositories;

namespace StorageWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly BlobRepository repository;
        public BlobController(BlobRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("Retrive")]
        public async Task<BlobStorage> GetFile(string fileName)
        {
            var details = await repository.GetFileAsync(fileName);
            return details;
        }

        [HttpPost("create")]
        public async Task<ActionResult<BlobStorage>> AddFile(string fileName, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }
            var details = await repository.AddFileAsync(fileName, file.OpenReadStream());
            return details;
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            await repository.DeleteFileAsync(fileName);
            return NoContent();
        }
    }
}
