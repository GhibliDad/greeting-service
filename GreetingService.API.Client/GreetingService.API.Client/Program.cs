using System.Text.Json;
using System.Net.Http.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;

namespace GreetingService.API.Client;

public class GreetingServiceClient
{
    private static HttpClient _httpClient = new();

    private const string _getGreetingsCommand = "get greetings";
    private const string _getGreetingCommand = "get greeting ";
    private const string _writeGreetingCommand = "write greeting ";
    private const string _updateGreetingCommand = "update greeting ";
    private const string _deleteGreetingCommand = "delete greeting ";
    private const string _deleteAllGreetingsCommand = "delete all";
    private const string _exportGreetingsCommand = "export greetings";
    private const string _repeatCallsCommand = "repeat calls ";
    private static string _from = "Batman";
    private static string _to = "Superman";

    public static async Task Main(string[] args)
    {
        _httpClient.BaseAddress = new Uri("https://towa-appservice-dev.azurewebsites.net/");
        //_httpClient.BaseAddress = new Uri("http://localhost:5299/");

        Console.WriteLine(@"
  ▄████  ██▀███  ▓█████ ▓█████▄▄▄█████▓ ██▓ ███▄    █   ▄████   ██████  ▐██▌ 
 ██▒ ▀█▒▓██ ▒ ██▒▓█   ▀ ▓█   ▀▓  ██▒ ▓▒▓██▒ ██ ▀█   █  ██▒ ▀█▒▒██    ▒  ▐██▌ 
▒██░▄▄▄░▓██ ░▄█ ▒▒███   ▒███  ▒ ▓██░ ▒░▒██▒▓██  ▀█ ██▒▒██░▄▄▄░░ ▓██▄    ▐██▌ 
░▓█  ██▓▒██▀▀█▄  ▒▓█  ▄ ▒▓█  ▄░ ▓██▓ ░ ░██░▓██▒  ▐▌██▒░▓█  ██▓  ▒   ██▒ ▓██▒ 
░▒▓███▀▒░██▓ ▒██▒░▒████▒░▒████▒ ▒██▒ ░ ░██░▒██░   ▓██░░▒▓███▀▒▒██████▒▒ ▒▄▄  
 ░▒   ▒ ░ ▒▓ ░▒▓░░░ ▒░ ░░░ ▒░ ░ ▒ ░░   ░▓  ░ ▒░   ▒ ▒  ░▒   ▒ ▒ ▒▓▒ ▒ ░ ░▀▀▒ 
  ░   ░   ░▒ ░ ▒░ ░ ░  ░ ░ ░  ░   ░     ▒ ░░ ░░   ░ ▒░  ░   ░ ░ ░▒  ░ ░ ░  ░ 
░ ░   ░   ░░   ░    ░      ░    ░       ▒ ░   ░   ░ ░ ░ ░   ░ ░  ░  ░      ░ 
      ░    ░        ░  ░   ░  ░         ░           ░       ░       ░   ░                                                                                                                                                            
        ");
        Console.WriteLine("Welcome to command line Greeting client");
        Console.WriteLine("Enter name of greeting sender:");
        var from = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(from))
            _from = from;

        Console.WriteLine("Enter name of greeting recipient:");
        var to = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(to))
            _to = to;

        while (true)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine(_getGreetingsCommand);
            Console.WriteLine($"{_getGreetingCommand} [id]");
            Console.WriteLine($"{_writeGreetingCommand} [message]");
            Console.WriteLine($"{_updateGreetingCommand} [id] [message]");
            Console.WriteLine($"{_deleteGreetingCommand} [id]");
            Console.WriteLine($"{_deleteAllGreetingsCommand}");
            Console.WriteLine(_exportGreetingsCommand);
            Console.WriteLine($"{_repeatCallsCommand} [count]");


            Console.WriteLine("\nWrite command and press [enter] to execute");

            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Command cannot be empty\n");
                continue;
            }

            if (command.Equals(_getGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                await GetGreetingsAsync();
            }
            else if (command.StartsWith(_getGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idPart = command.Replace(_getGreetingCommand, "");
                if (Guid.TryParse(idPart, out var id))
                {
                    await GetGreetingAsync(id);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID\n");
                }
            }
            else if (command.StartsWith(_writeGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var message = command.Replace(_writeGreetingCommand, "");
                await WriteGreetingAsync(message);
            }
            else if (command.StartsWith(_updateGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idAndMessagePart = command.Replace(_updateGreetingCommand, "") ?? "";
                var idPart = idAndMessagePart.Trim().Split(" ").First();
                var messagePart = idAndMessagePart.Replace(idPart, "").Trim();

                if (Guid.TryParse(idPart, out var id))
                {
                    await UpdateGreetingAsync(id, messagePart);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID");
                }
            }
            else if (command.StartsWith(_deleteGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idPart = command.Replace(_deleteGreetingCommand, "").Trim();

                if (Guid.TryParse(idPart, out var id))
                {
                    await DeleteGreetingAsync(id);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID\n");
                }
            }
            else if (command.StartsWith(_deleteAllGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Are you Sure? Y/N");
                var response = Console.ReadLine();

                if (response.Contains("y", StringComparison.OrdinalIgnoreCase))
                {
                    await DeleteAllGreetingsAsync();
                }
                else if (response.Contains("n", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("Invalid answer");
                }
            }
            else if (command.StartsWith(_exportGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                await ExportGreetingsAsync();
            }
            else if (command.StartsWith(_repeatCallsCommand, StringComparison.OrdinalIgnoreCase))
            {
                var countPart = command.Replace(_repeatCallsCommand, "");

                if (int.TryParse(countPart, out var count))
                {
                    await RepeatCallsAsync(count);
                }
                else
                {
                    Console.WriteLine($"Could not parse {countPart} as int");
                }
            }
            else
            {
                Console.WriteLine("Command not recognized\n");
            }
        }
    }

    private static async Task<IEnumerable<Greeting>> GetGreetingsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Greeting/");
            var greetingsString = await response.Content.ReadAsStringAsync();
            var greetings = JsonSerializer.Deserialize<IList<Greeting>>(greetingsString);

            foreach (var greeting in greetings)
            {
                Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}");
            }

            Console.WriteLine();
            return greetings;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Get greetings failed: {ex.Message}\n");
        }

        return Enumerable.Empty<Greeting>();
    }

    private static async Task GetGreetingAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/greeting/{id}");
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var greeting = JsonSerializer.Deserialize<Greeting>(responseBody);
        Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}\n");
    }

    private static async Task WriteGreetingAsync(string message)
    {
        var greeting = new Greeting
        {
            from = _from,
            to = _to,
            message = message,
        };
        //var serialized = JsonSerializer.Serialize(greeting);
        //var content = new StringContent(serialized);
        //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var response = await _httpClient.PostAsJsonAsync("api/Greeting", greeting);

        Console.WriteLine($"Status: {response.StatusCode}");
    }

    private static async Task UpdateGreetingAsync(Guid id, string message)
    {
        var greeting = new Greeting
        {
            id = id,
            from = _from,
            to = _to,
            message = message,
        };
        //var serialized = JsonSerializer.Serialize(greeting);
        //var content = new StringContent(serialized);
        //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var response = await _httpClient.PutAsJsonAsync("api/Greeting", greeting);

        Console.WriteLine($"Status: {response.StatusCode}");
    }

    private static async Task DeleteGreetingAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"api/greeting/{id}");

        Console.WriteLine($"Greeting with ID: {id} deleted.");
    }

    private static async Task DeleteAllGreetingsAsync()
    {
        var response = await _httpClient.GetAsync("api/Greeting/");
        var greetingsString = await response.Content.ReadAsStringAsync();
        var greetings = JsonSerializer.Deserialize<IList<Greeting>>(greetingsString);

        if (greetings.Count == 0)
        {
            Console.WriteLine("There are no greeting to delete.");
        }
        else
        {
            greetings.Clear();
            //foreach (var greeting in greetings)
            //{
            //    var id = greeting.id;
            //    await _httpClient.DeleteAsync($"api/Greeting/{id}");
            //}
            Console.WriteLine("All greetings deleted.");
        }
    }

    private static async Task ExportGreetingsAsync()
    {
        var response = await _httpClient.GetAsync("api/Greeting/");
        response.EnsureSuccessStatusCode();
        var greetingsString = await response.Content.ReadAsStringAsync();
        var greetings = JsonSerializer.Deserialize<IList<Greeting>>(greetingsString);

        if (greetings != null)
        {
            var filename = "greetingExport.xml";
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };
            using var xmlWriter = XmlWriter.Create(filename, xmlWriterSettings);
            var serializer = new XmlSerializer(typeof(List<Greeting>));
            serializer.Serialize(xmlWriter, greetings);

            Console.WriteLine($"Exported {greetings.Count()} greetings to {filename}\n");
        }
        else
        {
            Console.WriteLine("There are no greetings to export");
        }
    }

    private static async Task RepeatCallsAsync(int count)
    {
        var greetings = await GetGreetingsAsync();
        var greeting = greetings.First();

        var jobs = new List<int>();
        for (int i = 1; i <= count; i++)
        {
            jobs.Add(i);
        }

        var stopwatch = Stopwatch.StartNew();

        foreach (var job in jobs)
        {
            var start = stopwatch.ElapsedMilliseconds;
            var response = await _httpClient.GetAsync($"api/greeting/{greeting.id}");
            var end = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Response: {response.StatusCode} - Call: {job} - latency: {end - start} ms - rate/s: {job / stopwatch.Elapsed.TotalSeconds}");
        }
    }
}