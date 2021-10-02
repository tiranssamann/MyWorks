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
            string datasource = @"sql5053.site4now.net";

            string database = "DB_A6293D_tiransama";
            string username = "DB_A6293D_tiransama_admin";
            string password = "7779134aA";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }

}