using System.Collections.Generic;
using System.Threading.Tasks;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Tables.Context;

namespace Rodrigo.Tech.Service.Interface.V1
{
    public interface IUserService
    {
        /// <summary>
        ///     Checks if Access token is valid from Microsoft/Google/Facebook
        ///     if true, returns the JWT token for the application, 
        ///     otherwise throws StatusCodeException
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AuthorizedUserResponse> PostAuthorizedUser(AuthorizedUserRequest request);

        /// <summary>
        ///     Gets the user weekly excercies
        /// </summary>
        /// <returns></returns>
        Task<IList<object>> GetUserExcercises();
    }
}