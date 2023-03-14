
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using StorageWebApp.Models;

namespace StorageWebApp.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private static string connectionString = "DefaultEndpointsProtocol=https;AccountName=webstorage8;AccountKey=iPhP3TWz7mRob8JBMNz+OEk7k2x8DGM5YQ+7prPqrpC68Y0hKGhn3CBIG9GvapwEhCsAlzOpOA8h+AStmJ4e1Q==;EndpointSuffix=core.windows.net";
        private readonly CloudBlobContainer _container;
        public BlobRepository(string connectionString, string containername)
        {
            var blobClient = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=webstorage8;AccountKey=iPhP3TWz7mRob8JBMNz+OEk7k2x8DGM5YQ+7prPqrpC68Y0hKGhn3CBIG9GvapwEhCsAlzOpOA8h+AStmJ4e1Q==;EndpointSuffix=core.windows.net").CreateCloudBlobClient();
            _container = blobClient.GetContainerReference("jdcon");
        }
        public async Task<BlobStorage> GetFileAsync(string fileName)
        {
            var blob = _container.GetBlockBlobReference(fileName);
            if (await blob.ExistsAsync())
            {
                return new BlobStorage
                {
                    FileName = fileName,
                    BlobUrl = blob.Uri.AbsoluteUri
                };
            }
            return null;

        }
        public async Task<BlobStorage> AddFileAsync(string fileName, Stream stream)
        {
            var blob = _container.GetBlockBlobReference(fileName);
            await blob.UploadFromStreamAsync(stream);
            return new BlobStorage
            {
                FileName = fileName,
                BlobUrl = blob.Uri.AbsoluteUri
            };
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var blob = _container.GetBlockBlobReference(fileName);
            await blob.DeleteAsync();
        }
    }
}
