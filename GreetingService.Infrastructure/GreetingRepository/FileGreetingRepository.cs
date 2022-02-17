using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class FileGreetingRepository : IGreetingRepository
    {
        private readonly string _filePath = "greetings.json";
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true, };

        public FileGreetingRepository(string filePath)
        {
            //if (string.IsNullOrEmpty(filePath))
            //    throw new ArgumentNullException();

            if (!File.Exists(filePath))
                File.WriteAllText(filePath, "[]");     //init file with empty json array

            _filePath = filePath;
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(content);
            return greetings;
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(content);
            return greetings?.FirstOrDefault(x => x.Id == id);
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
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(content);
            var existingGreeting = greetings?.FirstOrDefault(x => x.Id == greeting.Id);

            if (existingGreeting == null)
                throw new Exception($"Greeting with ID: {greeting.Id} does not exist");

            existingGreeting.To = greeting.To;
            existingGreeting.From = greeting.From;
            existingGreeting.Message = greeting.Message;

            File.WriteAllText(_filePath, JsonSerializer.Serialize(greetings, _jsonSerializerOptions));
        }

        public async Task DeleteAsync(Guid id)
        {
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<List<Greeting>>(content);
            var greeting = greetings?.FirstOrDefault(x => x.Id == id);

            if (greeting == null)
                throw new Exception($"Greeting with ID: {id} does not exist");

            greetings.Remove(greeting);

            File.WriteAllText(_filePath, JsonSerializer.Serialize(greetings, _jsonSerializerOptions));
        }

        public async Task DeleteAllAsync()
        {
            var content = File.ReadAllText(_filePath);
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(content);

            greetings?.Clear();

            File.WriteAllText(_filePath, JsonSerializer.Serialize(greetings, _jsonSerializerOptions));
        }

        public Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            throw new NotImplementedException();
        }
    }
}