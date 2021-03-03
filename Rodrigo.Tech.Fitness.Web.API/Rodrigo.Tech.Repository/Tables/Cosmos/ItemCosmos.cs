using Newtonsoft.Json;
using System;
using Rodrigo.Tech.Respository.Pattern.Interface;

namespace Rodrigo.Tech.Respository.Tables.Cosmos
{
    [Serializable]
    public class ItemCosmos : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
