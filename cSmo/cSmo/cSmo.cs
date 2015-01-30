using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cSmo
{
    public class cSmo
    {
        public static void test()
        {
            SqlServerSmo sqlserver = new SqlServerSmo("C:/TEMP/SQL/", "localhost", "sa", "r00t$ql", "MAEAS");
            sqlserver.encryptProcedures();
            sqlserver.encryptFunctions();

            //sqlserver.saveProcedures();
            //sqlserver.saveFunctions();
            //sqlserver.saveTables();
        }
    }
}
