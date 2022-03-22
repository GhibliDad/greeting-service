using Azure.Storage.Blobs;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class BlobGreetingRepository : IGreetingRepository
    {
        private const string _blobContainerName = "greetings";
        private const string _blobContainerCsvName = "greetings-csv";
        private readonly BlobContainerClient _blobContainerClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, };
        private readonly string _connectionString;

        public BlobGreetingRepository(IConfiguration configuration)
        {
            _connectionString = configuration["LogStorageAccount"];
            _blobContainerClient = new BlobContainerClient(_connectionString, _blobContainerName);
            _blobContainerClient.CreateIfNotExists();
        }

        public async Task CreateAsync(Greeting greeting)
        {
            var blobName = $"{greeting.From}/{greeting.To}/{greeting.id}";
            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            if (await blobClient.ExistsAsync())
                throw new Exception($"Greeting with ID: {greeting.id} already exists");

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
            var oldGreeting = await GetAsync(greeting.id);

            var oldGreetingPath = $"{oldGreeting.From}/{oldGreeting.To}/{oldGreeting.id}";
            var oldGreetingBlobClient = _blobContainerClient.GetBlobClient(oldGreetingPath);
            await oldGreetingBlobClient.DeleteAsync();

            var newGreetingPath = $"{greeting.From}/{greeting.To}/{greeting.id}";
            var newGreetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            var newGreetingBlobClient = _blobContainerClient.GetBlobClient(newGreetingPath);
            await newGreetingBlobClient.UploadAsync(newGreetingBinary);
        }

        public async Task DeleteAsync(Guid id)
        {
            await DeleteAsync(id, _blobContainerName);
            await DeleteAsync(id, _blobContainerCsvName);
        }
        private async Task DeleteAsync(Guid id, string containerName)
        {
            var blobContainerClient = new BlobContainerClient(_connectionString, containerName);
            var blobs = blobContainerClient.GetBlobsAsync();

            var blob = await blobs.FirstOrDefaultAsync(x => x.Name.EndsWith(id.ToString()));

            if (blob == null)
                throw new Exception($"Greeting with ID: {id} does not exist.");

            var blobClient = blobContainerClient.GetBlobClient(blob.Name);
            await blobClient.DeleteAsync();
        }

        public async Task DeleteAllAsync()
        {
            await DeleteAllAsync(_blobContainerName);
            await DeleteAllAsync(_blobContainerCsvName);
        }

        private async Task DeleteAllAsync(string containerName)
        {
            var blobContainerClient = new BlobContainerClient(_connectionString, containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            await foreach (var blob in blobs)
            {
                var blobClient = blobContainerClient.GetBlobClient(blob.Name);
                await blobClient.DeleteAsync();
            }
        }

        public async Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            var greetings = new List<Greeting>();

            var prefix = "";
            if (!string.IsNullOrWhiteSpace(from))
            {
                prefix = from;

                if (!string.IsNullOrWhiteSpace(to))
                {
                    prefix = $"{prefix}/{to}";
                }
            }

            var blobs = _blobContainerClient.GetBlobsAsync(prefix: prefix);

            
            await foreach (var blob in blobs)
            {
                var blobNameParts = blob.Name.Split('/');
             
                if (from != null && to != null && blob.Name.StartsWith($"{from}/{to}/"))
                {
                    var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                    var greetingBinary = await blobClient.DownloadContentAsync();
                    var greeting = greetingBinary.Value.Content.ToObjectFromJson<Greeting>();
                    greetings.Add(greeting);
                }

                else if (from == null && to != null && blobNameParts[1].Equals(to))
                {
                    var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                    var greetingBinary = await blobClient.DownloadContentAsync();
                    var greeting = greetingBinary.Value.Content.ToObjectFromJson<Greeting>();
                    greetings.Add(greeting);
                }

                else if (from != null && to == null && blob.Name.StartsWith($"{from}"))
                {
                    var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                    var greetingBinary = await blobClient.DownloadContentAsync();
                    var greeting = greetingBinary.Value.Content.ToObjectFromJson<Greeting>();
                    greetings.Add(greeting);
                }

                else if (from == null && to == null)
                {
                    var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                    var greetingBinary = await blobClient.DownloadContentAsync();
                    var greeting = greetingBinary.Value.Content.ToObjectFromJson<Greeting>();
                    greetings.Add(greeting);
                }           
            }
            
            return greetings;
        }

        //public async Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        //{
        //    var greetings = new List<Greeting>();
        //    var selectedGreetings = new List<Greeting>();
        //
        //    await blobs.FirstOrDefaultAsync(x => x.Name.StartsWith(id.ToString()));

        //    selectedGreetings =
        //        from g in greetings
        //        where g.From == from
        //        select g;
        //}
    }
}
