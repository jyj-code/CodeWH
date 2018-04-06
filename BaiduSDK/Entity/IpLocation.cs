using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaiduSDK.Entity
{
    /// <summary>
    /// 经纬度
    /// </summary>
    public class IpLocation : RegionalCascadeAddres
    {
        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Lng { get; set; }
    }
}
