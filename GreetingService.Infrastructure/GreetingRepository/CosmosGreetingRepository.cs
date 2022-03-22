using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
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
        private readonly ILogger _logger;
        private const string _cosmosDbName = "greetingscdb";
        private const string _cosmosContainerName = "greetings";
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosGreetingRepository(IConfiguration configuration, ILogger logger, CosmosClient cosmosClient)
        {
            //var _connectionString = configuration["CosmosDbConnectionString"];
            _cosmosClient = cosmosClient;
            _container = _cosmosClient.GetContainer(_cosmosDbName, _cosmosContainerName);
            _logger = logger;
        }

        public async Task CreateAsync(Greeting greeting)
        {
            await _container.UpsertItemAsync(greeting, new PartitionKey(greeting.id.ToString()));
        }

        public async Task DeleteAllAsync()
        {
            //await _container.R
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
                var stringId = id.ToString();
                var response = await _container.ReadItemAsync<Greeting>(stringId, new PartitionKey(stringId));

                //var response = await _container.GetItemLinqQueryable<Greeting>(x => x.Id == id);

                return response;
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
            var q = "SELECT * FROM c WHERE 1 = 1";
            var qFrom = $" AND c['From'] = '{from}'";
            var qTo= $" AND c.To = '{to}'";

            List<Greeting> greetingList = new();

            if (!string.IsNullOrEmpty(from))
            {
                q += qFrom;
            }
            
            if (!string.IsNullOrEmpty(to))
            {
                q += qTo;
            }

            QueryDefinition query = new QueryDefinition(q); 

            var iterator = _container.GetItemQueryIterator<Greeting>(query);

            while (iterator.HasMoreResults)
            {
                var results = await iterator.ReadNextAsync();
                greetingList.AddRange(results.ToList());
            }
            
            return greetingList;
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            var stringId = greeting.id.ToString();
            await _container.UpsertItemAsync(greeting, new PartitionKey(stringId));
        }
    }
}


