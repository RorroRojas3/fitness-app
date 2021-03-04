using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rodrigo.Tech.Model.Response.V1;
using Rodrigo.Tech.Repository.Tables.Context;

namespace Rodrigo.Tech.Service.Interface.V1
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
        Task<FileResponse> GetFileInfo(Guid id);

        /// <summary>
        ///     Creates file on DB 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<FileResponse> PostFile(IFormFile formFile);

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
        Task<FileResponse> UpdateFile(Guid id, IFormFile formFile);

        /// <summary>
        ///     Gets binary file 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<File> GetFileDownload(Guid id);
    }
}