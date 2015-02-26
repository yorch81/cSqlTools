using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * myConfig 
 *
 * myConfig Singleton Class for General Configurations
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
namespace cSqlTools
{
    /// <summary>
    /// General Configurations
    /// </summary>
    class myConfig
    {
        /// <summary>
        /// Current Directory Application
        /// </summary>
        public String CurrentDir { get; set; }
        
        /// <summary>
        /// Connection Type
        /// </summary>
        public int dbType { get; set; }

        /// <summary>
        /// Base Path
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// DataBase Server
        /// </summary>
        public string serverDb { get; set; }

        /// <summary>
        /// DataBase User
        /// </summary>
        public string userDb { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// DataBase Name
        /// </summary>
        public string dbName { get; set; }

        /// <summary>
        /// INSTANCE
        /// </summary>
        private static myConfig INSTANCE = null;

        /// <summary>
        /// Private Constructor
        /// </summary>
        private myConfig()
        {
        }

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <returns>
        /// myConfig Instance
        /// </returns>
        public static myConfig getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new myConfig();
            }

            return INSTANCE;
        }
    }
}
