using Azure.Storage.Blobs;
using GreetingService.Core;
using GreetingService.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class BlobUserService : IUserService
    {
        private const string _blobContainerName = "users";
        private const string _blobName = "users.json";
        private readonly BlobContainerClient _blobContainerClient;
        private readonly ILogger<BlobUserService> _logger;

        public BlobUserService(IConfiguration configuration, ILogger<BlobUserService> logger)
        {
            var connectionString = configuration["LogStorageAccount"];
            _blobContainerClient = new BlobContainerClient(connectionString, _blobContainerName);
            _blobContainerClient.CreateIfNotExists();
            _logger = logger;
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var blobClient = _blobContainerClient.GetBlobClient(_blobName);
            if (!blobClient.Exists())
                return false;
            
            var blobContent = blobClient.DownloadContent();
            var usersDictionary = blobContent.Value.Content.ToObjectFromJson<IDictionary<string, string>>();

            if (usersDictionary.ContainsKey(username))
            {
                if (usersDictionary[username] != null && usersDictionary[username] == password)
                {
                    _logger.LogInformation("Valid credentials for {username}", username);
                    return true;
                }

                _logger.LogWarning("Invalid credentials for {username}", username);
                return false;
            }

            return false;
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
