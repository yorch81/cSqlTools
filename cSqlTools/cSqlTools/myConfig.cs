using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cSqlTools
{
    class myConfig
    {
        public String CurrentDir { get; set; }
        public int dbType { get; set; }
        public string path { get; set; }
        public string serverDb { get; set; }
        public string userDb { get; set; }
        public string password { get; set; }
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
