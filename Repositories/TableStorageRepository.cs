using Azure.Data.Tables;
using StorageWebApp.Models;

namespace StorageWebApp.Repositories
{
    public class TableStorageRepository : ITableStorageRepository
    {
        private const string TableName = "table1";
        private readonly IConfiguration _configuration;
        public TableStorageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_configuration["Storage:ConnectionString"]);
            var tableClient = serviceClient.GetTableClient(TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
        public async Task<Table> GetTableAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<Table>(category, id);
        }
        public async Task<Table> UpsertTableAsync(Table table)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(table);
            return table;
        }
        public async Task DeleteTableAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(category, id);
        }

    }
}
