using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rodrigo.Tech.Repository.Pattern.Interface;

namespace Rodrigo.Tech.Repository.Tables.Context
{
    [Serializable]
    [Table(nameof(User))]
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] Picture { get; set; }
    }
}