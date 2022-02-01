using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.API.Client
{
    public class Greeting
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime timestamp { get; set; } = DateTime.Now;
        public string from { get; set; }
        public string to { get; set; }
        public string message { get; set; }
    }
}
