using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class ArticleLink
    {
        public string ID { get; set; }
        public string Article_ID { get; set; }
        public string Link_Url { get; set; }
        public string Template_ID { get; set; }
        public DateTime Date { get; set; }
    }
}
