using BN.CleanArchitecture.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities.Users
{
    public class BlogUser : AuditedEntity<Guid>
    {
        protected BlogUser(Guid id) : base(id)
        {
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }
    }
}
