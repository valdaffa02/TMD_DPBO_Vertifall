using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertifall.Model
{
    public class TScore
    {
        private static MySqlConnection conn;
        private static MySqlCommand cmd;
        private static string constr = "server=127.0.0.1;uid=root;pwd=;database=db_keep_standing";

        public TScore()
        {

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = constr;
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void ReadUsername()
        {
            try
            {
                string queryString = "SELECT username FROM tscore;";
                using (conn = new MySqlConnection(constr))
                {
                    cmd = new MySqlCommand(queryString, conn);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static DataTable ReadAll()
        {
            try
            {
                string queryString = "SELECT * FROM tscore;";
                using (conn = new MySqlConnection(constr))
                {
                    cmd = new MySqlCommand(queryString, conn);
                    conn.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public static void InsertCScore(CScore cscore)
        {
            try
            {
                string queryString = "INSERT INTO tscore (username, score, standing) VALUES (?username, ?score, ?standing);";

                using (conn = new MySqlConnection(constr))
                {
                    cmd = new MySqlCommand(queryString, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("?username", cscore.Username);
                    cmd.Parameters.AddWithValue("?score", cscore.Score);
                    cmd.Parameters.AddWithValue("?standing", cscore.Standing);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Insert Error " + ex.Message);
            }
        }

        public static void UpdateCScore(CScore cscore)
        {
            try
            {
                string queryString = "UPDATE tscore SET score = ?score, standing = ?standing WHERE username = ?username;";

                using (conn = new MySqlConnection(constr))
                {
                    cmd = new MySqlCommand(queryString, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("?score", cscore.Score);
                    cmd.Parameters.AddWithValue("?standing", cscore.Standing);
                    cmd.Parameters.AddWithValue("?username", cscore.Username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error" + ex.Message);
            }
        }
    }
}
