using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shopping.Models
{
    public class DB
    {
        static SqlConnection con = new SqlConnection("data source=DESKTOP-USIKR4F ; database=EShopping ;integrated security = true");

        public static void Query(string query)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void Query(string query,SqlParameter[] prm)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddRange(prm);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static DataTable Table(string query)
        {
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public static DataTable Table(string query,SqlParameter[] prm)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddRange(prm);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public static DataTable sp_Table(string spName, SqlParameter[] prm)
        {
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.Parameters.AddRange(prm);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public static void sp_Query(string spName, SqlParameter[] prm)
        {
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.Parameters.AddRange(prm);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
