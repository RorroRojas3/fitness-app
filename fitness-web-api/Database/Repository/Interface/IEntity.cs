using System;

namespace fitness_web_api.Database.Repository.Interface
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}