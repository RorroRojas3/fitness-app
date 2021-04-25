using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class FacebookValidateTokenResponse
    {
        [JsonProperty("data")]
        public ValidateTokenData Data { get; set; }
    }

    public class ValidateTokenData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("data_access_expires_at")]
        public int DataAccessExpiresAt { get; set; }

        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("auth_type")]
        public string AuthType { get; set; }
    }
}