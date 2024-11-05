using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Controle
    {
        BDD.Connecteur connexion = new BDD.Connecteur();
        public Boolean AjouterControle(string MotdePasse)
        {
            string query = "Insert into Controle (Mot_de_Passe) values (@Mot_de_Passe)";
            SqlParameter[] parameters = new SqlParameter[1];

            if (string.IsNullOrEmpty(MotdePasse))
            {
                parameters[0] = new SqlParameter("@Mot_de_Passe", DBNull.Value);
                parameters[0].Value = MotdePasse;
            }
            else
            {
                parameters[0] = new SqlParameter("@Mot_de_Passe", SqlDbType.VarChar);
                parameters[0].Value = MotdePasse;
            }


            if (connexion.setdata(query, parameters) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public Boolean ModifierControle(int id, string MotdePasse)
        {
            string query = "Update Controle set Mot_de_Passe = @Mot_de_Passe where Id = @Id";
            SqlParameter[] parameters = new SqlParameter[2];

            parameters[1] = new SqlParameter("@Id", SqlDbType.Int);
            parameters[1].Value = id;

            if (string.IsNullOrEmpty(MotdePasse))
            {
                parameters[0] = new SqlParameter("@Mot_de_Passe", DBNull.Value);
                parameters[0].Value = MotdePasse;
            }
            else
            {
                parameters[0] = new SqlParameter("@Mot_de_Passe", SqlDbType.VarChar);
                parameters[0].Value = MotdePasse;
            }



            if (connexion.setdata(query, parameters) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listControle()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select*from Controle", null);
            return table;
        }



        public Boolean SupprimerControle(int id)
        {
            string query = "Delete from Controle where id = @id";
            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;


            if (connexion.setdata(query, parameters) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetControlebyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Controle where Id = @Id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
