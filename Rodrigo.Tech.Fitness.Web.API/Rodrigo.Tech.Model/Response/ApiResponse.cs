using System.Net;

namespace Rodrigo.Tech.Model.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse(HttpStatusCode httpStatusCode, T data = default, string message = null)
        {
            HttpStatusCode = (int)httpStatusCode;
            Data = data;
            Message = message;
        }

        public int HttpStatusCode { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}
