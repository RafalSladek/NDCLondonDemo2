using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCLondonDemo2
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }
}