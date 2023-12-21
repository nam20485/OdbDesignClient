using System.Text.Json;
using System.Text.Json.Serialization;

namespace Odb.Client.Lib
{
    public class OdbDesignHttpClient : HttpClient
    {
        private const int SECONDS_PER_MINUTE = 60;

        private readonly int _requestTimeoutInMinutes = 20;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true)
            }
        };

        public OdbDesignHttpClient(Uri apiUri)
        {
            BaseAddress = apiUri;
            Timeout = TimeSpan.FromSeconds(_requestTimeoutInMinutes * SECONDS_PER_MINUTE);
        }

        public async Task<FileArchive> FetchFileArchiveAsync(string name)
        {
            FileArchive fileArchive = null;
            var response = await GetAsync($"filemodel/{name}");
            if (response.IsSuccessStatusCode)
            {
                const bool useLocalCopy = false;

                Stream stream = null;
                var path = $"{name}.json";
                if (useLocalCopy)
                {
                    stream = new FileStream(path, FileMode.Open);                    
                }
                else
                {
                    stream = await response.Content.ReadAsStreamAsync();                    
                    File.WriteAllText(path, await response.Content.ReadAsStringAsync());                    
                }
                fileArchive = await JsonSerializer.DeserializeAsync<FileArchive>(stream, _jsonSerializerOptions);

            }
            return fileArchive;
        }

        public FileArchive FetchFileArchive(string name)
        {
            return FetchFileArchiveAsync(name).GetAwaiter().GetResult();
        }
    }
}
