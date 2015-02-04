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
            //Console.WriteLine("MSSQLSERVER");
            //cSmo.cSmo smo = cSmo.cSmo.getInstance(cSmo.cSmo.MSSQLSERVER, "C:/TEMP/SQL/MSSQLSERVER/", "localhost", "sa", "r00t$ql", "MCS");
            //smo.saveProcedures();
            //smo.saveFunctions();
            //smo.saveTables();

            Console.WriteLine("MYSQL");
            cSmo.cSmo smo = cSmo.cSmo.getInstance(cSmo.cSmo.MYSQL, "C:/TEMP/SQL/MYSQL/", "localhost", "root", "r00t$ql", "test");
            smo.saveProcedures();
            smo.saveFunctions();
            smo.saveTables();
        }
    }
}
