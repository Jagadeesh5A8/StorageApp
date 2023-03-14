namespace StorageWebApp.Repositories
{
    public interface IFileShareRepository
    {
        Task<bool> UploadFile(IFormFile file);
        Task<byte[]> DownloadFile(string fileName);
        Task<bool> DeleteFile(string fileName);

    }
}
