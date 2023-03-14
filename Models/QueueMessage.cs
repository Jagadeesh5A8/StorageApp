namespace StorageWebApp.Models
{
    public class QueueMessage
    {
        public string MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageTimestamp { get; set; }
        public QueueMessage(string messageContent)
        {
            MessageContent = messageContent;
        }
    }
}
