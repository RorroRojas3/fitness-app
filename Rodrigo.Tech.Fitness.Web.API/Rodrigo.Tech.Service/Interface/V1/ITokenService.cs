

using Rodrigo.Tech.Repository.Tables.Context;

namespace Rodrigo.Tech.Service.Interface.V1
{
    public interface ITokenService
    {
        /// <summary>
        ///     Creates JWT token for valid Microsoft/Google/Facebook user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateJWTToken(User user);

        /// <summary>
        ///     Validates JWT token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        bool ValidateJWTToken(string jwt);
    }
}