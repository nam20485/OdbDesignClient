using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Odb.Client.Lib
{
    public class OdbDesignClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OdbDesignClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FileArchive> FetchFileArchiveAsync(string name)
        {            
            FileArchive fileArchive = null;

            using var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"filemodel/{name}");
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
                //fileArchive = await JsonSerializer.DeserializeAsync<FileArchive>(stream, _jsonSerializerOptions);

            }

            return fileArchive;
        }
    }
}
