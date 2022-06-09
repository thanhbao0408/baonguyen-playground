using BN.CleanArchitecture.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities.Taggings
{
    public class BlogTag : AuditedEntity<Guid>
    {
        public BlogTag(Guid id) : base(id)
        {
        }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string BlogTagColorName { get; set; }

        [MaxLength(50)]
        [Required]
        public string BlogTagBgColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string BlogTagBorderColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string BlogTagTextColor { get; set; }
    }
}
