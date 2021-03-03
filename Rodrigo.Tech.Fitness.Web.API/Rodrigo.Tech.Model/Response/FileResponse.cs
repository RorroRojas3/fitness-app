using System;

namespace Rodrigo.Tech.Model.Response
{
    public class FileResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}