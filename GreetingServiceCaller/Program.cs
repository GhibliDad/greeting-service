using System.Text.Json;
using System.Net.Http.Json;
using System.Xml;
using System.Xml.Serialization;

namespace client_2_caller;

public class Program
{
    private static HttpClient _httpClient = new();

    public static async Task Main(string[] args)
    {
        await ThreadsafeStringTimer(100);
    }
    public static async Task ThreadsafeStringTimer(int count)
    {
        var greetings = await GetGreetingAsync();
    }
}