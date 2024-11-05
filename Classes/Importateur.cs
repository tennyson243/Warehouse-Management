using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Importateur
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterImportateur(string nom, string adresse, string pays, string ville, string Telephone, byte [] photo)
        {
            string query = "insert into Importateur (Nom, Adresse, Pays, Ville, Telephone, Photo) values (@Nom, @Adresse, @Pays, @Ville, @Telephone, @Photo)";

            SqlParameter[] parameter = new SqlParameter[6];

            if (string.IsNullOrEmpty(nom))
            {
                parameter[0] = new SqlParameter("@Nom", DBNull.Value);
                parameter[0].Value = nom;
            }
            else
            {
                parameter[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
                parameter[0].Value = nom;
            }

            if (string.IsNullOrEmpty(adresse))
            {
                parameter[1] = new SqlParameter("@Adresse", DBNull.Value);
                parameter[1].Value = adresse;
            }
            else
            {
                parameter[1] = new SqlParameter("@Adresse", SqlDbType.VarChar);
                parameter[1].Value = adresse;
            }

            if (string.IsNullOrEmpty(pays))
            {
                parameter[2] = new SqlParameter("@Pays", DBNull.Value);
                parameter[2].Value = pays;
            }
            else
            {
                parameter[2] = new SqlParameter("@Pays", SqlDbType.VarChar);
                parameter[2].Value = pays;
            }

            if (string.IsNullOrEmpty(ville))
            {
                parameter[3] = new SqlParameter("@Ville", DBNull.Value);
                parameter[3].Value = ville;
            }
            else
            {
                parameter[3] = new SqlParameter("@Ville", SqlDbType.VarChar);
                parameter[3].Value = ville;
            }

            if (string.IsNullOrEmpty(Telephone))
            {
                parameter[4] = new SqlParameter("@Telephone", DBNull.Value);
                parameter[4].Value = Telephone;
            }
            else
            {
                parameter[4] = new SqlParameter("@Telephone", SqlDbType.VarChar);
                parameter[4].Value = Telephone;
            }

            parameter[5] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[5].Value = photo;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierImportateur(int id, string nom, string adresse, string pays, string ville, string Telephone,  byte[] photo)
        {
            string query = "update Importateur set Nom = @nom, Adresse = @Adresse, Pays = @Pays, Ville = @Ville, Telephone = @Telephone, Photo = @Photo where id_Importateur = @Id_Importateur ";

            SqlParameter[] parameter = new SqlParameter[7];

            parameter[6] = new SqlParameter("@Id_Importateur", SqlDbType.Int);
            parameter[6].Value = id;


            if (string.IsNullOrEmpty(nom))
            {
                parameter[0] = new SqlParameter("@Nom", DBNull.Value);
                parameter[0].Value = nom;
            }
            else
            {
                parameter[0] = new SqlParameter("@Nom", SqlDbType.VarChar);
                parameter[0].Value = nom;
            }

            if (string.IsNullOrEmpty(adresse))
            {
                parameter[1] = new SqlParameter("@Adresse", DBNull.Value);
                parameter[1].Value = adresse;
            }
            else
            {
                parameter[1] = new SqlParameter("@Adresse", SqlDbType.VarChar);
                parameter[1].Value = adresse;
            }

            if (string.IsNullOrEmpty(pays))
            {
                parameter[2] = new SqlParameter("@Pays", DBNull.Value);
                parameter[2].Value = pays;
            }
            else
            {
                parameter[2] = new SqlParameter("@Pays", SqlDbType.VarChar);
                parameter[2].Value = pays;
            }

            if (string.IsNullOrEmpty(ville))
            {
                parameter[3] = new SqlParameter("@Ville", DBNull.Value);
                parameter[3].Value = ville;
            }
            else
            {
                parameter[3] = new SqlParameter("@Ville", SqlDbType.VarChar);
                parameter[3].Value = ville;
            }

            if (string.IsNullOrEmpty(Telephone))
            {
                parameter[4] = new SqlParameter("@Telephone", DBNull.Value);
                parameter[4].Value = Telephone;
            }
            else
            {
                parameter[4] = new SqlParameter("@Telephone", SqlDbType.VarChar);
                parameter[4].Value = Telephone;
            }

            parameter[5] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[5].Value = photo;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listImportateur()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Importateur as ID, Nom, Adresse, Telephone from Importateur ", null);
            return table;
        }

        public DataTable listImportateurCool()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Importateur as ID, Nom, Adresse, Telephone, Photo from Importateur ", null);
            return table;
        }

        public Boolean EffaceImportateur(int id)
        {
            string query = "Delete from Importateur where Id_importateur = @Id_importateur";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_importateur", SqlDbType.Int);
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

        public DataTable getImportateurbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Importateur where Id_Importateur = @Id_Importateur";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Importateur", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
