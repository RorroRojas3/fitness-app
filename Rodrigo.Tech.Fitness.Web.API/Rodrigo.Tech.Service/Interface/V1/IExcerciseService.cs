﻿using Microsoft.AspNetCore.Http;
using Rodrigo.Tech.Model.Enums.V1;
using Rodrigo.Tech.Model.Request.V1;
using Rodrigo.Tech.Model.Response.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Interface.V1
{
    public interface IExcerciseService
    {
        /// <summary>
        ///     Gets all excercises
        /// </summary>
        /// <returns></returns>
        Task<ApiResponse<IList<ExcerciseResponse>>> GetExcercises();

        /// <summary>
        ///     Gets single excercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<ExcerciseResponse>> GetExcercise(Guid id);

        /// <summary>
        ///     Creates excercise
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse<ExcerciseResponse>> PostExcercise(ExcerciseRequest request);

        /// <summary>
        ///     Updates excercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse<ExcerciseResponse>> PutExcercise(Guid id, ExcerciseRequest request);

        /// <summary>
        ///     Deletes Excercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> DeleteExcercise(Guid id);

        /// <summary>
        ///     Gets list of excercise types
        /// </summary>
        /// <returns></returns>
        ApiResponse<IDictionary<string, int>> GetExcerciseTypes();

        /// <summary>
        ///     Assigns Icon to Excercise type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<ApiResponse<object>> PostExcerciseTypeIcon(ExcerciseTypeEnum id, IFormFile formFile);
    }
}