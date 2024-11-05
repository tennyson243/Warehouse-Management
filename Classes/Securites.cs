using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Securites
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterSecurite(string plaque,  string type, DateTime entree, DateTime sortie, string statut, byte [] photo, string Chauffeur)
        {
            string query = "insert into Securite(Plaque, Type, Date_Entree, Date_Sortie, Statut, Photo,Chauffeur) values (@Plaque,@Type, @Date_Entree,@Date_Sortie, @Statut, @Photo, @Chauffeur)";

            SqlParameter[] parameter = new SqlParameter[7];


            if (string.IsNullOrEmpty(plaque))
            {
                parameter[0] = new SqlParameter("@Plaque", DBNull.Value);
                parameter[0].Value = plaque;
            }
            else
            {
                parameter[0] = new SqlParameter("@Plaque", SqlDbType.VarChar);
                parameter[0].Value = plaque;
            }

            if (string.IsNullOrEmpty(type))
            {
                parameter[1] = new SqlParameter("@Type", DBNull.Value);
                parameter[1].Value = type;
            }
            else
            {
                parameter[1] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameter[1].Value = type;
            }

            parameter[2] = new SqlParameter("@Date_Entree", SqlDbType.DateTime);
            parameter[2].Value = entree;

            parameter[3] = new SqlParameter("@Date_Sortie", SqlDbType.DateTime);
            parameter[3].Value = sortie;

            if (string.IsNullOrEmpty(type))
            {
                parameter[4] = new SqlParameter("@Statut", DBNull.Value);
                parameter[4].Value = statut;
            }
            else
            {
                parameter[4] = new SqlParameter("@Statut", SqlDbType.VarChar);
                parameter[4].Value = statut;
            }

            parameter[5] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[5].Value = photo;

            if (string.IsNullOrEmpty(Chauffeur))
            {
                parameter[6] = new SqlParameter("@Chauffeur", DBNull.Value);
                parameter[6].Value = Chauffeur;
            }
            else
            {
                parameter[6] = new SqlParameter("@Chauffeur", SqlDbType.VarChar);
                parameter[6].Value = Chauffeur;
            }

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierSecurite(int id,string plaque, string type, DateTime entree, DateTime sortie, string statut, byte[] photo, string Chauffeur)
        {
            string query = "Update Securite set Plaque = @Plaque, Type= @Type, Date_Entree = @Date_Entree, Date_Sortie = @Date_Sortie, Statut = @Statut, Photo = @Photo, Chauffeur=@Chauffeur where Id_Securite = @Id_Securite";

            SqlParameter[] parameter = new SqlParameter[8];

            parameter[6] = new SqlParameter("@Id_Securite", SqlDbType.Int);
            parameter[6].Value = id;
            if (string.IsNullOrEmpty(plaque))
            {
                parameter[0] = new SqlParameter("@Plaque", DBNull.Value);
                parameter[0].Value = plaque;
            }
            else
            {
                parameter[0] = new SqlParameter("@Plaque", SqlDbType.VarChar);
                parameter[0].Value = plaque;
            }

            if (string.IsNullOrEmpty(type))
            {
                parameter[1] = new SqlParameter("@Type", DBNull.Value);
                parameter[1].Value = type;
            }
            else
            {
                parameter[1] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameter[1].Value = type;
            }

            parameter[2] = new SqlParameter("@Date_Entree", SqlDbType.DateTime);
            parameter[2].Value = entree;

            parameter[3] = new SqlParameter("@Date_Sortie", SqlDbType.DateTime);
            parameter[3].Value = sortie;

            if (string.IsNullOrEmpty(statut))
            {
                parameter[4] = new SqlParameter("@Statut", DBNull.Value);
                parameter[4].Value = statut;
            }
            else
            {
                parameter[4] = new SqlParameter("@Statut", SqlDbType.VarChar);
                parameter[4].Value = statut;
            }

            parameter[5] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[5].Value = photo;

            if (string.IsNullOrEmpty(Chauffeur))
            {
                parameter[7] = new SqlParameter("@Chauffeur", DBNull.Value);
                parameter[7].Value = Chauffeur;
            }
            else
            {
                parameter[7] = new SqlParameter("@Chauffeur", SqlDbType.VarChar);
                parameter[7].Value = Chauffeur;
            }

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listdelasecurite()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select Id_Securite as Id, Plaque,Type, Date_Entree from Securite", null);
            return table;
        }

        public DataTable listsecurite()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select Id_Securite as Id, Plaque, Type, Date_Entree, Date_Sortie, Statut from Securite", null);
            return table;
        }

        public DataTable listsecuriteOk()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select Id_Securite as Id, Plaque, Type, Date_Entree, Date_Sortie, Statut from Securite Where Statut !='En Magasin'", null);
            return table;
        }

        public Boolean EffaceSecurite(int id)
        {
            string query = "Delete from securite where Id_Securite = @Id_securite";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_securite", SqlDbType.Int);
            parameter[0].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable getSecuriteparid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Securite where Id_Securite = @Id_Securite";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Securite", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
