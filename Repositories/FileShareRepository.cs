using Azure;
using Azure.Storage.Files.Shares;

namespace StorageWebApp.Repositories
{
    public class FileShareRepository : IFileShareRepository
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=jdstora8;AccountKey=zqGe8/qeOtr7CryMq4/sGJklZtZHZ0rPt4ez25ZVHLuBT4Jc72KnpCqjdu64RtxpRzFF3Yi89kEm+AStz0dokQ==;EndpointSuffix=core.windows.net";
        private static string fileShareName = "file1";
        //private static string ShareClient shareClient;

        public FileShareRepository()
        {

        }
        public async Task<byte[]> DownloadFile(string fileName)
        {
            var shareClient = new ShareClient(connectionString, fileShareName);
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(fileName);
            var response = await shareFileClient.DownloadAsync();
            using var memoryStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
        public async Task<bool> UploadFile(IFormFile file)
        {
            var shareClient = new ShareClient(connectionString, fileShareName);
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(file.FileName);
            using (var stream = file.OpenReadStream())
            {
                shareFileClient.Create(stream.Length);
                await shareFileClient.UploadRangeAsync(new HttpRange(0, file.Length), stream);
            }
            return true;
        }
        public async Task<bool> DeleteFile(string fileName)
        {
            var shareClient = new ShareClient(connectionString, fileShareName);
            var shareDirectoryClient = shareClient.GetDirectoryClient("");
            var shareFileClient = shareDirectoryClient.GetFileClient(fileName);
            await shareFileClient.DeleteAsync();
            return true;

        }

    }
}
