using BaiduSDK.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KebueUI.Models
{
    public class ChatRequest
    {
        public string Address { get; set; }
        public string ModelType { get; set; }
        public string Ip { get; set; }
        public string Os { get; set; }
        public MapEntity mapEntity { get; set; }
        public string UserAgent { get; set; }
    }
}
