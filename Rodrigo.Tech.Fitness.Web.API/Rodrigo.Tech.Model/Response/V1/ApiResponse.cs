using System.Net;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class ApiResponse<T>
    {
        public ApiResponse(HttpStatusCode httpStatusCode, T data = default)
        {
            HttpStatusCode = (int)httpStatusCode;
            Data = data;
        }

        public int HttpStatusCode { get; set; }

        public T Data { get; set; }
    }
}
