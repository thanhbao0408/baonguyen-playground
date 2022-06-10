using BN.CleanArchitecture.Core.Domain.Entities;
using Playground.Core.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playground.Core.Entities.Blog.Comments
{
    public class Comment : AuditedEntity<Guid>
    {
        public Comment(Guid id) : base(id)
        {
        }
        
        public Guid ArticleId { get; set; }
        public Guid? RepliedCommentId { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid? BlogUserId { get; set; }
        [ForeignKey("BlogUserId")]
        public PlaygroundUser BlogUser { get; set; }
        
    }
}
