using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NET.Architect.Model
{
    public class UserInfoDetail
    {
        [Key]
        [DisplayName("主键")]
        public string Id { get; set; }

        [DisplayName("访问IP")]
        public string Ip { get; set; }

        [DisplayName("访问游览器")]
        public string Browser { get; set; }

        [DisplayName("访问操作系统类型")]
        public string OS { get; set; }

        [DisplayName("访问设备类型")]
        public string ModelType { get; set; }

        [DisplayName("备注说明")]
        public string Remarks { get; set; }

        [DisplayName("操作时间")]
        public string OperationTime { get; set; }
    }
}
