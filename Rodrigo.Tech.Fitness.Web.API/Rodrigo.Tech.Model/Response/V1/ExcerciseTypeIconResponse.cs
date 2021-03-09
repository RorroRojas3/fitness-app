using Rodrigo.Tech.Model.Enums.V1;
using System;
using System.IO;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class ExcerciseTypeIconResponse
    {
        public Guid Id { get; set; }

        public ExcerciseTypeEnum ExcerciseTypeId { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public Stream Stream { get; set; }
    }
}
