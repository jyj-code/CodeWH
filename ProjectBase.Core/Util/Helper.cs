using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace ProjectBase.Core.Util
{
    public class Helper
    {
        #region 写日志
        private static object myLock = new object();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="folderPath">文件夹路径</param>
        /// <param name="content">日志内容</param>
        /// <param name="errName">产生的日志文件名字</param>
        public static void WriteLog(string folderPath, string content, string errName)
        {
            lock (myLock)
            {
                string date = errName;
                string logFolderPath = folderPath + "\\Log";
                string filePath = logFolderPath + "\\" + "日志" + "_" + date + ".txt";
                //创建目录Log
                DirectoryInfo dir = new DirectoryInfo(logFolderPath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                //创建日志文件
                FileInfo file = new FileInfo(filePath);
                if (!file.Exists)
                {
                    using (StreamWriter write = file.CreateText())
                    {
                        write.WriteLine("-------------------------日志------------------------------");
                    }
                }
                //文件已存在追加内容
                using (StreamWriter write = file.AppendText())
                {
                    content = string.Format("[{0}]:" + content, System.DateTime.Now.ToLongTimeString());
                    write.WriteLine(content);
                    write.WriteLine("-----------------------------------------------------------------");
                }
            }
        }

        #endregion

        #region 移除html标签
        /// <summary>
        /// 移除html标签
        /// </summary>
        /// <param name="html">传入html字符串</param>
        /// <returns>移除Html标签后的Html字符串</returns>
        public static string RemoveHtml(string html)
        {
            string m_outstr = "";
            m_outstr = html.Clone() as string;
            m_outstr = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
            m_outstr = new Regex(@"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
            m_outstr = new Regex(@"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(m_outstr, "");
            Regex objReg = new System.Text.RegularExpressions.Regex("(<[^>]+?>)|&nbsp;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg.Replace(m_outstr, "");
            Regex objReg2 = new System.Text.RegularExpressions.Regex("(\\s)+", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            m_outstr = objReg2.Replace(m_outstr, " ");
            return m_outstr;
        }
        #endregion

        #region 对抓取到的网页进行分析并输出 ResolverAndOutput
        /// <summary>
        /// 对抓取到的网页进行分析组合成有规律的数组，不过滤HTml
        /// </summary>
        /// <param name="result">result：抓取后待分析的网页</param>
        /// <param name="regexStr">regexStr：对整个网页进行正则截取时，正则开始标签</param>
        /// <param name="regexEnd">regexEnd：对整个网页进行正则截取时，正则结束标签</param>
        /// <param name="regexTab">regexTab：确定抓取范围后匹配某列的正则</param>
        /// <param name="ColNum">共有几列</param>
        /// <param name="IsRemoveHtml">是否移除Html</param>
        /// <returns></returns>
        /// 
        public static string ResolverAndOutput(string result, string regexStr, string regexEnd, string regexTab, int ColNum, bool IsRemoveHtml)
        {
            string strTempContent = "";
            string patternStart = regexStr;         //表达式开始标签,regexStr
            string patternEnd = regexEnd;           //表达式结束标签,regexEnd
            string regex = patternStart + @"([\s\S]*)" + patternEnd;          //组合后的表达式 
            //regex = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";           //匹配http:
            strTempContent = GetPatternHtml(regex, result, ColNum);        //通过正则表达式获得所需信息的大table
            if (strTempContent != "wrong")
            {
                strTempContent = strTempContent.Replace("\n", "");            //去掉\n符
                strTempContent = strTempContent.Replace("></td>", "> </td>");            //在<td></td>之间加入空字符，以便被正则
                //strTempContent = strTempContent.Replace("\r", "");          //去掉\r符
                string regex2 = regexTab;                                     //确定抓取范围后匹配某列的正则，regexTab
                strTempContent = GetPatternHtml(regex2, strTempContent, ColNum); //正则找到每列值
                if (IsRemoveHtml == true)
                {
                    strTempContent = RemoveHtml(strTempContent);         //正则移除html标签
                }
                if (strTempContent != "wrong")
                {
                    return strTempContent;
                }
                else
                {
                    string tmpNull = "";
                    for (int i = 0; i < ColNum; i++)
                    {
                        tmpNull = tmpNull + "Null$";
                    }
                    return tmpNull + "?";
                }
            }
            else
            {
                string retNull = "";
                for (int i = 0; i < ColNum; i++)
                {
                    retNull = retNull + "Null$";
                }
                return retNull + "?";
            }

        }
        #endregion

        #region 通过正则表达式，获取要得到的字符串信息
        /// <summary>
        /// 通过正则表达式，获取要得到的信息。
        /// </summary>
        /// <param name="pattern">传入正则表达式</param>
        /// <param name="THtml">传入被正则的html页</param>
        /// <param name="Col">要被正则的Html有几列。</param>
        /// <returns></returns>
        public static string GetPatternHtml(string pattern, string THtml, int Col)
        {
            Regex regex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            MatchCollection mc = regex.Matches(THtml);
            string strTempContent = "";
            if (mc.Count > 0)
            {
                int num = 1;
                foreach (Match matI in mc)
                {
                    strTempContent += matI.Groups[0].Value + "$";
                    if (num % Col == 0)
                    {
                        strTempContent += "~";
                    }
                    num = num + 1;
                }
            }
            else
            {
                strTempContent = "wrong";
            }
            return strTempContent;
        }
        #endregion

        #region 执行cmd命令
        /// <summary>
        /// 执行cmd命令
        /// 多命令请使用批处理命令连接符：
        /// <![CDATA[
        /// &:同时执行两个命令
        /// |:将上一个命令的输出,作为下一个命令的输入
        /// &&：当&&前的命令成功时,才执行&&后的命令
        /// ||：当||前的命令失败时,才执行||后的命令]]>
        /// 其他请百度
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="output"></param>
        public static string RunCmd(string cmd)
        {
            string output = string.Empty;
            cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            using (Process p = new Process())
            {
                p.StartInfo.FileName = StaticParameters.CmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
            }
            return output;
        }
        #endregion
        /// <summary>
        /// 对象交换
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        private static TOut TransExp<TIn, TOut>(TIn tIn)
        {
            Dictionary<string, object> _Dic = new Dictionary<string, object>();
            string key = string.Format("trans_exp_{0}_{1}", typeof(TIn).FullName, typeof(TOut).FullName);
            if (!_Dic.ContainsKey(key))
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TOut).GetProperties())
                {
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
                Func<TIn, TOut> func = lambda.Compile();

                _Dic[key] = func;
            }
            return ((Func<TIn, TOut>)_Dic[key])(tIn);
        }
    }
}
