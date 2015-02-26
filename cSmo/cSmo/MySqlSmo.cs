using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

/**
 * MySqlSmo 
 *
 * MySqlSmo Implementation of absSmo for MySQL
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
    /// Implementation of absSmo for MySQL
    /// </summary>
    class MySqlSmo : absSmo
    {
        /// <summary>
        /// MySQL Connection
        /// </summary>
        private MySqlConnection connection = null;

        /// <summary>
        /// Directory Base
        /// </summary>
        private string basedir = "";

        /// <summary>
        /// Constructor of Class
        /// </summary>
        /// <param name="baseDir">
        /// Base Directory to save SQL Scripts
        /// </param>
        /// <param name="serverName">
        /// MySQL Server Name
        /// </param>
        /// <param name="userDb">
        /// User of MySQL
        /// </param>
        /// <param name="password">
        /// Password of User
        /// </param>
        /// <param name="dbName">
        /// DataBase Name
        /// </param>
        public MySqlSmo(string baseDir, string serverName, string userDb, string password, string dbName)
        {
            connection = new MySqlConnection();

            connection.ConnectionString = "Data Source=" + serverName + ";Initial Catalog=" + dbName + ";uid=" + userDb + ";pwd=" + password + ";";

            try
            {
                basedir = baseDir;

                connection.Open();

                if (!(connection.State == ConnectionState.Open))
                {
                    connection = null;
                }
            }
            catch (Exception e)
            {
                connection = null;
            }
        }

        /// <summary>
        /// Checks if Connected to MySQL
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public override bool isConnected()
        {
            return connection == null ? false : true;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <returns></returns>
        public override int encryptProcedures()
        {
            //Not Implemented
            return 0;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <returns></returns>
        public override int encryptFunctions()
        {
            //Not Implemented
            return 0;
        }

        /// <summary>
        /// Save DDL Scripts of Stored Procedures
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveProcedures(string dir = "")
        {
            int retValue = 0;

            if (dir.Length > 0)
                this.basedir = dir;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                deleteDir(this.basedir + "Procedures/");

                DataTable tProcedures = getProcedures();

                for (int i = 0; i < tProcedures.Rows.Count; i++)
                {
                    string procedureName = (string) tProcedures.Rows[i].ItemArray[0];
                    string spFile = this.basedir + "Procedures/" + procedureName + ".sql";

                    string procedureText =  getProcedureText(procedureName);

                    saveFile(spFile, procedureText);
                }
            }

            return retValue;
        }

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveFunctions(string dir = "")
        {
            int retValue = 0;

            if (dir.Length > 0)
                this.basedir = dir;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                deleteDir(this.basedir + "Functions/");

                DataTable tFunctions = getFunctions();

                for (int i = 0; i < tFunctions.Rows.Count; i++)
                {
                    string functionName = (string)tFunctions.Rows[i].ItemArray[0];
                    string fnFile = this.basedir + "Functions/" + functionName + ".sql";

                    string functionText = getFunctionText(functionName);

                    saveFile(fnFile, functionText);
                }
            }

            return retValue;
        }

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveTables(string dir = "")
        {
            int retValue = 0;

            if (dir.Length > 0)
                this.basedir = dir;

            if (!(Directory.Exists(basedir)))
                retValue = 1;

            if (!isConnected())
                retValue = 2;

            if (retValue == 0)
            {
                deleteDir(this.basedir + "Tables/");

                DataTable tTables = getTables();

                for (int i = 0; i < tTables.Rows.Count; i++)
                {
                    string tableName = (string)tTables.Rows[i].ItemArray[0];
                    string tbFile = this.basedir + "Tables/" + tableName + ".sql";

                    string tableText = getTableScript(tableName);

                    saveFile(tbFile, tableText);
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
            if (isConnected())
            {
                String query = "SELECT SCHEMA_NAME AS DBSCHEMAS";
                query += " FROM INFORMATION_SCHEMA.SCHEMATA";
                query += " WHERE SCHEMA_NAME NOT IN ('information_schema', 'mysql', 'performance_schema', 'test')";
                query += " ORDER BY SCHEMA_NAME;";

                MySqlCommand myCommand = new MySqlCommand(query, connection);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                DataSet myDs = new DataSet();

                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(myDs);

                return myDs;
            }
            else
                return null;
        }

        /// <summary>
        /// Gets Text Script of Stored Procedure
        /// </summary>
        /// <param name="procedureName">
        /// Stored Procedure Name
        /// </param>
        /// <example>
        /// schema.procedure_name
        /// </example>
        /// <returns>String</returns>
        private String getProcedureText(string procedureName)
        {
            String retVale = "";

            String query = "SHOW CREATE PROCEDURE " + procedureName + ";";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = (string)myDs.Tables[0].Rows[0].ItemArray[2];

            return retVale;
        }

        /// <summary>
        /// Gets Text Script of User Function
        /// </summary>
        /// <param name="functionName">
        /// User Function Name
        /// </param>
        /// <example>
        /// schema.function_name
        /// </example>
        /// <returns>String</returns>
        private String getFunctionText(string functionName)
        {
            String retVale = "";

            String query = "SHOW CREATE FUNCTION " + functionName + ";";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = (string)myDs.Tables[0].Rows[0].ItemArray[2];

            return retVale;
        }

        /// <summary>
        /// Gets Text Script of User Table
        /// </summary>
        /// <param name="tableName">
        /// User Table Name
        /// </param>
        /// <example>
        /// schema.table_name
        /// </example>
        /// <returns>String</returns>
        private String getTableScript(string tableName)
        {
            String retVale = "";

            String query = "SHOW CREATE TABLE " + tableName + ";";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = (string)myDs.Tables[0].Rows[0].ItemArray[1];

            return retVale;
        }

        /// <summary>
        /// Gets DataTable of Stored Procedures
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable getProcedures()
        {
            DataTable retVale = null;

            String query = "SELECT concat(ROUTINE_SCHEMA, '.', ROUTINE_NAME) AS PROCEDURES";
            query += " FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
            query += " AND ROUTINE_SCHEMA NOT IN ('information_schema', 'mysql', 'performance_schema', 'test')";
            query += " ORDER BY ROUTINE_SCHEMA, ROUTINE_NAME;";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = myDs.Tables[0];

            return retVale;
        }

        /// <summary>
        /// Gets DataTable of User Functions
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable getFunctions()
        {
            DataTable retVale = null;

            String query = "SELECT concat(ROUTINE_SCHEMA, '.', ROUTINE_NAME) AS FUNCTIONS";
            query += " FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'FUNCTION'";
            query += " AND ROUTINE_SCHEMA NOT IN ('information_schema', 'mysql', 'performance_schema', 'test')";
            query += " ORDER BY ROUTINE_SCHEMA, ROUTINE_NAME;";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = myDs.Tables[0];

            return retVale;
        }

        /// <summary>
        /// Gets DataTable of User Tables
        /// </summary>
        /// <returns>DataTable</returns>
        private DataTable getTables()
        {
            DataTable retVale = null;

            String query = "SELECT concat(TABLE_SCHEMA, '.', TABLE_NAME) AS TABLES FROM INFORMATION_SCHEMA.TABLES";
            query += " WHERE TABLE_SCHEMA NOT IN ('information_schema', 'mysql', 'performance_schema', 'test')";
            query += " AND TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_SCHEMA, TABLE_NAME;";

            MySqlCommand myCommand = new MySqlCommand(query, connection);
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            DataSet myDs = new DataSet();

            myAdapter.SelectCommand = myCommand;
            myAdapter.Fill(myDs);

            retVale = myDs.Tables[0];

            return retVale;
        }
    }
}
