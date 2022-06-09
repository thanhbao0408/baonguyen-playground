using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.CleanArchitecture.Core.Domain.Entities
{
    public class AuditedEntity : Entity, IHasAuditableFields
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public class AuditedEntity<TKey> : Entity<TKey>, IHasAuditableFields
    {
        protected AuditedEntity() { }
        protected AuditedEntity(TKey id) : base(id)
        {
        }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
