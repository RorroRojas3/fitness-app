using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Rodrigo.Tech.Service.Interface.Common;

namespace Rodrigo.Tech.Service.Implementation.Common
{
    public class HttpClientService : IHttpClientService
    {
        private readonly ILogger _logger;

        public HttpClientService(ILogger<HttpClientService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> Json(string url, HttpMethod httpMethod, Dictionary<string, string> headers = null, object body = null)
        {
            _logger.LogInformation($"{nameof(HttpClientService)} - {nameof(Json)} - Started, " +
                $"{nameof(url)}: {url}, " +
                $"{nameof(body)}: {JsonConvert.SerializeObject(body)}, " +
                $"{nameof(httpMethod)}: {httpMethod}");

            using var client = CreateClient();
            HttpResponseMessage httpResponseMessage = null;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            switch (httpMethod.ToString().ToUpper())
            {
                case "GET":
                    httpResponseMessage = await client.GetAsync(url);
                    break;
                case "POST":
                    httpResponseMessage = await client.PostAsJsonAsync(url, body);
                    break;
                case "PUT":
                    httpResponseMessage = await client.PutAsJsonAsync(url, body);
                    break;
                case "DELETE":
                    httpResponseMessage = await client.DeleteAsync(url);
                    break;
                default:
                    _logger.LogError($"{nameof(HttpClientService)} - {nameof(Json)} - {nameof(httpMethod)} not found, " +
                        $"{nameof(url)}: {url}, " +
                        $"{nameof(body)}: {JsonConvert.SerializeObject(body)}, " +
                        $"{nameof(httpMethod)}: {httpMethod}");
                    break;
            }

            return httpResponseMessage;
        }

        /// <summary>
        ///     Creates HttpClient 
        /// </summary>
        /// <returns></returns>
        private HttpClient CreateClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    return true;
                }
            };
            var httpClient = new HttpClient(httpClientHandler);
            return httpClient;
        }
    }
}
