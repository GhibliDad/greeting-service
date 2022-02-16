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

namespace GreetingService.Infrastructure.GreetingRepository
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

        public async Task CreateAsync(Greeting greeting)
        {
            var blobName = $"{greeting.From}/{greeting.To}/{greeting.Id}";
            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
                throw new Exception($"Greeting with ID: {greeting.Id} already exists");

            var greetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            await blobClient.UploadAsync(greetingBinary);
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            var blobs = _blobContainerClient.GetBlobsAsync();
            var blob = await blobs.FirstOrDefaultAsync(x => x.Name.EndsWith(id.ToString()));

            if (blob == null)
                throw new Exception($"Greeting with ID: {id} does not exist.");

            var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
            var blobContent = await blobClient.DownloadContentAsync();
            var greeting = blobContent.Value.Content.ToObjectFromJson<Greeting>();
            
            return greeting;
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

        public async Task UpdateAsync(Greeting greeting)
        {
            var blobClient = _blobContainerClient.GetBlobClient(greeting.Id.ToString());
            await blobClient.DeleteIfExistsAsync();
            var greetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            await blobClient.UploadAsync(greetingBinary);
        }

        public async Task DeleteAsync(Guid id)
        {
            //var blobClient = _blobContainerClient.GetBlobClient(id.ToString());
            //await blobClient.DeleteIfExistsAsync();

            var blobClient = _blobContainerClient.GetBlobClient(id.ToString());
            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteAsync();
            }

            throw new Exception($"Delete Failed. Greeting with ID: {id} not found.");
        }

        public async Task DeleteAllAsync()
        {
            var blobs = _blobContainerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                await blobClient.DeleteAsync();
            }
        }
    }
}
