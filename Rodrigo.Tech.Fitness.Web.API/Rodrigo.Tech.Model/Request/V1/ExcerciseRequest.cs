using System.ComponentModel.DataAnnotations;

namespace Rodrigo.Tech.Model.Request.V1
{
    public class ExcerciseRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int Type { get; set; }
    }
}
