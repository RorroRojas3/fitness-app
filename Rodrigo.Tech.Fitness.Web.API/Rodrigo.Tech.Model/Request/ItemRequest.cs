using System.ComponentModel.DataAnnotations;

namespace Rodrigo.Tech.Model.Requests
{
    public class ItemRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}