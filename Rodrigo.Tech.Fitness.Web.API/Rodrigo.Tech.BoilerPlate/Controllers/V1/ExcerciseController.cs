using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Service.Interface.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Fitness.Web.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    //[Authorize]
    public class ExcerciseController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IExcerciseService _excerciseService;

        public ExcerciseController(ILogger<ExcerciseController> logger,
                                    IExcerciseService excerciseService)
        {
            _logger = logger;
            _excerciseService = excerciseService;
        }

        /// <summary>
        ///     Gets all excercises
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IList<ExcerciseResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExcercises()
        {
            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(GetExcercises)} - Started");

            var result = await _excerciseService.GetExcercises();

            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(GetExcercises)} - Finished");
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        /// <summary>
        ///     Gets excercise
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ExcerciseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExcercise(Guid id)
        {
            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(GetExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _excerciseService.GetExcercise(id);

            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(GetExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return StatusCode(result.HttpStatusCode, result.Data);
        }

        /// <summary>
        ///     Creates excercise 
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "name": "string",
        ///        "description": "string"
        ///        "type": int
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ExcerciseResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostExcercise([FromBody] ExcerciseRequest request)
        {
            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(PostExcercise)} - Started, " +
                 $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");

            var result = await _excerciseService.PostExcercise(request);

            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(PostExcercise)} - Finished, " +
                 $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return StatusCode(result.HttpStatusCode, result.Data);
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
        ///        "name": "string",
        ///        "description": "string"
        ///        "type": int
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ExcerciseRequest), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutExcercise(Guid id, [FromBody] ExcerciseRequest request)
        {
            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(PutExcercise)} - Started, " +
                 $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");

            var result = await _excerciseService.PutExcercise(id, request);

            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(PutExcercise)} - Finished, " +
                 $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return StatusCode(result.HttpStatusCode, result.Data);
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
            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(DeleteExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _excerciseService.DeleteExcercise(id);

            _logger.LogInformation($"{nameof(ExcerciseController)} - {nameof(DeleteExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return StatusCode(result.HttpStatusCode, result.Data);
        }
    }
}
