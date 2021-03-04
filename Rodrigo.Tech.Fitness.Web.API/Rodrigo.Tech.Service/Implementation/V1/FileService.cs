using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Service.Interface.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = Rodrigo.Tech.Repository.Tables.Context.File;

namespace Rodrigo.Tech.Service.Implementation
{
    public class FileService : IFileService
    {
        private readonly IRepository<File> _fileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FileService(IRepository<File> fileRepository,
                            IMapper mapper,
                            ILogger<FileService> logger)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteFile(Guid id)
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(DeleteFile)} - Started, " +
                $"{nameof(id)}: {id}");

            var result =  await _fileRepository.Delete(id);

            _logger.LogInformation($"{nameof(FileService)} - {nameof(DeleteFile)} - Finished, " +
                $"{nameof(id)}: {id}");
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<FileResponse>> GetAllFiles()
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetAllFiles)} - Started");

            var files = await _fileRepository.GetAll();
            
            if (files.Count == 0)
            {
                _logger.LogError($"{nameof(FileService)} - {nameof(GetAllFiles)} - Not found");
                return null;
            }

            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetAllFiles)} - Finished");
            return _mapper.Map<List<FileResponse>>(files); ;
        }

        /// <inheritdoc/>
        public async Task<FileResponse> GetFileInfo(Guid id)
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetFileInfo)} - Started, " +
                $"{nameof(id)}: {id}");

            var file = await _fileRepository.Get(id);

            if (file == null)
            {
                _logger.LogInformation($"{nameof(FileService)} - {nameof(GetFileInfo)} - Not found, " +
                $"{nameof(id)}: {id}");
                return null;
            }

            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetFileInfo)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<FileResponse>(file);
        }

        /// <inheritdoc/>
        public async Task<FileResponse> PostFile(IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(PostFile)} - Started");

            _logger.LogInformation($"{nameof(FileService)} - {nameof(PostFile)} - Formfile to Byte array");
            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                data = memoryStream.ToArray();
            }

            var file = new File
            {
                Name = formFile.FileName,
                ContentType = formFile.ContentType,
                Data = data
            };

            _logger.LogInformation($"{nameof(FileService)} - {nameof(PostFile)} - Adding to database");
            var result = await _fileRepository.Add(file);

            _logger.LogInformation($"{nameof(FileService)} - {nameof(PostFile)} - Finished");
            return _mapper.Map<FileResponse>(file);
        }

        /// <inheritdoc/>
        public async Task<FileResponse> UpdateFile(Guid id, IFormFile formFile)
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(UpdateFile)} - Started, " +
                $"{nameof(id)}: {id}");

            var file = await _fileRepository.Get(id);

            if (file == null)
            {
                _logger.LogError($"{nameof(FileService)} - {nameof(UpdateFile)} - Not found, " +
                $"{nameof(id)}: {id}");
                return null;
            }

            _logger.LogInformation($"{nameof(FileService)} - {nameof(UpdateFile)} - Updating file, " +
                $"{nameof(id)}: {id}");

            byte[] data;
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                data = memoryStream.ToArray();
            }

            file.Name = formFile.Name;
            file.ContentType = formFile.ContentType;
            file.Data = data;
            await _fileRepository.Update(file);

            _logger.LogInformation($"{nameof(FileService)} - {nameof(UpdateFile)} - Finished, " +
                $"{nameof(id)}: {id}");
            return _mapper.Map<FileResponse>(file);
        }

        /// <inheritdoc/>
        public async Task<File> GetFileDownload(Guid id)
        {
            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetFileDownload)} - Started, " +
                $"{nameof(id)}: {id}");

            var file = await _fileRepository.Get(id);

            if (file == null)
            {
                _logger.LogError($"{nameof(FileService)} - {nameof(GetFileDownload)} - Not found, " +
                $"{nameof(id)}: {id}");
                return null;
            }

            _logger.LogInformation($"{nameof(FileService)} - {nameof(GetFileDownload)} - Finished, " +
                $"{nameof(id)}: {id}");
            return file;
        }
    }
}