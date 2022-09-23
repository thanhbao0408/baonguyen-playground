using BN.CleanArchitecture.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Core.Entities.Taggings
{
    public class TagColor: Entity<Guid>
    {
        public TagColor()
        {

        }
        public TagColor(Guid id) : base(id)
        {

        }
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string BgColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string BorderColor { get; set; }

        [MaxLength(50)]
        [Required]
        public string TextColor { get; set; }
    }
}
