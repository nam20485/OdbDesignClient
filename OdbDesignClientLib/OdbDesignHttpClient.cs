using System.Text.Json;
using System.Text.Json.Serialization;

namespace Odb.Client.Lib
{
    public class OdbDesignHttpClient
    {        
        private readonly HttpClient _httpClient;

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

        public OdbDesignHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;            
        }

        public async Task<FileArchive> FetchFileArchiveAsync(string name)
        {
            FileArchive fileArchive = null;

            var response = await _httpClient.GetAsync($"filemodel/{name}");
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
                    File.WriteAllText(path, await response.Content.ReadAsStringAsync());
                    stream = await response.Content.ReadAsStreamAsync();                                     
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
