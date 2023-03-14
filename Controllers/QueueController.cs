
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorageWebApp.Models;
using StorageWebApp.Repositories;

namespace StorageWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueRepository _queueRepository;

        public QueueController(IQueueRepository queueRepository)
        {
            this._queueRepository = queueRepository;
        }

        [HttpPost("CreateMessage")]
        public async Task AddMessage(QueueMessage message)
        {
            await _queueRepository.AddMessageAsync(message);
        }


        [HttpGet("DequeueMessage")]
        public async Task<QueueMessage> DequeueMessage()
        {
            return await _queueRepository.DequeueMessageAsync();

        }

        [HttpPut("UpdateMessage")]
        public async Task UpdateMessage(QueueMessage message)
        {
            await _queueRepository.UpdateMessageAsync(message);
        }

        [HttpDelete("ClearQueue")]
        public async Task ClearQueue()
        {
            await _queueRepository.ClearQueueAsync();
        }


    }
}
