using Odb.Client.Lib;

internal class Program
{
    public static int Main(string[] args)
    {
        Console.WriteLine("OdbDesign Client");

        if (args.Length > 1)
        {            
            if (!string.IsNullOrWhiteSpace(args[0]) &&
                !string.IsNullOrWhiteSpace(args[1]))
            {
                var apiUri = new Uri(args[0]);
                var client = new OdbDesignClient(apiUri);

                var designName = args[1];
                var fileArchive = client.FetchFileArchive(designName);

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