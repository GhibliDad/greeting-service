using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
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
        private readonly CosmosClient _cosmosClient;
        private const string _cosmosAccountName = "greetingscdb";
        private const string _cosmosContainerName = "greetings";
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, };
        private readonly string _connectionString;

        public CosmosGreetingRepository(IConfiguration configuration)
        {
            _connectionString = configuration["LogStorageAccount"];
            _cosmosClient = new CosmosClient(_connectionString, _cosmosContainerName);
            //_cosmosClient.CreateDatabaseIfNotExistsAsync();
        }

        public async Task CreateAsync(Greeting greeting)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            throw new NotImplementedException();
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
