using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Declarant
    {

        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterDeclarant(string nom, string adresse, string pays, string ville, string Telephone, byte[] photo)
        {
            string query = "insert into Declarant (Nom, Adresse, Pays, Ville, Telephone, Photo) values (@Nom, @Adresse, @Pays, @Ville, @Telephone, @Photo)";

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

        public Boolean ModifierDeclarant(int id, string nom, string adresse, string pays, string ville, string Telephone, byte[] photo)
        {
            string query = "update Declarant set Nom = @nom, Adresse = @Adresse, Pays = @Pays, Ville = @Ville, Telephone = @Telephone, Photo = @Photo where id_Declarant = @Id_Declarant ";

            SqlParameter[] parameter = new SqlParameter[7];

            parameter[6] = new SqlParameter("@Id_Declarant", SqlDbType.Int);
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

        public DataTable listDeclarant()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Declarant as ID, Nom, Adresse, Telephone from Declarant", null);
            return table;
        }

        public DataTable listDeclarantCool()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Declarant as ID, Nom, Adresse, Telephone, Photo from Declarant", null);
            return table;
        }

        public DataTable list()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Declarant", null);
            return table;
        }

        public Boolean EffaceDeclarant(int id)
        {
            string query = "Delete from Declarant where Id_Declarant = @Id_Declarant";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_Declarant", SqlDbType.Int);
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

        public DataTable getDeclarantbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Declarant where Id_Declarant = @Id_Declarant";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Declarant", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
