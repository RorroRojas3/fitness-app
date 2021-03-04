using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Fitness.Web.API.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Authorize]
    public class FileController : Controller
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileService;

        public FileController(ILogger<FileController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        /// <summary>
        ///     Gets file based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Info/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFileInfo(Guid id)
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetFileInfo)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _fileService.GetFileInfo(id);

            if (result == null)
            {
                _logger.LogError($"{nameof(FileController)} - {nameof(GetFileInfo)} - Not found, " +
               $"{nameof(id)}: {id}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetFileInfo)} - Finished, " +
               $"{nameof(id)}: {id}");
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        ///     Gets file based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Download/{id}")]
        [Produces("application/octet-stream")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFileDownload(Guid id)
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetFileDownload)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _fileService.GetFileDownload(id);

            if (result == null)
            {
                _logger.LogError($"{nameof(FileController)} - {nameof(GetFileDownload)} - Not found, " +
               $"{nameof(id)}: {id}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            Stream stream = new MemoryStream(result.Data);

            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetFileDownload)} - Finished, " +
               $"{nameof(id)}: {id}");
            return new FileStreamResult(stream, "application/octet-stream");
        }

        /// <summary>
        ///     Creates file
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostFile(IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(PostFile)} - Started");

            var result = await _fileService.PostFile(formFile);

            _logger.LogInformation($"{nameof(FileController)} - {nameof(PostFile)} - Finished");
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        ///     Updates file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutFile(Guid id, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(PutFile)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _fileService.UpdateFile(id, formFile);

            if (result == null)
            {
                _logger.LogInformation($"{nameof(FileController)} - {nameof(PutFile)} - Not found, " +
                $"{nameof(id)}: {id}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"{nameof(FileController)} - {nameof(PutFile)} - Finished, " +
                $"{nameof(id)}: {id}");
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        ///     Deletes file based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(DeleteFile)} - Started, " +
                $"{nameof(id)}: {id}");

            var result = await _fileService.DeleteFile(id);

            if (!result)
            {
                _logger.LogInformation($"{nameof(FileController)} - {nameof(DeleteFile)} - Not found, " +
                $"{nameof(id)}: {id}");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"{nameof(FileController)} - {nameof(DeleteFile)} - Finished, " +
                $"{nameof(id)}: {id}");
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        ///     Get all files
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FileResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFiles()
        {
            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetAllFiles)} - Started");

            var result = await _fileService.GetAllFiles();

            if (result == null)
            {
                _logger.LogError($"{nameof(FileController)} - {nameof(GetAllFiles)} - Not found");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation($"{nameof(FileController)} - {nameof(GetAllFiles)} - Finished");
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}