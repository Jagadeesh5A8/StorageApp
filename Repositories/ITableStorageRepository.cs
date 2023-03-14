using StorageWebApp.Models;

namespace StorageWebApp.Repositories
{
    public interface ITableStorageRepository
    {
        Task<Table> GetTableAsync(string category, string id);
        Task<Table> UpsertTableAsync(Table table);
        Task DeleteTableAsync(string category, string id);

    }
}
