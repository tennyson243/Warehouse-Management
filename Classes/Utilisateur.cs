using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{           

    class Utilisateur
    {
        BDD.Connecteur connexion = new BDD.Connecteur();
        public Boolean AjouterUser(string nm, string postnom, string NomUtilisateur, string MotdePasse)
        {
            string query = "Insert into Utilisateur (Nom, Post_Nom, Nom_Utilisateur, Mot_de_Passe) values (@Nom, @Post_Nom, @Nom_Utilisateur, @Mot_de_Passe)";
            SqlParameter[] parameters = new SqlParameter[4];

            if (string.IsNullOrEmpty(nm))
            {
                parameters[0] = new SqlParameter("@Nom", DBNull.Value);
                parameters[0].Value = nm;
            }
            else
            {
                parameters[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
                parameters[0].Value = nm;
            }
            if (string.IsNullOrEmpty(postnom))
            {
                parameters[1] = new SqlParameter("@Post_Nom", DBNull.Value);
                parameters[1].Value = postnom;
            }
            else
            {
                parameters[1] = new SqlParameter("@Post_Nom", SqlDbType.VarChar);
                parameters[1].Value = postnom;
            }
            if (string.IsNullOrEmpty(NomUtilisateur))
            {
                parameters[2] = new SqlParameter("@Nom_Utilisateur", DBNull.Value);
                parameters[2].Value = NomUtilisateur;
            }
            else
            {
                parameters[2] = new SqlParameter("@Nom_Utilisateur", SqlDbType.VarChar);
                parameters[2].Value = NomUtilisateur;
            }
            if (string.IsNullOrEmpty(MotdePasse))
            {
                parameters[3] = new SqlParameter("@Mot_de_Passe", DBNull.Value);
                parameters[3].Value = MotdePasse;
            }
            else
            {
                parameters[3] = new SqlParameter("@Mot_de_Passe", SqlDbType.VarChar);
                parameters[3].Value = MotdePasse;
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

        public Boolean ModifierUtilisateur(int id, string nm, string postnom, string NomUtilisateur, string MotdePasse)
        {
            string query = "Update Utilisateur set Nom = @Nom, Post_Nom = @Post_Nom, Nom_Utilisateur = @Nom_Utilisateur, Mot_de_Passe = @Mot_de_Passe where Id = @Id";
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[4] = new SqlParameter("@Id", SqlDbType.Int);
            parameters[4].Value = id;

            if (string.IsNullOrEmpty(nm))
            {
                parameters[0] = new SqlParameter("@Nom", DBNull.Value);
                parameters[0].Value = nm;
            }
            else
            {
                parameters[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
                parameters[0].Value = nm;
            }
            if (string.IsNullOrEmpty(postnom))
            {
                parameters[1] = new SqlParameter("@Post_Nom", DBNull.Value);
                parameters[1].Value = postnom;
            }
            else
            {
                parameters[1] = new SqlParameter("@Post_Nom", SqlDbType.VarChar);
                parameters[1].Value = postnom;
            }
            if (string.IsNullOrEmpty(NomUtilisateur))
            {
                parameters[2] = new SqlParameter("@Nom_Utilisateur", DBNull.Value);
                parameters[2].Value = NomUtilisateur;
            }
            else
            {
                parameters[2] = new SqlParameter("@Nom_Utilisateur", SqlDbType.VarChar);
                parameters[2].Value = NomUtilisateur;
            }
            if (string.IsNullOrEmpty(MotdePasse))
            {
                parameters[3] = new SqlParameter("@Mot_de_Passe", DBNull.Value);
                parameters[3].Value = MotdePasse;
            }
            else
            {
                parameters[3] = new SqlParameter("@Mot_de_Passe", SqlDbType.VarChar);
                parameters[3].Value = MotdePasse;
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

        public DataTable listUtilisateur()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select*from Utilisateur", null);
            return table;
        }



        public Boolean SupprimerUtilisateur(int id)
        {
            string query = "Delete from Utilisateur where id = @id";
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

        public DataTable GetUtilisateurbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Utilisateur where Id = @Id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

    }
}
