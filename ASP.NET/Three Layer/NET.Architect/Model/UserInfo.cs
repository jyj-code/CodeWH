using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NET.Architect.Model
{
    public class UserInfo
    {
        [Key]
        [DisplayName("主键ID")]
        public string Id { get; set; }

        [DisplayName("用户唯一IP地址")]
        [Key]
        public string Ip { get; set; }

        [DisplayName("用户详细地址")]
        public string Isp { get; set; }

        [DisplayName("国家")]
        public string Country { get; set; }

        [DisplayName("省份")]
        public string Province { get; set; }

        [DisplayName("城市")]
        public string City { get; set; }

        [DisplayName("区县")]
        public string District { get; set; }

        [DisplayName("街道")]
        public string Street { get; set; }

        [DisplayName("门牌号")]
        public string Street_Number { get; set; }

        [DisplayName("行政区划代码（身份证前6位）")]
        public string Admin_Area_Code { get; set; }

        [DisplayName("经度")]
        public string Lat { get; set; }

        [DisplayName("纬度")]
        public string Lng { get; set; }

        [DisplayName("百度IP定位结果唯一ID")]
        public string Locid { get; set; }

        [DisplayName("百度IP定位定位结果半径")]
        public string radius { get; set; }

        [DisplayName("百度IP定位定位结果可信度")]
        public string Confidence { get; set; }

        [DisplayName("用户状态1无效 0有效")]
        public int Status { get; set; }

        [DisplayName("最近操作时间")]
        public string OperationTime { get; set; }

        [DisplayName("图像索引")]
        public int Img { get; set; }

        [DisplayName("访问次数")]
        public int InCount { get; set; }

        [DisplayName("备注")]
        public string Remarks { get; set; }
    }
}
