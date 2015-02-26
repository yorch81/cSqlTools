using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace cSqlTools
{
    class myUtil
    {
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
    }
}
