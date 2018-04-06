using System.Net;
using System.Text;

namespace APIRequestServices
{
    public class BaiduHelghtIPReqquest
    {
        public static string BaidHelghtIp(string LocationIP, string ModelType)
        {
            string strURL = $"https://api.map.baidu.com/highacciploc/v1?qcip={LocationIP}&qterm={ModelType}&ak=bDSpgtzrQvEEhAo1NdEEHjWo7f2KTiTn&extensions=3";
            System.Net.HttpWebRequest request;
            // 创建一个HTTP请求  
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var responseText = myreader.ReadToEnd();
            myreader.Close();
            return responseText;
        }
    }
}
