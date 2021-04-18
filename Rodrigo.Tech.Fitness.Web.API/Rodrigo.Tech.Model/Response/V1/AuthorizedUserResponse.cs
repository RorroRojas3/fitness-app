namespace Rodrigo.Tech.Model.Response.V1
{
    public class AuthorizedUserResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string JWTToken { get; set; }
    }
}