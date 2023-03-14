using Azure.Storage.Queues;
using Newtonsoft.Json;
using StorageWebApp.Models;

namespace StorageWebApp.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        private readonly QueueClient _queueClient;

        public QueueRepository(IConfiguration configuration)
        {
            string connectionString = configuration["Storage:ConnectionString"];
            string queueName = configuration.GetValue<string>("Storage:queueName");
            _queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task AddMessageAsync(QueueMessage message)
        {
            string messageBody = JsonConvert.SerializeObject(message);
            await _queueClient.SendMessageAsync(messageBody);
        }

        public async Task<QueueMessage> DequeueMessageAsync()
        {
            QueueMessage message = null;
            var receivedMessage = await _queueClient.ReceiveMessageAsync();

            if (receivedMessage != null)
            {
                message = JsonConvert.DeserializeObject<QueueMessage>(receivedMessage.Value.MessageText);
                await _queueClient.DeleteMessageAsync(receivedMessage.Value.MessageId, receivedMessage.Value.PopReceipt);
            }

            return message;
        }

        public async Task UpdateMessageAsync(QueueMessage message)
        {
            var receivedMessage = await _queueClient.ReceiveMessageAsync();
            if (receivedMessage?.Value != null)
            {
                var updatedMessage = JsonConvert.DeserializeObject<QueueMessage>(receivedMessage.Value.MessageText);
                updatedMessage.MessageId = message.MessageId;
                updatedMessage.MessageContent = message.MessageContent;
                var messageBody = JsonConvert.SerializeObject(updatedMessage);
                await _queueClient.UpdateMessageAsync(receivedMessage.Value.MessageId, receivedMessage.Value.PopReceipt, messageBody, TimeSpan.Zero);
            }
        }


        public async Task ClearQueueAsync()
        {
            await _queueClient.ClearMessagesAsync();
        }

    }
}
