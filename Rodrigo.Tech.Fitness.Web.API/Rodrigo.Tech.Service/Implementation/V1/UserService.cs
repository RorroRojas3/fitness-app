using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Exceptions;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables.Context;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Service.Implementation.V1
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;

        private readonly ITokenService _tokenService;

        public UserService(ILogger<UserService> logger,
                            IMapper mapper,
                            IRepository<User> userRepository,
                            ITokenService tokenService)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        #region  User Creation
        /// <inheritdoc/>
        public async Task<AuthorizedUserResponse> PostAuthorizedUser(AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserService)} - {nameof(PostAuthorizedUser)} -" +
                $"Started, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");

            UserResponse userResponse = null;
            switch (request.LogInTypeId)
            {
                case LogInTypeEnum.MICROSOFT:
                    break;
                case LogInTypeEnum.GOOGLE:
                    userResponse = await GetGoogleUser(request);
                    break;
                case LogInTypeEnum.FACEBOOK:
                    break;
                default:
                    break;
            }

            var user = await _userRepository.GetWithExpression(x => x.Email.Equals(userResponse.Email));
            if (user == null)
            {
                var newUser = _mapper.Map<User>(userResponse);
                await _userRepository.Add(newUser);
                user = newUser;
            }

            var authorizedUserResponse = _mapper.Map<AuthorizedUserResponse>(user);
            authorizedUserResponse.JWTToken = _tokenService.CreateJWTToken(user);

            _logger.LogInformation($"{nameof(UserService)} - {nameof(PostAuthorizedUser)} -" +
                $"Finished, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");

            return authorizedUserResponse;
        }

        /// <summary>
        ///     Gets the user's information from Google Sign in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<UserResponse> GetGoogleUser(AuthorizedUserRequest request)
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.AccessToken);
            if (!payload.Audience.Equals(Environment.GetEnvironmentVariable(EnvironmentConstants.GOOGLE_CLIENTID)))
            {
                throw new StatusCodeException(HttpStatusCode.Forbidden, $"{nameof(EnvironmentConstants.GOOGLE_CLIENTID)} not valid");
            }
            if (!payload.EmailVerified)
            {
                throw new StatusCodeException(HttpStatusCode.Forbidden, $"{nameof(payload.EmailVerified)} is false");
            }

            return _mapper.Map<UserResponse>(payload);
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