using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;
using System.IO;

namespace DataBase
{
    public class ConnService : IDisposable
    {
        public const string DatabaseName = "\\CrawlerDb.db";
        public static string ConnectionStrPath
        {
            get
            {
                return @"E:\工作室\CodeWH\DatabaseFile\" + DatabaseName;
                // return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+ DatabaseName;
            }
            set { }
        }
        public static string ConnectionDatabaseStr(string path)
        {
            return string.Format("Data Source={0}", ConnectionStrPath);
        }
        public static string ConnectionDatabaseType = DbType.SqlLite.ToString();
        public static DbType DBTypeConnection
        {
            get
            {
                if (ConnectionDatabaseType == DbType.SqlServer.ToString())
                {
                    return DbType.SqlServer;
                }
                else if (ConnectionDatabaseType == DbType.MySql.ToString())
                {
                    return DbType.MySql;
                }
                else if (ConnectionDatabaseType == DbType.PostgreSQL.ToString())
                {
                    return DbType.PostgreSQL;
                }
                else
                    return DbType.SqlLite;
            }
        }
        #region 数据库连接对象
        private DbConnection connection = null;
        public DbConnection Connection
        {
            get
            {
                return connection;
            }
        }
        #endregion
        public ConnService()
        {
            if (connection == null)
            {
                switch (DBTypeConnection)
                {
                    case DbType.SqlLite:
                        if (!File.Exists(ConnectionStrPath))
                        {
                            ConnectionStrPath = System.IO.Directory.GetCurrentDirectory() + DatabaseName;
                            if (!File.Exists(ConnectionStrPath))
                                return;
                        }
                        connection = new SqliteConnection(ConnectionDatabaseStr(ConnectionStrPath));
                        connection.Open();
                        break;
                }
            }
        }
        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }
        public enum DbType
        {
            SqlServer,
            SqlLite,
            MySql,
            PostgreSQL
        }
    }

}
