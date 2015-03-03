using System;
using System.IO;
using System.Data;

/**
 * myUtil 
 *
 * myUtil Static Class of Utils
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
    class myUtil
    {
        /// <summary>
        /// Gets Text of a File
        /// </summary>
        /// <param name="file">
        /// File Name (Full Path)
        /// </param>
        /// <returns>
        /// String
        /// </returns>
        public static String getTextForFile(string file)
        {
            string retValue = "";

            try
            {
                retValue = File.ReadAllText(file);
            }
            catch (Exception e)
            {
                retValue = null;
            }

            return retValue;
        }

        /// <summary>
        /// Gets a File Stream for a File
        /// </summary>
        /// <param name="file">
        /// File Name (Full Path)
        /// </param>
        /// <returns>
        /// FileStream
        /// </returns>
        public static FileStream getStreamForFile(string file)
        {
            FileStream stream;

            try
            {
                stream = new FileStream(file, FileMode.Open);
            }
            catch (Exception e)
            {
                stream = null;
            }

            return stream;
        }

        /// <summary>
        /// Gets HTML SELECT for DataBases
        /// </summary>
        /// <param name="dbType">
        /// Connection Type 1 MSSQLSERVER 2 MYSQL
        /// </param>
        /// <param name="serverDb">
        /// Server Name
        /// </param>
        /// <param name="userDb">
        /// DataBase User
        /// </param>
        /// <param name="password">
        /// User Password
        /// </param>
        /// <returns>
        /// String
        /// </returns>
        public static String getDbListHtml(int dbType, string serverDb, string userDb, string password)
        {
            string retValue = "";

            DataSet dsDb = cSmo.cSmo.getDataBases(dbType, serverDb, userDb, password);

            if (dsDb != null)
            {
                for (int i = 0; i < dsDb.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        myConfig cfg = myConfig.getInstance();
                        cfg.dbName = (string)dsDb.Tables[0].Rows[i]["DBSCHEMAS"];
                        retValue += "<option selected value='";
                    }
                    else
                        retValue += "<option value='";

                    retValue += dsDb.Tables[0].Rows[i]["DBSCHEMAS"];
                    retValue += "'>";
                    retValue += dsDb.Tables[0].Rows[i]["DBSCHEMAS"];
                    retValue += "</option>";
                }
            }

            return retValue;
        }
    }
}
