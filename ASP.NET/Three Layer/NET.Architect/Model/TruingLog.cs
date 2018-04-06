using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NET.Architect.Model
{
    public class TruingLog : CustomerUserInfo
    {
        [DisplayName("访问ID")]
        public string ID { get; set; }
        [DisplayName("访问IP")]
        public string UserId { get; set; }
        [DisplayName("答案")]
        public string SESSION { get; set; }
        [DisplayName("问题")]
        public string INFO { get; set; }
        [DisplayName("访问时间")]
        public string LAST_MODIFIED_TIME { get; set; }
    }
}
