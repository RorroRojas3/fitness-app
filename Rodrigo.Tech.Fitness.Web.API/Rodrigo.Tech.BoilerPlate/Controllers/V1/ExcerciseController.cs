using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Fitness.Web.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Authorize]
    public class ExcerciseController : ControllerBase
    {
        private readonly ILogger _logger;

        public ExcerciseController(ILogger<ExcerciseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     Gets all excercises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExcercises()
        {
            return null;
        }

        /// <summary>
        ///     Gets excercise
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExcercise(Guid id)
        {
            return null;
        }

        /// <summary>
        ///     Creates excercise 
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "example",
        ///        "value": "example"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostExcercise([FromBody] object request)
        {
            return null;
        }

        /// <summary>
        ///     Updates excercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "example",
        ///        "value": "example"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutExcercise(Guid id, [FromBody] object request)
        {
            return null;
        }

        /// <summary>
        ///     Deletes excercise based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteExcercise(Guid id)
        {
            return null;
        }
    }
}
