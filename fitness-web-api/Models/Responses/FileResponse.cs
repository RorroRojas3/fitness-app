using System;
using Newtonsoft.Json;

namespace fitness_web_api.Models.Responses
{
    public class FileResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}