using Rodrigo.Tech.Model.Attribute.V1;
using Rodrigo.Tech.Model.Enums.V1;
using System.ComponentModel.DataAnnotations;

namespace Rodrigo.Tech.Model.Request.V1
{
    public class ExcerciseRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [RequiredEnum(ErrorMessage = "Invalid type")]
        public ExcerciseTypeEnum Type { get; set; }
    }
}
