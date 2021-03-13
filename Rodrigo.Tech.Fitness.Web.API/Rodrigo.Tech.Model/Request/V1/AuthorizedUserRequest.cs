using Rodrigo.Tech.Model.Enums.V1;
using System.ComponentModel.DataAnnotations;

namespace Rodrigo.Tech.Model.Request.V1
{
    public class AuthorizedUserRequest
    {
        [Required]
        public LogInTypeEnum LogInTypeId { get; set; }

        [Required]
        public string AccessToken { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
