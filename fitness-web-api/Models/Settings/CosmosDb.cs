using System.Collections.Generic;

namespace fitness_web_api.Models.Settings
{
    public class CosmosDb
    {
        public string DatabaseName { get; set; }

        public List<ContainerCollection> ContainerCollection { get; set; }

        public string Account { get; set; }

        public string Key { get; set; }
    }

    public class ContainerCollection
    {
        public string Name { get; set; }

        public string PartitionKey { get; set; }
    }
}