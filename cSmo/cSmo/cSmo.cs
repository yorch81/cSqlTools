using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

/**
 * cSmo 
 *
 * cSmo Implementation of SMO Factory
 *
 * Copyright 2015 Jorge Alberto Ponce Turrubiates
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
namespace cSmo
{
    /// <summary>
    /// SMO Factory Class
    /// </summary>
    public class cSmo
    {
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
        /// Gets a Factory Instance
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
            return new cSmo(connType, basedir, server, user, password, dbName);
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
        public int saveProcedures(string dir = "")
        {
            return smo.saveProcedures(dir);
        }

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int saveFunctions(string dir = "")
        {
            return smo.saveFunctions(dir);
        }

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public int saveTables(string dir = "")
        {
            return smo.saveTables(dir);
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

        /// <summary>
        /// Gets DataBases List
        /// </summary>
        /// <param name="connType">
        /// 1 MSSQLSERVER 2 MYSQL
        /// </param>
        /// <param name="serverName">
        /// Server Name
        /// </param>
        /// <param name="userDb">
        /// DataBase User</param>
        /// <param name="password">
        /// Password
        /// </param>
        /// <returns>
        /// DataSet
        /// </returns>
        public static DataSet getDataBases(int connType, string serverName, string userDb, string password)
        {
            if (connType == MSSQLSERVER)
            {
                // Native Connection
                SqlConnection tConn = new SqlConnection();
                tConn.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=master;uid=" + userDb + ";pwd=" + password + ";";

                DataSet sqlDs = new DataSet();

                try
                {
                    tConn.Open();

                    if (!(tConn.State == ConnectionState.Open))
                    {
                        tConn = null;
                    }

                    if (tConn != null)
                    {
                        String query = "SELECT sdb.name AS DBSCHEMAS FROM master..sysdatabases sdb ";
                        query += " WHERE sdb.name NOT IN ('master','model','msdb','pubs','northwind','tempdb')";
                        query += " ORDER BY sdb.name";

                        SqlCommand sqlCommand = new SqlCommand(query, tConn);
                        SqlDataAdapter sqlAdapter = new SqlDataAdapter();

                        sqlAdapter.SelectCommand = sqlCommand;
                        sqlAdapter.Fill(sqlDs);

                        tConn = null;
                    }
                }
                catch (Exception e)
                {
                    tConn = null;
                    sqlDs = null;
                }

                return sqlDs;
            }
            else
            {
                //MySQL
                MySqlConnection tConn = new MySqlConnection();

                tConn.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=test;uid=" + userDb + ";pwd=" + password + ";";

                try
                {
                    tConn.Open();

                    if (!(tConn.State == ConnectionState.Open))
                    {
                        tConn = null;
                    }
                }
                catch (Exception e)
                {
                    tConn = null;
                }

                if (tConn != null)
                {
                    String query = "SELECT SCHEMA_NAME AS DBSCHEMAS";
                    query += " FROM INFORMATION_SCHEMA.SCHEMATA";
                    query += " WHERE SCHEMA_NAME NOT IN ('information_schema', 'mysql', 'performance_schema', 'test')";
                    query += " ORDER BY SCHEMA_NAME;";

                    MySqlCommand myCommand = new MySqlCommand(query, tConn);
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                    DataSet myDs = new DataSet();

                    myAdapter.SelectCommand = myCommand;
                    myAdapter.Fill(myDs);

                    return myDs;
                }
                else
                    return null;
            }
        }
    }
}
