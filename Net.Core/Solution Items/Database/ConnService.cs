using Common;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseOperation
{
    public class ConnService : IDisposable
    {

        public static DbType DBTypeConnection
        {
            get
            {
                if (StaticToolHelp.ConnectionDatabaseType.Contains("SqlServer"))
                {
                    return DbType.SqlServer; 
                }
                else if (StaticToolHelp.ConnectionDatabaseType.Contains("MySql"))
                {
                    return DbType.MySql;
                }
                else if (StaticToolHelp.ConnectionDatabaseType.Contains("PostgreSQL"))
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
                        if (!File.Exists(StaticToolHelp.ConnectionDatabaseStr.Substring(StaticToolHelp.ConnectionDatabaseStr.IndexOf(":") - 1)))
                        {
                            StaticToolHelp.ConnectionDatabaseStr = StaticToolHelp.ConnectionDatabaseStr.Substring(0, StaticToolHelp.ConnectionDatabaseStr.IndexOf(":") - 1) + System.IO.Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileName(StaticToolHelp.ConnectionDatabaseStr);
                        }
                        connection = new SqliteConnection(StaticToolHelp.ConnectionDatabaseStr);
                        break;
                    case DbType.MySql:
                        connection = new MySql.Data.MySqlClient.MySqlConnection(StaticToolHelp.ConnectionDatabaseStr);
                        break;
                    case DbType.PostgreSQL:
                        connection = new Npgsql.NpgsqlConnection(StaticToolHelp.ConnectionDatabaseStr);
                        break;
                    default:
                        
                    connection = new SqlConnection(StaticToolHelp.ConnectionDatabaseStr);
                        break;
                }
                connection.Open();
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
