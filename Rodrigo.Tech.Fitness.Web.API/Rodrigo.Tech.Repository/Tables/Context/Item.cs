using Rodrigo.Tech.Respository.Pattern.Interface;
using Rodrigo.Tech.Respository.Schemas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Respository.Tables.Context
{
    [Serializable]
    [Table(nameof(Item), Schema = nameof(TableSchemas.Example))]
    public class Item : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}