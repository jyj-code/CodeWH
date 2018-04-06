using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ProjectBase.Core.Mail
{
    public class Mails
    {
        #region MyRegion
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">邮件内容</param>
        public void Send(string receiver, string body)
        {
            Send(new MailInfo { Receiver = receiver, ReceiverName = receiver, Body = body, Subject = body });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isSingleSend">是否群发单显</param>
        public void Send(string receiver, string body, bool isSingleSend)
        {
            Send(new MailInfo { Receiver = receiver, Body = body, Subject = body }, isSingleSend);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人姓名</param>
        /// <param name="body">邮件内容</param>
        public void Send(string receiver, string receiverName, string body)
        {
            Send(new MailInfo { Receiver = receiver, ReceiverName = receiverName, Body = body, Subject = body });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="receiverName">接收人姓名</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public void Send(string receiver, string receiverName, string subject, string body)
        {
            Send(new MailInfo { Receiver = receiver, ReceiverName = receiverName, Body = body, Subject = subject });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiver">接收人邮箱</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="isSingleSend">是否群发单显</param>
        public void Send(string receiver, string subject, string body, bool isSingleSend)
        {
            Send(new MailInfo { Receiver = receiver, Body = body, Subject = subject }, isSingleSend);
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="attachments">附件列表 System.Net.Mail.Attachment </param>
        public void Send(MailInfo info, params Attachment[] attachments)
        {
            var message = new MailMessage();
            foreach (var item in attachments)
            {
                message.Attachments.Add(item);
            }
            try
            {
                Send(info, message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="filePath">附件路径 System.String </param>
        public void Send(MailInfo info, string filePath)
        {
            var message = new MailMessage();
            message.Attachments.Add(new Attachment(filePath));
            Send(info, message);
        }

        /// <summary>
        /// 发送邮件（带附件）
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="filePath">附件路径 System.String </param>
        /// <param name="isSingleSend">是否群发单显</param>
        public void Send(MailInfo info, string filePath, bool isSingleSend)
        {
            var message = new MailMessage();
            message.Attachments.Add(new Attachment(filePath));
            Send(info, isSingleSend, message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="message">默认为null。 System.Net.Mail.MailMessage </param>
        public void Send(MailInfo info, MailMessage message = null)
        {
            Send(info, new MailConfig(), message);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="isSingleSend">是否群发单显</param>
        /// <param name="message">默认为null。 System.Net.Mail.MailMessage </param>
        public void Send(MailInfo info, bool isSingleSend, MailMessage message = null)
        {
            Send(info, new MailConfig(), message, isSingleSend);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="info">接收人信息 Mafly.Mail.MailInfo </param>
        /// <param name="mailConfig">发件邮箱配置</param>
        /// <param name="message">默认为null。 System.Net.Mail.MailMessage </param>
        /// <param name="isSingleSend">是否群发单显。当邮件接收人为多个时，可选择该模式，即可对多个收件人分别发送，收件方不会知道这封邮件有多个收件人</param>
        protected void Send(MailInfo info, MailConfig mailConfig, MailMessage message = null, bool isSingleSend = false)
        {
            message = message ?? new MailMessage();
            message.Subject = info.Subject;
            if (!string.IsNullOrEmpty(info.Replay))
                message.ReplyToList.Add(new MailAddress(info.Replay));
            message.Body = info.Body;
            if (!string.IsNullOrEmpty(info.CC))
                message.CC.Add(info.CC);

            //群发单显（当邮件接收人为多个时，可选择该模式，即可对多个收件人分别发送，收件方不会知道这封邮件有多个收件人）
            if (isSingleSend && info.Receiver.Contains(","))
            {
                foreach (var item in info.Receiver.Split(','))
                {
                    message.To.Clear();
                    message.To.Add(item);

                    SmtpClientSend(mailConfig, message);
                }
                return;
            }
            else
            {
                if (!isSingleSend && info.Receiver.Contains(","))
                    message.To.Add(info.Receiver);
                else
                    message.To.Add(new MailAddress(info.Receiver, string.IsNullOrEmpty(info.ReceiverName) ? info.Receiver : info.ReceiverName));
            }

            SmtpClientSend(mailConfig, message);
        }

        #endregion

        /// <summary>
        /// SmtpClientSend
        /// </summary>
        /// <param name="mailConfig"></param>
        /// <param name="message"></param>
        private void SmtpClientSend(MailConfig mailConfig, MailMessage message)
        {
            var sender = new SmtpClient();
            sender.UseDefaultCredentials = false;
            sender.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                message.IsBodyHtml = mailConfig.IsHtml;
                message.From = new MailAddress(mailConfig.From, mailConfig.DisplayName);
                sender.Host = mailConfig.Host;
                sender.Port = mailConfig.Port;
                sender.Credentials = new NetworkCredential(mailConfig.User, mailConfig.Password);
                sender.EnableSsl = mailConfig.EnableSsl;
                sender.Send(message);
            }
            catch
            {
                //发送异常一般为发送邮箱配置有误，这里提供一个默认发件邮箱。
                message.From = new MailAddress("NuGets@163.com", "Mafly");
                message.IsBodyHtml = true;
                sender.Host = "smtp.163.com";
                sender.Port = 25;
                sender.Credentials = new NetworkCredential("NuGets@163.com", "vzihlbquwnriqlht");
                sender.EnableSsl = false;
                sender.Send(message);
            }
        }
    }

    #region 发送邮件的信息
    /// <summary>
    ///     发送邮件的信息
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 主题行
        /// </summary>
        private string _subject;

        /// <summary>
        /// 接收者名字
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 接收者邮箱（多个用英文“,”号分割）
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 邮件的主题行
        /// </summary>
        public string Subject
        {
            get
            {
                if (string.IsNullOrEmpty(_subject) && _subject.Length > 15)
                {
                    return Body.Substring(0, 15);
                }
                return _subject;
            }
            set { _subject = value; }
        }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 抄送人集合
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 回复地址
        /// </summary>
        public string Replay { get; set; }

    }
    #endregion

    #region 邮件配置信息
    /// <summary>
    /// 邮件配置信息
    /// </summary>
    public class MailConfig
    {
        public MailConfig()
        {
            Host = "smtp.163.com";
            Port = 25;
            User = "nugets@163.com";
            From = User;
            Password = "vzihlbquwnriqlht";
            IsHtml = true;
            DisplayName = "系统自动发送";
            EnableSsl = false;
        }
        /// <summary>
        /// 主机名 如：smtp.163.com
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口号 如：25
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否包含Html代码
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// 发送者显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 是否启用SSL 默认：false 
        /// 如果启用 端口号要改为加密方式发送的
        /// </summary>
        public bool EnableSsl { get; set; }
    }
    #endregion
}
