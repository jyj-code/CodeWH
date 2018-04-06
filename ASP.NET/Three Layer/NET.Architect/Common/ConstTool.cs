using System.Collections.Generic;

namespace NET.Architect.Common
{
    public class ConstTool
    {
        public const string Insert = " INSERT INTO {0}({1})VALUES({2})";
        public const string Delete = " DELETE FROM {0} WHERE {1} ";
        public const string Update = " UPDATE {0} SET {1} WHERE {2}";
        public const string Select = " SELECT {0} FROM {1} ";
        public const string DateFormat1 = "yyyy-MM-dd HH:mm:ss";
        public const string DateFormat2 = "HH:mm:ss";
        public const string RedirectUrl = "http://kebue.com/";
        public static char[] constant ={ '0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        public static Dictionary<string, string> FilterDict
        {
            get
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("当天请求次数已使用完", "未找到匹配你的问题。你是从火星来的吧？");
                dict.Add("妖王", "同学");
                return dict;
            }
        }
        public const string MessageFormat = "<div class='chat-right col-xs-6'><div class='row'><div class='col-xs-6'><div class='chat-context chat-right-context'>{0}</div></div><div class='col-xs-6'><dl class='chat-right-title'><dt><span class='fll'><i class='glyphicon glyphicon-user'></i>&nbsp;{1}</span><span class='flr'><img class='chat-right-title-img' src='../../Content/image/user/{2}.jpg' title='IP地址：{6}   科布尔-机器人-科小智' width='35' height='35'></span></dt><dd><span class='fll'></span><span class='flr'>{3} {4} </span></dd><dd><span class='fll'>{5} </span><span class='clear'></span></dd></dl></div></div></div>";
        public const string HomeMessage = "我是科小智，我的官网是<a href='http://kebue.com/' target='_blank' title='科布尔'><h1>kebue.com</h1></a>";
        public static List<string> MessageFilter = new List<string> { "你是谁", "您是谁", "你是谁?", "您是谁?", "who are you", "who are you ?" };

    }
}
