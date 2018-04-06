using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDBHelper
    {
        /// <summary>
        /// 根据SQL从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        List<T> ReadCollection<T>(string sql, DynamicParameters p) where T : class;
        /// <summary>
        /// 根据存储过程从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        List<T> ReadCollectionBySP<T>(string procName, DynamicParameters p) where T : class;
        /// <summary>
        /// 根据存储过程从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        List<T> ReadCollectionBySP<T>(string procName, ref DynamicParameters p) where T : class;
        /// <summary>
        /// 根据SQL从数据库中读取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>查询出的对象</returns>
        T ReadObject<T>(string sql, DynamicParameters p) where T : class;
        /// <summary>
        /// 根据存储过程从数据库中读取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>查询出的对象</returns>
        T ReadObjectBySP<T>(string procName, DynamicParameters p) where T : class;
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        void ExcuteSQL(string sql, DynamicParameters p);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        void ExcuteSP(string procName, DynamicParameters p);
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>int型返回值</returns>
        int ExecuteScalar(string sql, DynamicParameters p);
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>decimal型返回值</returns>
        decimal ExecuteScalarDecimal(string sql, DynamicParameters p);
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>float型返回值</returns>
        float ExecuteScalarfloat(string sql, DynamicParameters p);
        /// <summary>
        /// 查询字符串类型的单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>string型返回值</returns>
        string ExecuteScalarString(string sql, DynamicParameters p);
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>int型返回值</returns>
        int ExecuteScalarBySP(string procName, DynamicParameters p);
        /// <summary>
        /// 将一个集合保存到数据库
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="sql">sql语句,示例 insert AUT_User (Name,Age) values(@Name, @Age)</param>
        /// <param name="collection">集合对象</param>
        void SaveCollection<T>(string sql, IList<T> collection);
        /// <summary>
        /// 判断数据库中是否存在该数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>存在返回 True</returns>
        bool Exists(string sql, DynamicParameters p);
    }
}
