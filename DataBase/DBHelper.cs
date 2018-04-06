using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DataBase
{
    public class DBHelper
    {
        /// <summary>
        /// 根据SQL从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        public static List<T> ReadCollection<T>(string sql, DynamicParameters p) where T : class
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<T>(sql, p).ToList<T>();
            }
        }
        /// <summary>
        /// 根据存储过程从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        public static List<T> ReadCollectionBySP<T>(string procName, DynamicParameters p) where T : class
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<T>(procName, p, commandType: CommandType.StoredProcedure).ToList<T>();
            }
        }
        /// <summary>
        /// 根据存储过程从数据库中读取对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>对象集合</returns>
        public static List<T> ReadCollectionBySP<T>(string procName, ref DynamicParameters p) where T : class
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<T>(procName, p, commandType: CommandType.StoredProcedure).ToList<T>();
            }
        }
        /// <summary>
        /// 根据SQL从数据库中读取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>查询出的对象</returns>
        public static T ReadObject<T>(string sql, DynamicParameters p) where T : class
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<T>(sql, p).SingleOrDefault<T>();
            }
        }
        /// <summary>
        /// 根据存储过程从数据库中读取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>查询出的对象</returns>
        public static T ReadObjectBySP<T>(string procName, DynamicParameters p) where T : class
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<T>(procName, p, commandType: CommandType.StoredProcedure).SingleOrDefault<T>();
            }
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        public static int ExcuteSQL(string sql, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
               return connService.Connection.Execute(sql, p);
            }
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        public static void ExcuteSQL(string sql, DynamicParameters p, ConnService connService, IDbTransaction transaction)
        {
            connService.Connection.Execute(sql, p, transaction);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        public static void ExcuteSP(string procName, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                connService.Connection.Execute(procName, p, commandTimeout: 1800, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>int型返回值</returns>
        public static int ExecuteScalar(string sql, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<int>(sql, p).First();
            }
        }
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>decimal型返回值</returns>
        public static decimal ExecuteScalarDecimal(string sql, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<decimal>(sql, p).First();
            }
        }
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>float型返回值</returns>
        public static float ExecuteScalarfloat(string sql, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<float>(sql, p).First();
            }
        }
        /// <summary>
        /// 查询字符串类型的单个返回值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>string型返回值</returns>
        public static string ExecuteScalarString(string sql, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<string>(sql, p).First();
            }
        }
        /// <summary>
        /// 查询单个返回值
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="p">参数集合</param>
        /// <returns>int型返回值</returns>
        public static int ExecuteScalarBySP(string procName, DynamicParameters p)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Query<int>(procName, p).First();
            }
        }
        /// <summary>
        /// 将一个集合保存到数据库
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="sql">sql语句,示例 insert AUT_User (Name,Age) values(@Name, @Age)</param>
        /// <param name="collection">集合对象</param>
        public static int SaveCollection<T>(string sql, IList<T> collection)
        {
            using (ConnService connService = new ConnService())
            {
              return  connService.Connection.Execute(sql, collection, null, 1800);
            }
        }
        public static int SaveCollection<T>(string sql, T collection)
        {
            using (ConnService connService = new ConnService())
            {
                return connService.Connection.Execute(sql, collection, null, 1800);
            }
        }
        /// <summary>
        /// 将一个集合保存到数据库
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="sql">sql语句,示例 insert AUT_User (Name,Age) values(@Name, @Age)</param>
        /// <param name="collection">集合对象</param>
        public static void SaveCollection<T>(string sql, IList<T> collection, ConnService connService, IDbTransaction transaction)
        {
            connService.Connection.Execute(sql, collection, transaction);
        }
        /// <summary>
        /// 判断数据库中是否存在该数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="p">参数集合</param>
        /// <returns>存在返回 True</returns>
        public static bool Exists(string sql, DynamicParameters p)
        {
            int recordCount = 0;
            using (ConnService connService = new ConnService())
            {
                recordCount = connService.Connection.Query<int>(sql, p).First();
            }
            return (recordCount > 0) ? true : false;
        }
    }
}
