using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Request.V1;
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

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///      Checks if user is registered 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Check")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAuthorizedUser([FromBody] AuthorizedUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostAuthorizedUser)} - Started");

            await Task.CompletedTask;

            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostAuthorizedUser)} - Finished");
            return StatusCode(StatusCodes.Status200OK, null);
        }


        /// <summary>
        ///     Creates user in DB
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCreateUser(NewUserRequest request)
        {
            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostCreateUser)} - Started");

            await Task.CompletedTask;

            _logger.LogInformation($"{nameof(UserController)} - {nameof(PostCreateUser)} - Finished");
            return StatusCode(StatusCodes.Status201Created, null);
        }
    }
}
