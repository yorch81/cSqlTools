using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace cSqlTools
{
    /// <summary>
    /// Init Class
    /// </summary>
    class App
    {
        /// <summary>
        /// Init Method
        /// </summary>
        /// <param name="args">
        /// web For Init Web Application
        /// </param>
        static void Main(string[] args)
        {
            int dbType = 0;
            string path = "";
            string serverDb = "";
            string userDb = "";
            string password = "";
            string dbName = "";

            Console.WriteLine("Welcome to cqlTools on Console !!!");

            Console.Write("Enter DataBase Type (1 SQL Server 2 MySQL): ");
            dbType = int.Parse(Console.ReadLine());

            Console.Write("Enter Path to save Scripts: ");
            path = Console.ReadLine();

            if (args.Length != 0)
            {
                Console.Write("Enter Server DataBase Name: ");
                serverDb = Console.ReadLine();

                if (dbType == 1)
                    Console.Write("Enter SQL Server User: ");
                else
                    Console.Write("Enter MySQL User: ");

                userDb = Console.ReadLine();

                Console.Write("Enter Password: ");
                password = Console.ReadLine();

                Console.Write("Enter DataBase Name: ");
                dbName = Console.ReadLine();

                cSmo.cSmo smo;

                if (dbType == 1)
                    smo = cSmo.cSmo.getInstance(cSmo.cSmo.MSSQLSERVER, path, serverDb, userDb, password, dbName);
                else
                    smo = cSmo.cSmo.getInstance(cSmo.cSmo.MYSQL, path, serverDb, userDb, password, dbName);

                if (smo.isConnected())
                {
                    Console.Write("Enter Action (1 Save DDL Scripts 2 Encrypt Routines): ");
                    int action = int.Parse(Console.ReadLine());

                    if (action == 1)
                    {
                        if ((Directory.Exists(path)))
                        {
                            Console.WriteLine("Saving DDL Scripts on: " + path);
                            smo.saveTables();
                            smo.saveProcedures();
                            smo.saveFunctions();
                        }
                        else
                            Console.WriteLine("The Path not Exists");
                    }
                    else
                    {
                        Console.WriteLine("Encrypting Routines of SQL Server (MySQL not Support)");
                        smo.encryptProcedures();
                        smo.encryptFunctions();
                    }
                }
                else
                    Console.WriteLine("Could not connect to DataBase Server");
            }
            else
            {
                if ((Directory.Exists(path)))
                {
                    myConfig cfg = myConfig.getInstance();
                    cfg.CurrentDir = Environment.CurrentDirectory.Replace("\\", "/");
                    cfg.path = path;
                    cfg.dbType = dbType;

                    new myHost(1080);
                }
                else
                    Console.WriteLine("Path doesn't Exists.");
            }

            Console.WriteLine("Press any key to Exit");
            Console.ReadKey();
        }
    }
}
