using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Service.Interface.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Fitness.Web.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,
                                IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        ///      Checks if user is registered 
        /// Sample request:
        ///
        ///     {
        ///        "logInTypeId": int,
        ///        "accessToken": "string"
        ///        "emailAddress": "string"
        ///     }
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AuthorizedUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAuthorizedUser([FromBody] AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostAuthorizedUser)} - Started");

            var authorizedUser = await _userService.PostAuhthorizedUser(request);

            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostAuthorizedUser)} - Finished");
            return StatusCode(StatusCodes.Status200OK, authorizedUser);
        }

        /// <summary>
        ///     Gets user's weekly excercises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Excercises")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserExcercises()
        {
            _logger.LogInformation($"{nameof(UserController)} - {nameof(GetUserExcercises)} - Started");

            var userExcercises = await _userService.GetUserExcercises();

            _logger.LogInformation($"{nameof(UserController)} - {nameof(GetUserExcercises)} - Finished");
            return StatusCode(StatusCodes.Status200OK, userExcercises);
        }
    }
}
