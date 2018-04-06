using System.Threading;

namespace NET.Web.UI.Models
{
    public class RequestParam
    {
        public string UserAgent { get; set; }
        public string Ip { get; set; }
        public string Token { get; set; }
        public string Data { get; set; }
        public WaitHandle WaitHandle { get; set; }
        public string ModelType { get; set; }
    }
}