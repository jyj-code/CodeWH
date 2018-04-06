using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;


namespace NET.DataAccessLayer.Common
{
    public class ConnService : IDisposable
    {
        public const string DatabaseName = "\\KbueTruing.db";
        public static string ConnectionStrPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DatabasePath"].ConnectionString + DatabaseName;
                // return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+ DatabaseName;
            }
            set { }
        }
        public static string ConnectionDatabaseStr(string path)
        {
            return string.Format("Data Source={0}", ConnectionStrPath);
        }
        public static string ConnectionDatabaseType { get { return "SqlLite"; } }
        public static DbType DBTypeConnection
        {
            get
            {
                if (ConnectionDatabaseType.Contains("SqlServer"))
                {
                    return DbType.SqlServer;
                }
                else if (ConnectionDatabaseType.Contains("MySql"))
                {
                    return DbType.MySql;
                }
                else if (ConnectionDatabaseType.Contains("PostgreSQL"))
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
                        connection = new SQLiteConnection(ConnectionDatabaseStr(ConnectionStrPath));
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
