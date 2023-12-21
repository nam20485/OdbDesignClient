using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Odb.Client.Lib;

internal class Program
{    
    private static readonly int _requestTimeoutInMinutes = 20;

    private const int SECONDS_PER_MINUTE = 60;

    public static int Main(string[] args)
    {
        Console.WriteLine("OdbDesign Client");

        if (args.Length > 1)
        {            
            if (!string.IsNullOrWhiteSpace(args[0]) &&
                !string.IsNullOrWhiteSpace(args[1]))
            {
                var apiUri = new Uri(args[0]);
                var designName = args[1];

                using var httpClient = new HttpClient()
                {
                    BaseAddress = apiUri,
                    Timeout = TimeSpan.FromSeconds(_requestTimeoutInMinutes * SECONDS_PER_MINUTE)
                };

                var odbDesignClient = new OdbDesignHttpClient(httpClient);                
                var fileArchive = odbDesignClient.FetchFileArchive(designName);

                return 0;                
            }
        }

        PrintUsage();
        return 1;
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Invalid arguments...");
        Console.WriteLine();
        Console.WriteLine("Usage: OdbDesignClient <api-uri> <design-name>");
    }
}