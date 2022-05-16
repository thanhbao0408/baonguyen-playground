using BN.CleanArchitecture.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Dtos.Articles
{
    public class ArticleDto: AuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }
        public string Slug { get; set; }
    }
}
