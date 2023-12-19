using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
//using System.Js

namespace Odb.Client.Lib
{
    public class OdbDesignClient : HttpClient
    {
        public OdbDesignClient(Uri apiUri)
        {
            BaseAddress = apiUri;
        }

        public async Task<FileArchive> FetchFileArchiveAsync(string name)
        {
            FileArchive fileArchive = null;
            var response = await GetAsync($"filemodel/{name}");
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                fileArchive = await JsonSerializer.DeserializeAsync<FileArchive>(stream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,                    
                });                
            }
            return fileArchive;
        }

        public FileArchive FetchFileArchive(string name)
        {
            return FetchFileArchiveAsync(name).GetAwaiter().GetResult();
        }
    }
}
