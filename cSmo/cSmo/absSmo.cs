using System;
using System.IO;
using System.Data;

// absSmo 
//
// absSmo Abstract Class to Manage SMO Functions
//
// Copyright 2015 Jorge Alberto Ponce Turrubiates
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace cSmo
{
    /// <summary>
    /// Abstract Class to Manage SMO Functions
    /// </summary>
    abstract class absSmo
    {
        /// <summary>
        /// Checks if Connected to SQL Server
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public abstract bool isConnected();

        /// <summary>
        /// Encrypt Stored Procedures
        /// </summary>
        /// <returns>
        /// int
        /// </returns>
        public abstract int encryptProcedures();

        /// <summary>
        /// Encrypt User Functions
        /// </summary>
        /// <returns>
        /// int
        /// </returns>
        public abstract int encryptFunctions();

        /// <summary>
        /// Save DDL Scripts of Stored Procedures
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// int
        /// </returns>
        public abstract int saveProcedures(string dir = "");

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// int
        /// </returns>
        public abstract int saveFunctions(string dir = "");

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <param name="dir">
        /// Optional Dir
        /// </param>
        /// <returns>
        /// int
        /// </returns>
        public abstract int saveTables(string dir = "");

        /// <summary>
        /// Gets a DataSet with DataBases List
        /// </summary>
        /// <returns>
        /// DataSet
        /// </returns>
        public abstract DataSet getDbList();

        /// <summary>
        /// Save File of DDL Script
        /// </summary>
        /// <param name="file">
        /// Full Path of File to Save
        /// </param>
        /// <param name="content">
        /// Content of File
        /// </param>
        /// <returns>
        /// bool
        /// </returns>
        protected bool saveFile(string file, string content)
        {
            bool retValue = true;

            try
            {
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(content);
                sw.Close();
            }
            catch(Exception e)
            {
                retValue = false;
            }

            return retValue;
        }

        /// <summary>
        /// Delete and Create Directory
        /// </summary>
        /// <param name="dir">
        /// Full Path to Directory
        /// </param>
        protected void deleteDir(string dir)
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);

            Directory.CreateDirectory(dir);
        }
    }
}
