using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
        /// Checks if Connected to SQL Server
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public override bool isConnected()
        {
            throw new NotImplementedException();
        }

        public override int encryptProcedures()
        {
            throw new NotImplementedException();
        }

        public override int encryptFunctions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save DDL Scripts of Stored Procedures
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveProcedures()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save DDL Scripts of User Functions
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveFunctions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save DDL Scripts of User Tables
        /// </summary>
        /// <returns>
        /// 0 successful 1 Directory not Exists 2 Not Connected 3 DataBase not Exists
        /// </returns>
        public override int saveTables()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a DataSet with DataBases List
        /// </summary>
        /// <returns>
        /// DataSet
        /// </returns>
        public override DataSet getDbList()
        {
            throw new NotImplementedException();
        }
    }
}
