using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Architect.Model
{
    public class CustomerUserInfo
    {
        /// <summary>
        /// 当前用户唯一ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 当前用户客户端IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 当前用户客户端地址
        /// </summary>
        public string Isp { get; set; }
        /// <summary>
        /// 当前用户客户端游览器和版本信息
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 当前用户客户端系统
        /// </summary>
        public string OS { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        ///纬度
        /// </summary>
        public string lng { get; set; }
        /// <summary>
        /// 默认为1有效 0位黑名单
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperationTime { get; set; }
        /// <summary>
        /// 用户图像
        /// </summary>
        public int IMG { get; set; }
        /// <summary>
        /// 进入统计
        /// </summary>
        public int InCount { get; set; }
        public string modelType { get; set; }
        public string Remarks { get; set; }
        public string Browser_1 { get; set; }
        public string OS_1 { get; set; }
    }
}
