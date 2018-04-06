using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    //public class RedisBase : IRedis
    //{
    //    private RedisClient client;
    //    public RedisBase()
    //    {
    //        //连接服务器  
    //        client = new RedisClient("127.0.0.1", 6379);
    //    }
    //    #region 获取
    //    public T Get<T>(string key)
    //    {
    //        return client.Get<T>(key);
    //    }
    //    public List<T> GetList<T>(string key)
    //    {
    //        return client.Get<List<T>>(key);
    //    }
    //    #endregion
    //    #region 写入方法
    //    public bool Insert<T>(string key, T Value)
    //    {
    //        return client.Set<T>(key, Value);
    //    }
    //    public bool Insert<T>(string key, T Value, TimeSpan expiresIn)
    //    {
    //        return client.Set<T>(key, Value, expiresIn);
    //    }

    //    public bool Insert<T>(string key, T Value, DateTime expiresAt)
    //    {
    //        return client.Set<T>(key, Value, expiresAt);
    //    }
    //    #endregion
    //    #region 移除
    //    public bool Remove(string key)
    //    {
    //        return client.Remove(key);
    //    }
    //    #endregion
    //}

}
