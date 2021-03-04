using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables.Context;
using Rodrigo.Tech.Service.Interface.V1;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Implementation
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Excercise> _excerciseRepository;

        public ExcerciseService(ILogger<ExcerciseService> logger,
                                IMapper mapper,
                                IRepository<Excercise> excerciseRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _excerciseRepository = excerciseRepository;

        }

        /// <inheritdoc/>
        public async Task<ApiResponse<bool>> DeleteExcercise(Guid id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var excercise = await _excerciseRepository.Delete(id);

            if (!excercise)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - " +
                    $"{nameof(excercise)} not found, " +
                    $"{nameof(id)}: {id}");
                return new ApiResponse<bool>(HttpStatusCode.NotFound);
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return new ApiResponse<bool>(HttpStatusCode.OK, true);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ExcerciseResponse>> GetExcercise(Guid id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var excercise = await _excerciseRepository.Get(id);

            if (excercise == null)
            {
                _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Started, " +
                    $"{nameof(excercise)} not found, " +
                    $"{nameof(id)}: {id}");
                return new ApiResponse<ExcerciseResponse>(HttpStatusCode.NotFound);
            }

            var mappedExcercise = _mapper.Map<ExcerciseResponse>(excercise);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return new ApiResponse<ExcerciseResponse>(HttpStatusCode.OK, mappedExcercise);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<IList<ExcerciseResponse>>> GetExcercises()
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - Started");

            var excercises = await _excerciseRepository.GetAll();

            if (excercises.Count == 0)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - " +
                    $"{nameof(excercises)} not found");
                return new ApiResponse<IList<ExcerciseResponse>>(HttpStatusCode.NotFound);
            }

            var mappedExcercises = _mapper.Map<IList<ExcerciseResponse>>(excercises);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - Finished");
            return new ApiResponse<IList<ExcerciseResponse>>(HttpStatusCode.OK, mappedExcercises);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ExcerciseResponse>> PostExcercise(ExcerciseRequest request)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PostExcercise)} - Started, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");

            var excercise = _mapper.Map<Excercise>(request);
            excercise = await _excerciseRepository.Add(excercise);

            if (excercise == null)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(PostExcercise)} - " +
                    $"{nameof(excercise)} already exists" +
                    $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
                return new ApiResponse<ExcerciseResponse>(HttpStatusCode.Conflict);
            }

            var mappedExcercise = _mapper.Map<ExcerciseResponse>(excercise);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PostExcercise)} - Finished, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return new ApiResponse<ExcerciseResponse>(HttpStatusCode.Created, mappedExcercise);
        }

        /// <inheritdoc/>
        public async Task<ApiResponse<ExcerciseResponse>> PutExcercise(Guid id, ExcerciseRequest request)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcercise)} - Started, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");

            var excercise = await _excerciseRepository.Get(id);

            if (excercise == null)
            {
                _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcercise)} - " +
                    $"{nameof(excercise)} not found, " +
                    $"{nameof(id)}: {id}, " +
                    $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
                return new ApiResponse<ExcerciseResponse>(HttpStatusCode.NotFound);
            }

            _mapper.Map(request, excercise);
            excercise = await _excerciseRepository.Update(excercise);
            var mappedExcercise = _mapper.Map<ExcerciseResponse>(excercise);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcercise)} - Finished, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return new ApiResponse<ExcerciseResponse>(HttpStatusCode.OK, mappedExcercise);
        }
    }
}
