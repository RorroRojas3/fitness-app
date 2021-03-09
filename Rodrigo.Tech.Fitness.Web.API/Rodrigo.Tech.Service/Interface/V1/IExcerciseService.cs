using Microsoft.AspNetCore.Http;
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
        Task<IList<ExcerciseResponse>> GetExcercises();

        /// <summary>
        ///     Gets single excercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExcerciseResponse> GetExcercise(Guid id);

        /// <summary>
        ///     Creates excercise
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ExcerciseResponse> PostExcercise(ExcerciseRequest request);

        /// <summary>
        ///     Updates excercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ExcerciseResponse> PutExcercise(Guid id, ExcerciseRequest request);

        /// <summary>
        ///     Deletes Excercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteExcercise(Guid id);

        /// <summary>
        ///     Gets list of excercise types
        /// </summary>
        /// <returns></returns>
        IDictionary<string, int> GetExcerciseTypes();

        /// <summary>
        ///     Gets ExcerciseType Icon image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExcerciseTypeIconResponse> GetExcerciseTypeIcon(ExcerciseTypeEnum id);

        /// <summary>
        ///     Assigns Icon to Excercise type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<ExcerciseTypeIconResponse> PostExcerciseTypeIcon(ExcerciseTypeEnum id, IFormFile formFile);

        /// <summary>
        ///     Updates ExerciseType Icon image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<ExcerciseTypeIconResponse> PutExcerciseTypeIcon(ExcerciseTypeEnum id, IFormFile formFile);

        /// <summary>
        ///     Deletes ExerciseType Icon image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteExcerciseTypeIcon(ExcerciseTypeEnum id);
    }
}
