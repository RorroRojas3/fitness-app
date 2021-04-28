using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Exceptions;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Model.Settings;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables.Context;
using Rodrigo.Tech.Service.Interface.Common;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Service.Implementation.V1
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        private readonly IRepository<User> _userRepository;

        private readonly ITokenService _tokenService;

        private readonly IHttpClientService _httpClientService;

        public UserService(ILogger<UserService> logger,
                            IMapper mapper,
                            IConfiguration configuration,
                            IRepository<User> userRepository,
                            ITokenService tokenService,
                            IHttpClientService httpClientService
                            )
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _httpClientService = httpClientService;
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
                    userResponse = await GetMicrosoftUser(request);
                    break;
                case LogInTypeEnum.GOOGLE:
                    userResponse = await GetGoogleUser(request);
                    break;
                case LogInTypeEnum.FACEBOOK:
                    userResponse = await GetFacebookUser(request);
                    break;
                default:
                    break;
            }

            var user = await _userRepository.GetWithExpression(x => x.Email.Equals(userResponse.Email));
            if (user == null)
            {
                _logger.LogInformation($"{nameof(UserService)} - {nameof(PostAuthorizedUser)} -" +
                    $"{nameof(user)} not found, creating user, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
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
        ///     Gets user's information from Microsoft
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<UserResponse> GetMicrosoftUser(AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - Started, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            var microsoftConfig = _configuration.GetSection(ConfigurationConstants.MicrosoftGraph).Get<MicrosoftGraph>();

            var url = $"{microsoftConfig.BaseUrl}/{microsoftConfig.Profile}";
            var headers = _httpClientService.GetBearerJWTAuthorizationHeader(request.AccessToken);

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - " +
                $"Calling {url}, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            var response = await _httpClientService.Json(url, HttpMethod.Get, headers);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - " +
                    $"Unsuccesfull call to {url}, " +
                    $"{nameof(response.StatusCode)}: {response.StatusCode}, " +
                    $"{nameof(responseContent)}: {responseContent}, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
                throw new StatusCodeException(HttpStatusCode.BadRequest, $"Unable to obtain user's profile information");
            }

            var microsoftProfileResponse = JsonConvert.DeserializeObject<MicrosoftProfileResponse>(responseContent);
            var userResponse = _mapper.Map<UserResponse>(microsoftProfileResponse);

            // url = $"{microsoftConfig.BaseUrl}/{microsoftConfig.Photo}";
            // _logger.LogInformation($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - " +
            //     $"Calling {url}, " +
            //     $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            // response = await _httpClientService.Json(url, HttpMethod.Get, headers);

            // responseContent = await response.Content.ReadAsStringAsync();

            // if (!response.IsSuccessStatusCode)
            // {
            //     _logger.LogError($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - " +
            //         $"Unsuccesfull call to {url}, " +
            //         $"{nameof(response.StatusCode)}: {response.StatusCode}, " +
            //         $"{nameof(responseContent)}: {responseContent}, " +
            //         $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            //     throw new StatusCodeException(HttpStatusCode.BadRequest, $"Unable to obtain user's profile picture");
            // }

            userResponse.Picture = "byteArray";

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetMicrosoftUser)} - Finished, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            return userResponse;
        }

        /// <summary>
        ///     Validates access token and obtains user information
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<UserResponse> GetFacebookUser(AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetFacebookUser)} - Started, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");

            var facebookConfig = _configuration.GetSection(ConfigurationConstants.Facebook).Get<Facebook>();
            var url = $"{facebookConfig.BaseUrl}/{facebookConfig.ValidateToken}";
            url = string.Format(url, request.AccessToken, request.AccessToken);

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                $"Calling {url}, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            var response = await _httpClientService.Json(url, HttpMethod.Get);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                    $"Unsuccesfull call to {url}, " +
                    $"{nameof(response.StatusCode)}: {response.StatusCode}, " +
                    $"{nameof(responseContent)}: {responseContent}, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
                throw new StatusCodeException(HttpStatusCode.Forbidden, $"Invalid {nameof(request.AccessToken)}");
            }

            var fbValidTokenResponse = JsonConvert.DeserializeObject<FacebookValidateTokenResponse>(responseContent);

            if (!fbValidTokenResponse.Data.IsValid)
            {
                _logger.LogError($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                    $"Facebook {nameof(request.AccessToken)} not valid, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
                throw new StatusCodeException(HttpStatusCode.Forbidden, $"{nameof(request.AccessToken)} not valid");
            }

            if (!fbValidTokenResponse.Data.AppId.Equals(Environment.GetEnvironmentVariable(EnvironmentConstants.FACEBOOK_CLIENTID)))
            {
                _logger.LogError($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                    $"Facebook {nameof(fbValidTokenResponse.Data.AppId)} not valid, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
                throw new StatusCodeException(HttpStatusCode.Forbidden, $"{nameof(fbValidTokenResponse.Data.AppId)} not valid");
            }

            url = $"{facebookConfig.BaseUrl}/{facebookConfig.UserInformation}";
            url = string.Format(url, fbValidTokenResponse.Data.UserId, request.AccessToken);

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                $"Calling {url}, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            response = await _httpClientService.Json(url, HttpMethod.Get);

            responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"{nameof(UserService)} - {nameof(GetFacebookUser)} - " +
                    $"Unsuccesfull call to {url}, " +
                    $"{nameof(response.StatusCode)}: {response.StatusCode}, " +
                    $"{nameof(responseContent)}: {responseContent}, " +
                    $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
                throw new StatusCodeException(HttpStatusCode.BadRequest, $"Could not obtain user's infomation from Facebook");
            }

            var fbUserInfo = JsonConvert.DeserializeObject<FacebookUserInformationResponse>(responseContent);

            _logger.LogInformation($"{nameof(UserService)} - {nameof(GetFacebookUser)} - Finished, " +
                $"{nameof(request)}: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<UserResponse>(fbUserInfo);
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