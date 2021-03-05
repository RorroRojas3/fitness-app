using Rodrigo.Tech.Repository.Pattern.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Repository.Tables.Context
{
    [Serializable]
    [Table(nameof(ExcerciseTypeMapping))]
    public class ExcerciseTypeMapping : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int ExcerciseTypeId { get; set; } 

        [Required]
        public byte[] Icon { get; set; }
    }
}
