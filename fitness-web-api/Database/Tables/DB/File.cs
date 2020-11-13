using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fitness_web_api.Database.Repository.Interface;

namespace fitness_web_api.Database.Tables
{
    [Table(nameof(File), Schema = nameof(Schemas.Example))]
    public class File : IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}