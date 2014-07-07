using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeDbUtil
{
    public class DBUtil
    {
        static public string GetDbConnStr(string dbName, string server = "bevmp3", string userName = "besadmin", string password = "ezas3.14")
        {
            return string.Format("Data Source={0};Initial Catalog={3};User ID={1};Password={2};", server, userName, password,dbName);

        }
        static public string GetDbConnStr(string server = "bevmp3", string userName = "besadmin", string password = "ezas3.14")
        {
            return string.Format("Data Source={0};User ID={1};Password={2};", server, userName, password);

        }
        static public DataRowCollection Execute(string connStr, string cmdStr)
        {

            var dt = new DataTable();
            using (var con = new SqlConnection(connStr))
            {
                var cmd = new SqlCommand(cmdStr, con);
                var da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dt);
            }
            return dt.Rows;
        }
        static public IEnumerable<string> GetDbNames(string server = "bevmp3", string userName = "besadmin", string password = "ezas3.14")
        {
            var dbNames = new List<string>();

            using (var con = new SqlConnection(GetDbConnStr(server,userName,password)))
            {
                con.Open();
                DataTable databases = con.GetSchema("Databases");
                foreach (DataRow database in databases.Rows)
                {
                    dbNames.Add(database.Field<String>("database_name"));

                    String databaseName = database.Field<String>("database_name");
                    short dbID = database.Field<short>("dbid");
                    DateTime creationDate = database.Field<DateTime>("create_date");
                }
            }

            return dbNames;
        }
    }
}
