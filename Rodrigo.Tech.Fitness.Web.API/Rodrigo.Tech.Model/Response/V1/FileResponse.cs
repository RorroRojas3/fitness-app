using System;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class FileResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}