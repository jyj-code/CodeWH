using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Article : ShortArticle
    {
        public string UserName { get; set; }
        public string typeName { get; set; }
        public string CommentCount { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
    }
}
