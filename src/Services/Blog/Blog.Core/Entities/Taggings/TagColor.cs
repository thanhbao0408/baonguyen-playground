using BN.CleanArchitecture.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities.Taggings
{
    public class TagColor: Entity<Guid>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string BgColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string BorderColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string TextColor { get; set; }
    }
}
