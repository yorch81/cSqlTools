using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Helpers;
using System.Threading.Tasks;
using System.IO;
using Nancy.Responses;

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

            Post["/connect"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                string serverDb = Request.Form.serverDb;
                string userDb = Request.Form.userDb;
                string password = Request.Form.password;
                string dbName = Request.Form.dbName;

                cfg.serverDb = serverDb;
                cfg.userDb = userDb;
                cfg.password = password;
                cfg.dbName = dbName;

                cSmo.cSmo smo = cSmo.cSmo.getInstance(cSmo.cSmo.MSSQLSERVER, cfg.path, serverDb, userDb, password, dbName);

                if (smo.isConnected())
                    return "Ok";
                else
                    return "Bad";
            };

            Post["/save"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                string dir = Request.Form.dir;

                cSmo.cSmo smo = cSmo.cSmo.getInstance(cSmo.cSmo.MSSQLSERVER, cfg.path, cfg.serverDb, cfg.userDb, cfg.password, cfg.dbName);

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

                cSmo.cSmo smo = cSmo.cSmo.getInstance(cSmo.cSmo.MSSQLSERVER, cfg.path, cfg.serverDb, cfg.userDb, cfg.password, cfg.dbName);

                if (smo.isConnected())
                {
                    smo.encryptProcedures();
                    smo.encryptFunctions();

                    return "Ok";
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
