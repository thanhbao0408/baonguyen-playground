using BN.CleanArchitecture.Core.Domain.Entities;

namespace Playground.Core.Entities.Users
{
    public class PlaygroundUser : AuditedEntity<Guid>
    {
        protected PlaygroundUser(Guid id) : base(id)
        {
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }
    }
}
