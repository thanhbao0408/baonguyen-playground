﻿using BN.CleanArchitecture.Core.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Playground.Core.Entities.Blog.Articles
{
    public class Article : AuditedEntity<Guid>
    {
        public Article(Guid id) : base(id)
        {
        }

        [MaxLength(500)]
        [Required]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Description { get; set; }

        public string Content { get; set; }

        public string CoverImage { get; set; }

        [StringLength(50)]
        [Required]
        public string Slug { get; set; }

        public Collection<ArticleTag> ArticleTags { get; set; } = new Collection<ArticleTag>();

        public void AddTag(Guid tagId)
        {
            ArticleTags.Add(new ArticleTag(Id, tagId));
        }

        public void RemoveTag(Guid tagId)
        {
            var tag = ArticleTags.FirstOrDefault(p => p.TagId == tagId);
            if(tag != null)
            {
                ArticleTags.Remove(tag);
            }
        }
    }
}