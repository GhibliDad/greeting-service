using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace GreetingService.Infrastructure.Test
{
    public class FileGreetingRepositoryTest
    {
        public FileGreetingRepository _repository { get; set; }

        private readonly string _filePath;
        private readonly List<Greeting> _testData;
        
        public FileGreetingRepositoryTest()
        {
            _filePath = $"greeting_unit_test_{DateTime.Now:yyyyMMddHHmmss}.json";
            _repository = new FileGreetingRepository(_filePath);

            _testData = new List<Greeting>
            {
                new Greeting
                {
                    From = "from1",
                    To = "to1",
                    Message = "message1",
                },
                new Greeting
                {
                    From = "from2",
                    To = "to2",
                    Message = "message2",
                },
                new Greeting
                {
                    From = "from3",
                    To = "to3",
                    Message = "message3",
                },
                new Greeting
                {
                    From = "from4",
                    To = "to4",
                    Message = "message4",
                },
            };

            File.WriteAllText(_filePath, JsonSerializer.Serialize(_testData, new JsonSerializerOptions { WriteIndented = true }));
        }

        [Fact]
        public void get_should_return_empty_collection()
        {
            var repo = new FileGreetingRepository();
            Assert.NotNull(repo);
        }

        [Fact]
        public void get_should_return_correct_greeting()
        {

        }

        [Fact]
        public void post_should_persist_to_file()
        {

        }

        [Fact]
        public void update_should_persist_to_file()
        {

        }
    }
}
