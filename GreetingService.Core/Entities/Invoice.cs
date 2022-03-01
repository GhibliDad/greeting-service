using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string User { get; set; }
        public List<Greeting> Greetings { get; set;}
        public int Year { get; set; }
        public int Month { get; set; }
        public int CostPerGreeting { get; set; } = 20;
        public int TotalCost { get; set; }
        public string Currency { get; set; } = "SEK";
    }
}
