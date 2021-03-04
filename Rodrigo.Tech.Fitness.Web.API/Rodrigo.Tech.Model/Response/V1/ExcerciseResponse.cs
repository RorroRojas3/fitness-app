using System;

namespace Rodrigo.Tech.Model.Response.V1
{
    public class ExcerciseResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }
    }
}
