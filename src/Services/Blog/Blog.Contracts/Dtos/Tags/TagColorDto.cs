using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Contracts.Dtos.Tags
{
    public class TagColorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string BgColor { get; set; }

        public string BorderColor { get; set; }

        public string TextColor { get; set; }
    }
}
