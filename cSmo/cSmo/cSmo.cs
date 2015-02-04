using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace cSmo
{
    public class cSmo
    {
        /// <summary>
        /// INSTANCE
        /// </summary>
        private static cSmo INSTANCE = null;

        /// <summary>
        /// Abstract SMO Instance
        /// </summary>
        private absSmo smo = null;

        /// <summary>
        /// MicroSoft SQL Server
        /// </summary>
        public const int MSSQLSERVER = 1;

        /// <summary>
        /// MySQL
        /// </summary>
        public const int MYSQL = 2;

        /// <summary>
        /// Private Constructor of Class
        /// </summary>
        /// <param name="connType">
        /// 1 MSSQLSERVER 2 MYSQL
        /// </param>
        /// <param name="basedir">
        /// Base Directory to save SQL Scripts
        /// </param>
        /// <param name="server">
        /// DataBase Server Name
        /// </param>
        /// <example>
        /// localhost, localhost\MSSQLSERVER_INSTANCE
        /// </example>
        /// <param name="user">
        /// User Name
        /// </param>
        /// <param name="password">
        /// Password of User
        /// </param>
        /// <param name="dbName">
        /// DataBase Name
        /// </param>
        private cSmo(int connType, string basedir, string server, string user, string password, string dbName)
        {
            if (connType == cSmo.MSSQLSERVER)
            {
                smo = new SqlServerSmo(basedir, server, user, password, dbName);
            }
            else
            {
                smo = new MySqlSmo(basedir, server, user, password, dbName);
            }
        }

        /// <summary>
        /// Gets a Singleton Instance
        /// </summary>
        /// <param name="connType">
        /// 1 MSSQLSERVER 2 MYSQL
        /// </param>
        /// <param name="basedir">
        /// Base Directory to save SQL Scripts
        /// </param>
        /// <param name="server">
        /// DataBase Server Name
        /// </param>
        /// <example>
        /// localhost, localhost\MSSQLSERVER_INSTANCE
        /// </example>
        /// <param name="user">
        /// User Name
        /// </param>
        /// <param name="password">
        /// Password of User
        /// </param>
        /// <param name="dbName">
        /// DataBase Name
        /// </param>
        public static cSmo getInstance(int connType, string basedir, string server, string user, string password, string dbName)
        {
            if (INSTANCE == null)
            {
                INSTANCE = new cSmo(connType, basedir, server, user, password, dbName);
            }

            return INSTANCE;
        }

        /// <summary>
        /// Checks if Connected
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public bool isConnected()
        {
            return smo.isConnected();
        }

        /// <summary>
        /// Encrypt Stored Procedures
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int encryptProcedures()
        {
            return smo.encryptProcedures();
        }

        /// <summary>
        /// Encrypt User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int encryptFunctions()
        {
            return smo.encryptFunctions();
        }

        /// <summary>
        /// Save DDL Scripts of Stored Procedures
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int saveProcedures()
        {
            return smo.saveProcedures();
        }

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int saveFunctions()
        {
            return smo.saveFunctions();
        }

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int saveTables()
        {
            return smo.saveTables();
        }

        /// <summary>
        /// Gets a DataSet with DataBases List
        /// </summary>
        /// <returns>
        /// DataSet
        /// </returns>
        public DataSet getDbList()
        {
            return smo.getDbList();
        }
    }
}
