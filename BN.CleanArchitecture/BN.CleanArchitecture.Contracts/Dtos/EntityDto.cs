using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.CleanArchitecture.Contracts.Dtos
{
    public class EntityDto
    {
    }

    public class EntityDto<TKey>: EntityDto
    {
        public virtual TKey Id { get; set; }

        protected EntityDto() { }

        protected EntityDto(TKey id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"[ENTITY DTO: {GetType().Name}] Id = {Id}";
        }
    }
}
