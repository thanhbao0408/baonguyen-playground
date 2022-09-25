using BN.CleanArchitecture.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Playground.Core.Entities.Taggings
{
    public class Tag : AuditedEntity<Guid>
    {
        public Tag(Guid id) : base(id)
        {
        }

        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public Guid ColorId { get; set; }
        public TagColor Color { get; set; }
    }
}
