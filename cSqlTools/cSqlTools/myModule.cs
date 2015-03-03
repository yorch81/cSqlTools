using System;
using Nancy;
using Nancy.Helpers;

/**
 * myModule 
 *
 * myModule Web Application Module
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
    /// Web Application Routes
    /// </summary>
    public class myModule : NancyModule
    {
        /// <summary>
        /// Constructor of Class
        /// </summary>
        public myModule()
        {
            Get["/"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();
                
                try
                {
                    return View["static/tree.html", cfg];
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            };

            Post["/dblist"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                int dbType = Request.Form.dbType;
                string serverDb = Request.Form.serverDb;
                string userDb = Request.Form.userDb;
                string password = Request.Form.password;

                cfg.dbType = dbType;
                cfg.serverDb = serverDb;
                cfg.userDb = userDb;
                cfg.password = password;

                return myUtil.getDbListHtml(dbType, serverDb, userDb, password);
            };

            Post["/save"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                int dbType = Request.Form.dbType;
                string dir = Request.Form.dir;
                string dbName = Request.Form.dbName;

                cfg.dbName = dbName;

                cSmo.cSmo smo = cSmo.cSmo.getInstance(dbType, cfg.path, cfg.serverDb, cfg.userDb, cfg.password, cfg.dbName);

                if (smo.isConnected())
                {
                    smo.saveTables(dir);
                    smo.saveProcedures(dir);
                    smo.saveFunctions(dir);

                    return "Ok";
                }
                else
                    return "Bad";
            };

            Post["/encrypt"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                int dbType = Request.Form.dbType;
                string dbName = Request.Form.dbName;

                cfg.dbName = dbName;

                if (dbType == cSmo.cSmo.MSSQLSERVER)
                {
                    cSmo.cSmo smo = cSmo.cSmo.getInstance(dbType, cfg.path, cfg.serverDb, cfg.userDb, cfg.password, cfg.dbName);

                    if (smo.isConnected())
                    {
                        smo.encryptProcedures();
                        smo.encryptFunctions();

                        return "Ok";
                    }
                    else
                        return "Bad";
                }
                else
                    return "Bad";
            };

            Post["/getfiles"] = parameters =>
            {
                string dir = Request.Form.dir;

                myConfig cfg = myConfig.getInstance();

                if (dir.Equals("./"))
                    dir = cfg.path;

                dir = HttpUtility.UrlDecode(dir);

                string strDir = "";

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dir);
                strDir = strDir + "<ul class=\"jqueryFileTree\" style=\"display: none;\">\n";

                foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
                    strDir = strDir + "\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n";

                foreach (System.IO.FileInfo fi in di.GetFiles())
                {
                    string ext = "";
                    if (fi.Extension.Length > 1)
                        ext = fi.Extension.Substring(1).ToLower();

                    strDir = strDir + "\t<li class=\"file ext_" + ext + "\"><a href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name + "</a></li>\n";
                }

                strDir = strDir + "</ul>";

                return strDir;
            };
        }

    }
}
