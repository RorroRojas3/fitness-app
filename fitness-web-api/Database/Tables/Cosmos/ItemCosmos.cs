using fitness_web_api.Database.Repository.Interface;
using Newtonsoft.Json;
using System;

namespace fitness_web_api.Database.Tables.Cosmos
{
    [Serializable]
    public class ItemCosmos : IEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
