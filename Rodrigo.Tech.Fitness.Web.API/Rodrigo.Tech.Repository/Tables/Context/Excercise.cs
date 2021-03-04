using Rodrigo.Tech.Repository.Pattern.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Repository.Tables.Context
{
    [Serializable]
    [Table(nameof(Excercise))]
    public class Excercise : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
