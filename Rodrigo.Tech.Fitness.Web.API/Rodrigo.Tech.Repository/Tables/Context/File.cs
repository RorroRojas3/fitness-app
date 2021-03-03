using Rodrigo.Tech.Respository.Pattern.Interface;
using Rodrigo.Tech.Respository.Schemas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Respository.Tables.Context
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