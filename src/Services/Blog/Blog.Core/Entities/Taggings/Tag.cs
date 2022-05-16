using BN.CleanArchitecture.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Entities.Taggings
{
    public class Tag : AuditedEntity<Guid>
    {
        public Tag(Guid id) : base(id)
        {
        }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid TagColorId { get; set; }
        public TagColor TagColor { get; set; }
    }
}
