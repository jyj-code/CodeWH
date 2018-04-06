namespace NET.Architect.Model
{
    public class UserInfoUnDeteil
    {
        /// <summary>
        /// True为合法用法，false说明进入了黑名单
        /// </summary>
        public bool Flage { get; set; }
        public string Key { get; set; }
        public UserInfo UserInfo { get; set; }
        public UserInfoDetail UserInfoDetail { get; set; }
        /// <summary>
        /// 前台发送的问题
        /// </summary>
        public string Message { get; set; }
    }
}
