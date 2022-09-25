using System.ComponentModel.DataAnnotations;

namespace Playground.WebAdmin.Models.Setting
{
    public class CreateUpdateTagViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200)]

        public string Name { get; set; }

        [Required]
        public Guid ColorId { get; set; }
    }
}
