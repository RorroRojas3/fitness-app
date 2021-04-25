using Newtonsoft.Json;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class FacebookUserInformationResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    public class Picture
    {
        [JsonProperty("data")]
        public PictureData Data { get; set; }
    }

    public class PictureData
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}