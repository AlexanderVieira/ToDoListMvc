using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly Task<CloudTable> _table;
        private readonly List<TodoModel> _todoModels;
        private TableContinuationToken _continuationToken;

        public TodoRepository(IConfigurationRoot configurationRoot)
        {
            _table = GetTableAsync(configurationRoot);
            _todoModels = new List<TodoModel>();
        }

        public async Task<CloudTable> GetTableAsync(IConfigurationRoot configurationRoot)
        {
            var connStr = configurationRoot.GetSection("MicrosoftAzureStorage:AzureStorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(connStr.Value);
            var tableClient = storageAccount.CreateCloudTableClient();
             var table = tableClient.GetTableReference("Todo");
            await table.CreateIfNotExistsAsync();

            return table;
        }

        public async Task CreateOrUpdate(TodoModel entity)
        {
            Random rnd = new Random();
            entity.Id = rnd.Next(100);
            entity.RowKey = entity.Id.ToString();
            entity.PartitionKey = entity.Name;
            var op = TableOperation.InsertOrReplace(entity);
            await _table.Result.ExecuteAsync(op);

        }

        public async Task<IEnumerable<TodoModel>> GetAll()
        {
            var query = new TableQuery<TodoModel>()
                .Where(TableQuery.GenerateFilterConditionForBool(nameof(TodoModel.Completed),
                    QueryComparisons.Equal, false));
            do
            {
                var queryResults = await _table.Result.ExecuteQuerySegmentedAsync(query, _continuationToken);
                _continuationToken = queryResults.ContinuationToken;
                var tables = queryResults.Results.ToList();
                _todoModels.AddRange(tables);

            } while (_continuationToken != null);

            return _todoModels;
        }

        public async Task<TodoModel> GetTodoModel(string pKey, string rKey)
        {
            var op = TableOperation.Retrieve<TodoModel>(pKey, rKey);
            var result = await _table.Result.ExecuteAsync(op);
            return result.Result as TodoModel;
        }

        public async Task Delete(TodoModel entity)
        {
            var op = TableOperation.Delete(entity);
            await _table.Result.ExecuteAsync(op);
        }
    }
}
