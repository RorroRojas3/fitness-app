using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class MicrosoftProfileResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty("businessPhones")]
        public List<object> BusinessPhones { get; set; }

        [JsonProperty("jobTitle")]
        public object JobTitle { get; set; }

        [JsonProperty("mail")]
        public object Mail { get; set; }

        [JsonProperty("mobilePhone")]
        public object MobilePhone { get; set; }

        [JsonProperty("officeLocation")]
        public object OfficeLocation { get; set; }

        [JsonProperty("preferredLanguage")]
        public object PreferredLanguage { get; set; }
    }
}