using NET.Architect.Model;

namespace UI
{
    public class RequestTuring
    {
        CustomizeTuringLoginc customizeturingloginc = new CustomizeTuringLoginc();
        RequestAPITuring requestapituring = new RequestAPITuring();
        /// <summary>
        /// 先在本地数据库查找是否有匹配数据返回空在去远程调用接口
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string GetText(string info, UserInfoUnDeteil userinfo)
        {
            string result = string.Empty;
            result = customizeturingloginc.GetText(info);
            if (string.IsNullOrEmpty(result))
                result = requestapituring.GetText(info, userinfo);
            return result;
        }
    }
}