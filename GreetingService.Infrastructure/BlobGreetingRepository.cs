using Azure.Storage.Blobs;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure
{
    public class BlobGreetingRepository : IGreetingRepository
    {
        private const string _blobContainerName = "greetings";
        private readonly BlobContainerClient _blobContainerClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, };

        public BlobGreetingRepository(IConfiguration configuration)
        {
            var connectionString = configuration["LogStorageAccount"];
            _blobContainerClient = new BlobContainerClient(connectionString, _blobContainerName);
            _blobContainerClient.CreateIfNotExists();
        }
        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            var greetings = new List<Greeting>();
            var blobs = _blobContainerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                var blobContent = await blobClient.DownloadContentAsync();
                var greeting = blobContent.Value.Content.ToObjectFromJson<Greeting>();
                greetings.Add(greeting);
            }
            
            return greetings;
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            var greetings = new List<Greeting>();
            var blobClient = _blobContainerClient.GetBlobClient(id.ToString());
            if (await blobClient.ExistsAsync()){
                var blobContent = await blobClient.DownloadContentAsync();
                var greeting = blobContent.Value.Content.ToObjectFromJson<Greeting>();
                return greeting;
            }
            
            return null;
        }

        public async Task CreateAsync(Greeting greeting)
        {
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(content);
            var existingGreeting = greetings?.FirstOrDefault(x => x.Id == greeting.Id);

            if (greetings.Any(x => x.Id == greeting.Id))
                throw new Exception($"Greeting with ID: {greeting.Id} already exists");

            greetings.Add(greeting);

            File.WriteAllText(_filePath, JsonSerializer.Serialize(greetings, _jsonSerializerOptions));
        }
        public async Task UpdateAsync(Greeting greeting)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
