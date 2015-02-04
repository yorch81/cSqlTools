using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cSqlTools
{
    class App
    {
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

            int action = 0;

            Console.Write("Enter Action (1 Save DDL Scripts 2 Encrypt Routines): ");
            action = int.Parse(Console.ReadLine());

            if (action == 1)
            {
                smo.saveTables();
                smo.saveProcedures();
                smo.saveFunctions();
            }
            else 
            {
                smo.encryptProcedures();
                smo.encryptFunctions();
            }
        }
    }
}
