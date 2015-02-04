using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using System.Data;
using System.Data.SqlClient;

/**
 * SqlServerSmo 
 *
 * SqlServerSmo Implementation of absSmo for SQL Server
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
    /// Implementation of absSmo for SQL Server
    /// </summary>
    class SqlServerSmo : absSmo
    {
        /// <summary>
        /// Server Connection
        /// </summary>
        private ServerConnection connection = null;

        /// <summary>
        /// Directory Base
        /// </summary>
        private string basedir = "";

        /// <summary>
        /// DataBase Name
        /// </summary>
        private string dbname = "";

        /// <summary>
        /// SQL Native Connection
        /// </summary>
        private SqlConnection conn = null;

        /// <summary>
        /// Constructor of Class
        /// </summary>
        /// <param name="baseDir">
        /// Base Directory to save SQL Scripts
        /// </param>
        /// <param name="serverName">
        /// SQL Server Name
        /// </param>
        /// <param name="userDb">
        /// User of SQL Server
        /// </param>
        /// <param name="password">
        /// Password of User
        /// </param>
        /// <param name="dbName">
        /// DataBase Name
        /// </param>
        public SqlServerSmo(string baseDir, string serverName, string userDb, string password, string dbName)
        {
            // SMO Connection
            try
            {
                basedir = baseDir;
                dbname = dbName;
                connection = new ServerConnection(serverName, userDb, password);
            }
            catch (Exception e)
            {
                connection = null;
            }

            // Native Connection
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=" + dbName + ";uid=" + userDb + ";pwd=" + password + ";";

            try
            {
                conn.Open();

                if (!(conn.State == ConnectionState.Open))
                {
                    conn = null;
                }
            }
            catch (Exception e)
            {
                connection = null;
            }
        }

        /// <summary>
        /// Checks if Connected to SQL Server
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public override bool isConnected()
        {
            return connection == null ? false : true;
        }

        /// <summary>
        /// Encrypt Stored Procedures
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int encryptProcedures()
        {
            int retValue = 0;

            if (!(Directory.Exists(basedir)))
                retValue = 1;
            
            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                try
                {
                    Server SmoServer = new Server(connection);

                    foreach (StoredProcedure sp in SmoServer.Databases[dbname].StoredProcedures)
                    {
                        if (!(sp.IsSystemObject))
                        {
                            try
                            {
                                sp.TextMode = false;
                                sp.IsEncrypted = true;
                                sp.Alter();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Encrypting Procedure: " + sp.Name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    retValue = 3;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Encrypt User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int encryptFunctions()
        {
            int retValue = 0;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                try
                {
                    Server SmoServer = new Server(connection);

                    foreach (UserDefinedFunction fn in SmoServer.Databases[dbname].UserDefinedFunctions)
                    {
                        if (!(fn.IsSystemObject))
                        {
                            try
                            {
                                fn.TextMode = false;
                                fn.IsEncrypted = true;
                                fn.Alter();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Encrypting User Function: " + fn.Name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    retValue = 3;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Save DDL Scripts of Stored Procedures
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveProcedures()
        {
            int retValue = 0;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                deleteDir(this.basedir + "Procedures/");

                try
                {
                    Server SmoServer = new Server(connection);

                    foreach (StoredProcedure sp in SmoServer.Databases[dbname].StoredProcedures)
                    {
                        if (!(sp.IsSystemObject))
                        {
                            try
                            {
                                string spFile = this.basedir + "Procedures/" + sp.Schema + "." + sp.Name + ".sql";
                                string spContent = sp.TextHeader + "\n" + sp.TextBody;

                                saveFile(spFile, spContent);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Saving Procedure: " + sp.Name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    retValue = 3;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveFunctions()
        {
            int retValue = 0;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                base.deleteDir(this.basedir + "Functions/");

                try
                {
                    Server SmoServer = new Server(connection);

                    foreach (UserDefinedFunction fn in SmoServer.Databases[dbname].UserDefinedFunctions)
                    {
                        if (!(fn.IsSystemObject))
                        {
                            try
                            {
                                string spFile = this.basedir + "Functions/" + fn.Schema + "." + fn.Name + ".sql";
                                string fnContent = fn.TextHeader + "\n" + fn.TextBody;

                                base.saveFile(spFile, fnContent);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Saving Function: " + fn.Name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    retValue = 3;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveTables()
        {
            int retValue = 0;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                deleteDir(this.basedir + "Tables/");
                
                try
                {
                    Server SmoServer = new Server(connection);

                    foreach (Table tb in SmoServer.Databases[dbname].Tables)
                    {
                        if (!(tb.IsSystemObject))
                        {
                            try
                            {
                                string spFile = this.basedir + "Tables/" + tb.Schema + "." + tb.Name + ".sql";
                                string tbContent = "";

                                foreach (string strScript in tb.Script())
                                {
                                    tbContent = tbContent + strScript + "\n";
                                }

                                saveFile(spFile, tbContent);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error Saving Table: " + tb.Name);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    retValue = 3;
                }
            }

            return retValue;
        }

        /// <summary>
        /// Gets a DataSet with DataBases List
        /// </summary>
        /// <returns>
        /// DataSet
        /// </returns>
        public override DataSet getDbList()
        {
            if (conn != null)
            {
                String query = "SELECT sdb.name AS DBSCHEMAS FROM master..sysdatabases sdb ";
                query += " WHERE sdb.name NOT IN ('master','model','msdb','pubs','northwind','tempdb')";
                query += " ORDER BY sdb.name";

                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter();
                DataSet sqlDs = new DataSet();

                sqlAdapter.SelectCommand = sqlCommand;
                sqlAdapter.Fill(sqlDs);

                return sqlDs;
            }
            else
                return null;
        }
    }
}
