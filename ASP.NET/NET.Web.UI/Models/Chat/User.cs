using Microsoft.AspNet.SignalR.Infrastructure;
using System.Collections.Generic;

namespace NET.Web.UI.Models.Chat
{
    public class User
    {
        public string ConnectionId { get; set; }
        /// <summary>
        /// 用户的连接集合
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// 用户房间集合，一个用户可以加入多个房间
        /// </summary>
        public List<ChatRoom> Rooms { get; set; }

        public User()
        {
            Connections = new List<Connection>();
            Rooms = new List<ChatRoom>();
        }
    }
}