using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DB
{
    public class DBConnectionManager
    {
       //private string DbSource = "임경찬\\GCLIM";// your DB address 
        private string DbSource = "DESKTOP-KV6KNHL\\CHAN";

        private string DbName = "AUCTION_DB";// your DB database name
        //private static string DbUser = "임경찬\\ADMIN";// your DB user name
        //private static string DbPassword = "";// your DB user password
        public  string connectionString = "";

        public DBConnectionManager()// 다른 컴퓨터에서 접근시 SQL인증으로 변경할 것.
        {
            connectionString = $"Data Source={DbSource};" +
                          $"Initial Catalog={DbName};" +
                          $"Integrated Security=True;";
        }
        public void Connect()
        {
            // 데이터베이스 연결
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // DB 서버 접속
                    connection.Open();
                }
                catch (Exception ex) // DB 서버 접속 실패 시
                {
                    Trace.WriteLine("Error connecting to database: " + ex.Message);
                }

                // DB 서버 접속 종료
                connection.Close();
            }


            Trace.WriteLine("DB connecting Test Complete!");
        }








    }
}
