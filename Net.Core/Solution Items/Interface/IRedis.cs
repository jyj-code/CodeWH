using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interface
{
   public interface IRedis
    {
        #region 写入方法  
        bool Insert<T>(string key, T Value);
        bool Insert<T>(string key, T Value, TimeSpan expiresIn);
        bool Insert<T>(string key, T Value, DateTime expiresAt);
        #endregion
        #region 获取方法
        T Get<T>(string key);
        List<T> GetList<T>(string key);
        #endregion
        bool Remove(string key);
    }
}
