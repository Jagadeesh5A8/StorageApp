using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageWebApp.Models;
using StorageWebApp.Repositories;

namespace StorageWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableStorageRepository _storageRepository;
        public TableController(ITableStorageRepository storageRepository)
        {
            _storageRepository = storageRepository ?? throw new ArgumentNullException(nameof(storageRepository));
        }
        [HttpGet]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageRepository.GetTableAsync(category, id));
        }
        [HttpPost("create")]
        public async Task<IActionResult> PostAsync([FromBody] Table table)
        {
            table.PartitionKey = table.Category;
            string Id = Guid.NewGuid().ToString();
            table.Id = Id;
            table.RowKey = Id;
            var createdTable = await _storageRepository.UpsertTableAsync(table);
            return CreatedAtAction(nameof(GetAsync), createdTable);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> PutAsync([FromBody] Table table)
        {
            table.PartitionKey = table.Category;
            table.RowKey = table.Id;
            await _storageRepository.UpsertTableAsync(table);
            return NoContent();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            await _storageRepository.DeleteTableAsync(category, id);
            return NoContent();

        }

    }
}
