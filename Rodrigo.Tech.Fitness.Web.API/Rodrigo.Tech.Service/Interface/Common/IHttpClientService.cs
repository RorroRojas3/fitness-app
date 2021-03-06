﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rodrigo.Tech.Service.Interface.Common
{
    public interface IHttpClientService
    {
        /// <summary>
        ///     GET/POST/DELETE/PUT Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        /// <param name="body"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> Json(string url, HttpMethod httpMethod, Dictionary<string, string> headers = null, object body = null);

        /// <summary>
        ///     Gets authorization header for Bearer JWT token
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        Dictionary<string, string> GetBearerJWTAuthorizationHeader(string jwt);
    }
}
