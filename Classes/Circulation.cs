using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Circulation
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterCirculation(string Type, int declarant, int importateur, int plaque, DateTime date, int Reste, int Colis, int retrais)
        {
            string query = "Insert into Circulation (Type, Declarant, Importateur, Plaque, Date, Reste, Colis, Retrais) values (@Type, @Declarant, @Importateur, @Plaque, @Date, @Reste, @Colis, @Retrais)";

            SqlParameter[] parameter = new SqlParameter[8];

            if (string.IsNullOrEmpty(Type))
            {
                parameter[0] = new SqlParameter("@Type", DBNull.Value);
                parameter[0].Value = Type;
            }
            else
            {
                parameter[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameter[0].Value = Type;
            }
            parameter[1] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[1].Value = declarant;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            parameter[3] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[3].Value = plaque;

            parameter[4] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[4].Value = date;

            parameter[5] = new SqlParameter("@Reste", SqlDbType.Int);
            parameter[5].Value = Reste;

            parameter[6] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[6].Value = Colis;

            parameter[7] = new SqlParameter("@Retrais", SqlDbType.Int);
            parameter[7].Value = retrais;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierCirculation(int id, string Type, int declarant, int importateur, int plaque, DateTime date)
        {
            string query = "Update Circulation set Circulation=@Circulation, Type = @Type, Declarant = @Declarant, Importateur = @Importateur, Plaque = @Plaque, Date = @Date Where Id_Circulation = @Id_Circulation";

            SqlParameter[] parameter = new SqlParameter[6];

            parameter[5] = new SqlParameter("@Id_Circulation", SqlDbType.Int);
            parameter[5].Value = id;

            if (string.IsNullOrEmpty(Type))
            {
                parameter[0] = new SqlParameter("@Type", DBNull.Value);
                parameter[0].Value = Type;
            }
            else
            {
                parameter[0] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameter[0].Value = Type;
            }
            parameter[1] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[1].Value = declarant;

            parameter[2] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[2].Value = importateur;

            parameter[3] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[3].Value = plaque;

            parameter[4] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[4].Value = date;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listdecirculation()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Circulation", null);
            return table;
        }

        public Boolean EffaceCirculation(int id)
        {
            string query = "Delete from Circulation where Id_Circulation = @Id_Circulation";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_Ciculation", SqlDbType.Int);
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
    }
}
