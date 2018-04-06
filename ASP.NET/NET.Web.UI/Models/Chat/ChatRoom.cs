using System.Collections.Generic;

namespace NET.Web.UI.Models.Chat
{
    /// <summary>
    /// 房间类
    /// </summary>
    public class ChatRoom
    {
        public ChatRoom()
        {
            Users = new List<User>();
        }
        public string RoomId { get; set; }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public List<User> Users { get; set; }
    }
}