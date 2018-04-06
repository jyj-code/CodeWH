using System;
using System.IO;
using System.Text;
using NET.Architect.Model;
using Log4net;
using System.Threading.Tasks;

namespace UI.Models
{
    public class FileOperations
    {
        #region XML操作相关
        //private static string XML_Path = System.AppDomain.CurrentDomain.BaseDirectory + @"Content\Turing\js\turing.xml";
        //public const string XML_Path_Node = "/Customize/TurindDb";
        //public const string XML_Node_Fir = "Customize";
        //public const string XML_Node_Chi = "TurindDb";
        ///// <summary>
        ///// 会话问题
        ///// </summary>

        ///// <summary>
        ///// 返回问题答案
        ///// </summary>
        //public const string Text = "text";
        //public const string XML_Cache_Key = "TuringCustom";

        //public static string Read(string path)
        //{
        //    System.Text.StringBuilder str = new System.Text.StringBuilder();
        //    System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default);
        //    System.String line;
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        str.Append(line.ToString());
        //    }
        //    return str.ToString();
        //}
        ///// <summary>
        ///// XML类容写入缓存
        ///// </summary>
        //public static void ReadXMLfile()
        //{
        //    CacheHelper.SetCache(XML_Cache_Key, ReaXML_InfoText);
        //    CacheHelper.SetCache("ReaXML_InfoCode", ReaXML_InfoCode);
        //}
        //public static Dictionary<string, string> ReaXML_InfoText
        //{
        //    get
        //    {
        //        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //        XmlDocument xml = new XmlDocument();
        //        xml.Load(XML_Path);
        //        XmlNodeList nodelist = xml.SelectNodes(XML_Path_Node);
        //        foreach (var item in nodelist)
        //        {
        //            XmlElement xe = (XmlElement)item;
        //            dictionary.Add(xe.GetAttribute(Info), xe.GetAttribute(Text));
        //        }
        //        return dictionary;
        //    }
        //}
        //public static Dictionary<string, string> ReaXML_InfoCode
        //{
        //    get
        //    {
        //        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //        XmlDocument xml = new XmlDocument();
        //        xml.Load(XML_Path);
        //        XmlNodeList nodelist = xml.SelectNodes(XML_Path_Node);
        //        foreach (var item in nodelist)
        //        {
        //            XmlElement xe = (XmlElement)item;
        //            dictionary.Add(xe.GetAttribute(Info), xe.GetAttribute(Code));
        //        }
        //        return dictionary;
        //    }
        //}
        ///// <summary>
        ///// 从本地缓存获取数据
        ///// </summary>
        ///// <param name="sessionKey"></param>
        ///// <returns></returns>
        //public static string GetSession(string sessionKey)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        sessionKey = sessionKey.Trim();
        //        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //        #region 本地缓存初始化
        //        var custom = CacheHelper.GetCache(XML_Cache_Key);
        //        if (custom == null)
        //        {
        //            // Models.Readfile.ReadXMLfile();
        //            custom = CacheHelper.GetCache(XML_Cache_Key);
        //        }
        //        #endregion
        //        if (custom != null)
        //        {
        //            dictionary = custom as Dictionary<string, string>;
        //            var ReaXML_InfoCode = CacheHelper.GetCache("ReaXML_InfoCode");
        //            if (ReaXML_InfoCode != null)
        //            {
        //                Dictionary<string, string> InfoCode = new Dictionary<string, string>();
        //                InfoCode = ReaXML_InfoCode as Dictionary<string, string>;
        //                switch (InfoCode[sessionKey])
        //                {
        //                    case "555002":
        //                        result = string.Format("<dl><dt><strong>{0}</strong></dt><dd><pre>{1}</pre></dd></dl>", sessionKey, dictionary[sessionKey.ToLower()]);
        //                        break;
        //                    default:
        //                        result = string.Format("<dl><dt><strong>{0}</strong></dt><dd>{1}</dd></dl>", sessionKey, dictionary[sessionKey.ToLower()]);
        //                        break;
        //                }
        //            }

        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        result = string.Empty;
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 是否存在 返回True代表存在 反之不存在
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static bool Exist(string key)
        //{
        //    bool result = false;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(ReaXML_InfoText[key]))
        //        {
        //            result = true;
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// XML修改
        ///// </summary>
        ///// <param name="key">修改ID</param>
        ///// <param name="Value">修改的新值</param>
        ///// <returns></returns>
        //public static bool Update(string key, string Value)
        //{
        //    bool result = false;
        //    try
        //    {
        //        if (Exist(key))
        //        {
        //            XmlDocument xml = new XmlDocument();
        //            xml.Load(XML_Path);
        //            XmlNodeList nodelist = xml.SelectNodes(XML_Path_Node);
        //            foreach (var item in nodelist)
        //            {
        //                XmlElement xe = (XmlElement)item;
        //                if (xe.GetAttribute(Info) == key)
        //                {
        //                    xe.SetAttribute(Text, Value);
        //                    result = true;
        //                    xml.Save(XML_Path);//保存。 
        //                    ReadXMLfile();
        //                    break;
        //                }
        //                else
        //                    continue;
        //            }
        //            ReadXMLfile();
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //    }
        //    return result;
        //}
        ////public static string Add(KnowledgeBase model, ClientAddress model2)
        ////{
        ////    string result = string.Empty;
        ////    try
        ////    {
        ////        if (!Exist(model.KnowName))
        ////        {
        ////            XmlDocument xml = new XmlDocument();
        ////            xml.Load(XML_Path);
        ////            XmlNode root = xml.SelectSingleNode(XML_Node_Fir);
        ////            XmlElement xe1 = xml.CreateElement(XML_Node_Chi);
        ////            xe1.SetAttribute(Info, model.KnowName);
        ////            xe1.SetAttribute(Text, model.KnowAnswer);
        ////            xe1.SetAttribute(Code, model.KnowType);
        ////            xe1.SetAttribute(userid, model.Ip);
        ////            xe1.SetAttribute(DateTime, System.DateTime.Now.ToString("yyyy-MM:dd HH:mm:ss"));
        ////            xe1.SetAttribute(address, model2.country + " " + model2.province + " " + model2.city + " " + model2.district);
        ////            root.AppendChild(xe1);
        ////            xml.Save(XML_Path);
        ////            result = "添加成功";
        ////            ReadXMLfile();
        ////        }
        ////        else
        ////            result = string.Format("系统中已经存在{0}的数据，请联系管理员", model.KnowName);
        ////    }
        ////    catch (System.Exception)
        ////    {
        ////        result = string.Format("保存发生异常失败");
        ////    }
        ////    return result;
        ////}
        ///// <summary>
        ///// XML删除
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static bool Delete(string key)
        //{
        //    bool result = false;
        //    try
        //    {
        //        if (Exist(key))
        //        {
        //            XmlDocument xml = new XmlDocument();
        //            xml.Load(XML_Path);
        //            XmlNodeList nodelist = xml.SelectNodes(XML_Path_Node);
        //            foreach (var item in nodelist)
        //            {
        //                XmlElement xe = (XmlElement)item;
        //                if (xe.GetAttribute(Info) == key)
        //                {
        //                    xe.RemoveAll();
        //                    result = true;
        //                    xml.Save(XML_Path);//保存。 
        //                    ReadXMLfile();
        //                    break;
        //                }
        //                else
        //                    continue;
        //            }
        //        }
        //    }
        //    catch (System.Exception)
        //    {
        //    }
        //    return result;
        //}
        #endregion

        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="strLog">记录到日志文件的信息</param>
        public  static void WriteLog(string UserId, string message, string session, string Client_IpAddress)
        {
            try
            {
                TruingLog log = new TruingLog();
                log.UserId = UserId;
                log.ID = System.Guid.NewGuid().ToString("N");
                log.SESSION = session;
                log.INFO = message;
                log.LAST_MODIFIED_TIME = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                new NET.BusinessRule.TruingLogBLL().Add(log);
                StringBuilder str = new StringBuilder();
                str.Append(log.UserId+"\t");
                str.Append(log.ID + "\t");
                str.Append(log.SESSION + "\t");
                str.Append(log.INFO + "\t");
                str.Append(log.LAST_MODIFIED_TIME + "\t");
                LoggerHelper.Info(str.ToString());
               
            }
            catch
            {

                #region 数据库插入失败写入TXT文件
                string DateTimeToTxt = System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string strPath = string.Empty;
                strPath = System.Configuration.ConfigurationManager.ConnectionStrings["DatabasePath"].ConnectionString;
                if (strPath == null)
                {
                    strPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    strPath = strPath + @"log";
                }
                else
                    strPath = strPath + @"\\log";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = strPath + "\\" + DateTimeToTxt;
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter thisStreamWriter = new StreamWriter(fs);
                thisStreamWriter.BaseStream.Seek(0, SeekOrigin.End);
                StringBuilder str = new StringBuilder();
                str.Append("[" + UserId + "]");
                str.Append("[" + Client_IpAddress + "]");
                str.Append("——[" + System.DateTime.Now.ToString("yy-MM-dd HH:mm:ss:ffff") + "]");
                str.Append("——[" + message + "]");
                if (session.Length > 20)
                {
                    str.Append("——[" + session.Substring(0, 20) + "]");
                }
                else
                    str.Append("——[" + session + "]");
                thisStreamWriter.WriteLine(str.ToString());
                thisStreamWriter.Flush();
                thisStreamWriter.Close();
                fs.Close();
                #endregion
                LoggerHelper.Info(str.ToString());
            }
        }
    }
}