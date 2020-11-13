using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using fitness_web_api.Services.Interface;

namespace fitness_web_api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class AzureBlobStorageController : Controller
    {
        private readonly ILogger _logger;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public AzureBlobStorageController(ILogger<AzureBlobStorageController> logger,
                                         IAzureBlobStorageService azureBlobStorageService)
        {
            _logger = logger;
            _azureBlobStorageService = azureBlobStorageService;
        }

        /// <summary>
        ///     Creates container in Azure Blob Storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{containerName}")]
        public async Task<IActionResult> PostContainer(string containerName)
        {
            _logger.LogInformation($"AzureBlobStorage - PostContainer - Started");

            if (string.IsNullOrEmpty(containerName))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "Container name not provided");
            }

            var result = await _azureBlobStorageService.CreateContainer(containerName);

            return null;
        }

        /// <summary>
        ///     Deletes Container from Azure Blob Storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{containerName}")]
        public async Task<IActionResult> DeleteContainer(string containerName)
        {
            _logger.LogInformation($"AzureBlobStorage - DeleteContainer - Started");

            if (string.IsNullOrEmpty(containerName))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "Container name not provided");
            }

            var result = await _azureBlobStorageService.DeleteContainer(containerName);

            return null;
        }

        /// <summary>
        ///     Gets File from Azure Blob Storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("File/{containerName}/{fileName}")]
        public async Task<IActionResult> GetFile([FromRoute] string containerName, string fileName)
        {
            _logger.LogInformation($"AzureBlobStorage - GetFile - Started");

            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(fileName))
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, $"Container name or File name not provided");
            }

            var result = await _azureBlobStorageService.GetFile(containerName, fileName);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        ///     Uploads file to Azure Blob Storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("File/{containerName}")]
        public async Task<IActionResult> PostFile(string containerName, IFormFile formFile)
        {
            _logger.LogInformation($"AzureBlobStorage - PostFile - Started");

            if (string.IsNullOrEmpty(containerName) || formFile == null)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, "Container name or File not provided");
            }

            var result = await _azureBlobStorageService.UploadFile(containerName, formFile);

            return null;
        }
    }
}