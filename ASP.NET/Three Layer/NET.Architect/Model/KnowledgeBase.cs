using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NET.Architect.Model
{
    public class KnowledgeBase
    {
        public string Code { get; set; }
        [DisplayName("知识库问题")]
        public string Info { get; set; }
        [DisplayName("知识库答案")]
        public string Text { get; set; }
        public string UserID { get; set; }

        [DisplayName("知识库最后编辑时间")]
        public string LAST_MODIFIED_TIME { get; set; }
        public string Ip { get; set; }
        public string ClientAddressJson { get; set; }
    }
}
