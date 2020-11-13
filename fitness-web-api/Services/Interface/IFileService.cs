using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using fitness_web_api.Database.Tables;
using fitness_web_api.Models.Requests;
using fitness_web_api.Models.Responses;

namespace fitness_web_api.Services.Interface
{
    public interface IFileService
    {
        /// <summary>
        ///     Gets all files from DB
        /// </summary>
        /// <returns></returns>
        Task<List<FileResponse>> GetAllFiles();

        /// <summary>
        ///     Gets file from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<File> GetFile(Guid id);

        /// <summary>
        ///     Creates file on DB 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<bool> PostFile(IFormFile formFile);

        /// <summary>
        ///     Deletes file from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFile(Guid id);

        /// <summary>
        ///     Updates file from DB based on Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<bool> UpdateFile(Guid id, IFormFile formFile);

        /// <summary>
        ///     Creates file by chunks
        /// </summary>
        /// <returns></returns>
        Task<bool> PostFileByChunks(FileByChunksRequest request);
    }
}