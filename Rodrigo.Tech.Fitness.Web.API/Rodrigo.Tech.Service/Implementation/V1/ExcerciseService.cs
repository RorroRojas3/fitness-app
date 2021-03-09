using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Exceptions;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Tables.Context;
using Rodrigo.Tech.Service.Interface.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Implementation.V1
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Excercise> _excerciseRepository;
        private readonly IRepository<ExcerciseTypeIcon> _exerciseTypeIconRepository;

        public ExcerciseService(ILogger<ExcerciseService> logger,
                                IMapper mapper,
                                IRepository<Excercise> excerciseRepository, 
                                IRepository<ExcerciseTypeIcon> excerciseTypeIconRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _excerciseRepository = excerciseRepository;
            _exerciseTypeIconRepository = excerciseTypeIconRepository;
        }

        #region Excercise
        /// <inheritdoc/>
        public async Task<bool> DeleteExcercise(Guid id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var excercise = await _excerciseRepository.Delete(id);

            if (!excercise)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - " +
                    $"{nameof(excercise)} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excercise)} not found");
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return true;
        }

        /// <inheritdoc/>
        public async Task<ExcerciseResponse> GetExcercise(Guid id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Started, " +
                $"{nameof(id)}: {id}");

            var excercise = await _excerciseRepository.Get(id);

            if (excercise == null)
            {
                _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Started, " +
                    $"{nameof(excercise)} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excercise)} not found");
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercise)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<ExcerciseResponse>(excercise); ;
        }

        /// <inheritdoc/>
        public async Task<IList<ExcerciseResponse>> GetExcercises()
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - Started");

            var excercises = await _excerciseRepository.GetAll();

            if (excercises.Count == 0)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - " +
                    $"{nameof(excercises)} not found");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excercises)} not found");
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcercises)} - Finished");
            return _mapper.Map<IList<ExcerciseResponse>>(excercises);
        }

        /// <inheritdoc/>
        public async Task<ExcerciseResponse> PostExcercise(ExcerciseRequest request)
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
                throw new StatusCodeException(HttpStatusCode.Conflict, $"{nameof(excercise)} already exists");
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PostExcercise)} - Finished, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<ExcerciseResponse>(excercise); ;
        }

        /// <inheritdoc/>
        public async Task<ExcerciseResponse> PutExcercise(Guid id, ExcerciseRequest request)
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
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excercise)} not found");
            }

            _mapper.Map(request, excercise);
            excercise = await _excerciseRepository.Update(excercise);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcercise)} - Finished, " +
                $"{nameof(id)}: {id}, " +
                $"{nameof(ExcerciseRequest)}: {JsonConvert.SerializeObject(request)}");
            return _mapper.Map<ExcerciseResponse>(excercise); ;
        }
        #endregion

        #region Exercise Types
        /// <inheritdoc/>
        public IDictionary<string, int> GetExcerciseTypes()
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcerciseTypes)} - Started");

            var excerciseTypes = Enum.GetValues(typeof(ExcerciseTypeEnum))
                                   .Cast<ExcerciseTypeEnum>()
                                   .ToDictionary(x => x.ToString(), x => (int)x);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcerciseTypes)} - Finished");
            return excerciseTypes;
        }
        #endregion

        #region Excercise Type Icons
        /// <inheritdoc/>
        public async Task<ExcerciseTypeIconResponse> GetExcerciseTypeIcon(ExcerciseTypeEnum id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcerciseTypeIcon)} - Started, " +
                $"{nameof(id)}: {id}");

            var excerciseTypeIcon = await _exerciseTypeIconRepository.GetWithExpression(x => x.ExcerciseTypeId == (int)id);

            if (excerciseTypeIcon == null)
            {
                _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcerciseTypeIcon)} - " +
                    $"{nameof(excerciseTypeIcon)} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excerciseTypeIcon)} not found");
            }

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(GetExcerciseTypeIcon)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<ExcerciseTypeIconResponse>(excerciseTypeIcon);   
        }

        /// <inheritdoc/>
        public async Task<ExcerciseTypeIconResponse> PostExcerciseTypeIcon(ExcerciseTypeEnum id, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PostExcerciseTypeIcon)} - Started, " +
                $"{nameof(id)}: {id}");

            var excerciseTypeIcon = await _exerciseTypeIconRepository.GetWithExpression(x => x.ExcerciseTypeId == (int)id);

            if (excerciseTypeIcon != null)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(PostExcerciseTypeIcon)} - {excerciseTypeIcon} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excerciseTypeIcon)} not found");
            }

            MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            var newExcerciseTypeIcon = new ExcerciseTypeIcon() 
            { 
                ExcerciseTypeId = (int)id, 
                Icon = memoryStream.ToArray(),
                Name = formFile.FileName,
                ContentType = formFile.ContentType
            };
            await _exerciseTypeIconRepository.Add(newExcerciseTypeIcon);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PostExcerciseTypeIcon)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<ExcerciseTypeIconResponse>(newExcerciseTypeIcon);
        }

        /// <inheritdoc/>
        public async Task<ExcerciseTypeIconResponse> PutExcerciseTypeIcon(ExcerciseTypeEnum id, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcerciseTypeIcon)} - Started, " +
                $"{nameof(id)}: {id}");

            var excerciseTypeIcon = await _exerciseTypeIconRepository.GetWithExpression(x => x.ExcerciseTypeId == (int)id);

            if (excerciseTypeIcon == null)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(PutExcerciseTypeIcon)} - " +
                    $"{nameof(excerciseTypeIcon)} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excerciseTypeIcon)} not found");
            }

            MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            excerciseTypeIcon.Icon = memoryStream.ToArray();
            excerciseTypeIcon.Name = formFile.FileName;
            excerciseTypeIcon.ContentType = formFile.ContentType;
            await _exerciseTypeIconRepository.Update(excerciseTypeIcon);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(PutExcerciseTypeIcon)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<ExcerciseTypeIconResponse>(excerciseTypeIcon);
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteExcerciseTypeIcon(ExcerciseTypeEnum id)
        {
            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcerciseTypeIcon)} - Started, " +
                $"{nameof(id)}: {id}");

            var excerciseTypeIcon = await _exerciseTypeIconRepository.GetWithExpression(x => x.ExcerciseTypeId == (int)id);

            if (excerciseTypeIcon == null)
            {
                _logger.LogError($"{nameof(ExcerciseService)} - {nameof(DeleteExcerciseTypeIcon)} - " +
                    $"{nameof(excerciseTypeIcon)} not found, " +
                    $"{nameof(id)}: {id}");
                throw new StatusCodeException(HttpStatusCode.NotFound, $"{nameof(excerciseTypeIcon)} not found");
            }

            await _exerciseTypeIconRepository.Delete(excerciseTypeIcon.Id);

            _logger.LogInformation($"{nameof(ExcerciseService)} - {nameof(DeleteExcerciseTypeIcon)} - Finished, " +
                $"{nameof(id)}: {id}");
            return true;
        }
        #endregion
    }
}
