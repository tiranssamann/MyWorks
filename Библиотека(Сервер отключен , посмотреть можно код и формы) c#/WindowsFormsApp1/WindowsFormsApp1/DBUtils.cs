using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tutorial.SqlConn
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"sql5052.site4now.net";

            string database = "DB_A66CD9_itstep";
            string username = "DB_A66CD9_itstep_admin";
            string password = "7779134ara";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }

}