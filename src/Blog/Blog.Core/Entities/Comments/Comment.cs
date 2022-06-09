using Blog.Core.Entities.Users;
using BN.CleanArchitecture.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public class Comment : AuditedEntity<Guid>
    {
        public Comment(Guid id) : base(id)
        {
        }
        
        public Guid ArticleId { get; set; }
        public Guid? BlogUserId { get; set; }
        public Guid? RepliedCommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("BlogUserId")]
        public BlogUser BlogUser { get; set; }
        
    }
}
