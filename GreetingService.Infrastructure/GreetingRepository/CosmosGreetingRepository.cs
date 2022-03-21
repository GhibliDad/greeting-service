using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class CosmosGreetingRepository : IGreetingRepository
    {
        private readonly ILogger logger;
        private const string _cosmosDbName = "greetingscsd";
        private const string _cosmosContainerName = "greetings";
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosGreetingRepository(IConfiguration configuration, ILogger logger, CosmosClient cosmosClient)
        {
            var _connectionString = configuration["CosmosDbConnectionString"];
            _cosmosClient = new CosmosClient(_connectionString);
            _cosmosClient.GetContainer(_cosmosDbName, _cosmosContainerName);
        }

        public async Task CreateAsync(Greeting greeting)
        {
            await _container.CreateItemAsync(greeting);
        }

        public async Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _container.DeleteItemAsync<Greeting>(id.ToString(), new PartitionKey(id.ToString()));
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Greeting>(id, new PartitionKey(id);
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            var query = _container.GetItemQueryIterator<Greeting>();
            var results = new List<Greeting>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            throw new NotImplementedException();
        }
    }
}
