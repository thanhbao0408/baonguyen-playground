using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contract.Dtos.Tags
{
    public class TagDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TagColorId { get; set; }
        public TagColorDto TagColor { get; set; }
    }
}
