

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Text.Json;

namespace DuckC2.Server.Models
{
    public class HttpListenerImplantHandler
    {
        public async Task<Duckling> HandleCheckin(HttpContext context)
        {

            var metadata = ExtractMetadata(context.Request.Headers);

            if (metadata == null)
                return null;

        
            return new Duckling(); ;
        }


        private AgentMetadata? ExtractMetadata(IHeaderDictionary headers)
        {
            if (!headers.TryGetValue("Authorization", out var encodedMetadata))
                return null;

            encodedMetadata = encodedMetadata.ToString().Remove(0, 7);

            if (String.IsNullOrWhiteSpace(encodedMetadata))
                return null;

            var json = Encoding.UTF8.GetString(Convert.FromBase64String(encodedMetadata));
            if (String.IsNullOrWhiteSpace(json))
                return null;

            return  JsonSerializer.Deserialize<AgentMetadata>(json);

        }
    }
}
