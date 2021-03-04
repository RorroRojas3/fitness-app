using Rodrigo.Tech.Repository.Pattern.Interface;
using Rodrigo.Tech.Repository.Schemas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Repository.Tables.Context
{
    [Table(nameof(File), Schema = nameof(TableSchemas.Example))]
    public class File : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}