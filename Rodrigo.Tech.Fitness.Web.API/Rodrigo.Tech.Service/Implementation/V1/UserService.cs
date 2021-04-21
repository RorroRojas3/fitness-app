using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Service.Implementation.V1
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        #region  User Creation
        /// <inheritdoc/>
        public async Task<AuthorizedUserResponse> PostAuhthorizedUser(AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserService)} - {nameof(PostAuhthorizedUser)} -" +
                $"Started, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");

            switch (request.LogInTypeId)
            {
                case LogInTypeEnum.MICROSOFT:
                    break;
                case LogInTypeEnum.GOOGLE:
                    GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.AccessToken);
                    break;
                case LogInTypeEnum.FACEBOOK:
                    break;
                default:
                    break;
            }


            _logger.LogInformation($"{nameof(UserService)} - {nameof(PostAuhthorizedUser)} -" +
                $"Finished, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");

            return null;
        }
        #endregion

        #region User Excercises
        /// <inheritdoc/>
        public async Task<IList<object>> GetUserExcercises()
        {
            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetUserExcercises)} - Started");

            await Task.CompletedTask;

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetUserExcercises)} - Finished");
            return null;
        }
        #endregion
    }
}