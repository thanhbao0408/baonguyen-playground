using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.CleanArchitecture.Contracts.Dtos
{
    public class AuditedEntityDto : EntityDto, IHasAuditableFields
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public class AuditedEntityDto<TKey> : EntityDto<TKey>, IHasAuditableFields
    {
        protected AuditedEntityDto() { }
        protected AuditedEntityDto(TKey id) : base(id)
        {
        }

        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
