using Rodrigo.Tech.Repository.Pattern.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rodrigo.Tech.Repository.Tables.Context
{
    [Serializable]
    [Table(nameof(ExcerciseTypeIcon))]
    public class ExcerciseTypeIcon : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int ExcerciseTypeId { get; set; } 

        [Required]
        [StringLength(300, ErrorMessage = "File name above 300 characters", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "File content type above 300 characters", MinimumLength = 5)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Icon { get; set; }
    }
}
