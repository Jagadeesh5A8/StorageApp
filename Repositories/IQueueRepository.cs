

using StorageWebApp.Models;

namespace StorageWebApp.Repositories
{
    public interface IQueueRepository
    {
        Task AddMessageAsync(QueueMessage message);
        Task<QueueMessage> DequeueMessageAsync();
        Task UpdateMessageAsync(QueueMessage message);
        Task ClearQueueAsync();

    }
}
