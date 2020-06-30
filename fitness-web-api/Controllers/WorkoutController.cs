using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace fitness_web_api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WorkoutController : Controller
    {
        // Private variables
        private readonly ILogger _logger;

        // WorkoutController constructor with DI
        public WorkoutController(ILogger<WorkoutController> logger)
        {
            _logger = logger;
        }

        // Gets list of all chest excercises
        [HttpGet]
        public async Task<IActionResult> GetChestExcercises()
        {
            try
            {
                _logger.LogInformation($"GetChestExcercises - Started");

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"GetChestExcercies - Failed: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Servcer error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostChestExcercises()
        {
            try
            {
                _logger.LogInformation($"PostChestExcercises - Started");

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"PostChestExcercises - Failed: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Servcer error");
            }
        }
    }