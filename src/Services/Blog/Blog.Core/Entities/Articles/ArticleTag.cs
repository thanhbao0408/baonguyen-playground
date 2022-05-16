using Blog.Core.Entities.Taggings;
using BN.CleanArchitecture.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities.Articles
{
    public class ArticleTag : AuditedEntity
    {
        public ArticleTag(Guid articleId, Guid tagId)
        {
            ArticleId = articleId;
            TagId = tagId;
        }

        [Key]
        public Guid ArticleId { get; set; }

        [Key]
        public Guid TagId { get; set; }

        public Tag Tag { get; set; }

    }
}
