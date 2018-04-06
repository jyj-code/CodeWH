using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiduSDK.Entity
{
    public class MapEntity
    {
        public HelghtIP content { get; set; }
        public HelghtIP_Result result { get; set; }
    }
    public class HelghtIP
    {
        /// <summary>
        /// 定位结果唯一ID，用于问题排查
        /// </summary>
        public string locid { get; set; }
        /// <summary>
        /// 定位结果半径
        /// </summary>
        public string radius { get; set; }
        /// <summary>
        /// 定位结果可信度
        /// </summary>
        public string confidence { get; set; }
        public IpLocation location { get; set; }
        public HelghtIP_address_component address_component { get; set; }
        /// <summary>
        /// 	结构化地址信息
        /// </summary>
        public string formatted_address { get; set; }
        public Business Pois { get; set; }

    }
    public class HelghtIP_Result
    {
        /// <summary>
        /// 定位时间
        /// </summary>
        public string loc_time { get; set; }
        /// <summary>
        /// 定位结果状态码
        ///161：定位成功
        ///167：定位失败
        ///1：服务器内部错误
        ///101：AK参数不存在
        ///200：应用不存在，AK有误请检查重试
        ///201：应用被用户自己禁止
        ///202：应用被管理员删除
        ///203：应用类型错误
        ///210：应用IP校验失败
        ///211：应用SN校验失败
        ///220：应用Refer检验失败
        ///240：应用服务被禁用
        ///251：用户被自己删除
        ///252：用户被管理员删除
        ///260：服务不存在
        ///261：服务被禁用
        ///301：永久配额超限，禁止访问
        ///302：当天配额超限，禁止访问
        ///401：当前并发超限，限制访问
        ///402：当前并发和总并发超限
        /// </summary>
        public string error { get; set; }
    }
    public class HelghtIP_address_component : RegionalCascadeAddres
    {
        /// <summary>
        /// 行政区划代码（身份证前6位）
        /// </summary>
        public string admin_area_code { get; set; }
    }
    /// <summary>
    /// 商圈信息
    /// </summary>
    public class Business
    {
        public string name { get; set; }
        public string uid { get; set; }
        public string address { get; set; }
        public string tag { get; set; }
        public IpLocation location { get; set; }
    }

}
