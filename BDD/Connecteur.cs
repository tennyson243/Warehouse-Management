using Cqrs.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.BDD
{
    class Connecteur
    {
        private SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");

        public void Openconnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeconnection()
        {
            if(con.State==System.Data.ConnectionState.Open)
            {

                con.Close();
            }
        }

        public SqlConnection getconnexion()
        {
            return con;
        }

        public DataTable getdata(string query, SqlParameter [] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, con);

            if(parameters!=null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            return table;

        }

        public int setdata(string query, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);

            }

            Openconnection();

            
            int commandsatate = cmd.ExecuteNonQuery();

            closeconnection();
            return commandsatate;
        }
        //static QueryFactory db = null;
        //public static QueryFactory Db()
        //{
        //    if(db==null)
        //    {
        //        var compiler = new sqlcompiler();
        //        db = new QueryFactory(con(), compiler)
        //    }
        //}
    }
}
