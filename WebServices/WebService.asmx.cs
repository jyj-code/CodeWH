using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServices
{
    /// <summary>
    /// WebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 求乘积
        /// </summary>
        /// <param name="x">乘积X</param>
        /// <param name="y">乘积Y</param>
        /// <returns></returns>
        [WebMethod(Description = "求乘积")]
        public int Calc(int x, int y)
        {
            return x * y;
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns></returns>
        [WebMethod(Description = "求和")]
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }
}
