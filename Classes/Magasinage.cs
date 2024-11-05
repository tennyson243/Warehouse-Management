using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Magasinage
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterEntreposage(int vehicule,int Colis, string nature, int quantite, int importateur, int statut)
        {
            string query = "Insert into Magasinage (Vehicule, Colis,  Nature, Quantite, Importateur, Reste) values (@Vehicule,@Colis, @Nature, @Quantite, @Importateur, @Reste)";

            SqlParameter[] parameter = new SqlParameter[6];

            parameter[0] = new SqlParameter("@Vehicule", SqlDbType.Int);
            parameter[0].Value = vehicule;

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = Colis;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[2] = new SqlParameter("@Nature", DBNull.Value);
                parameter[2].Value = nature;
            }
            else
            {
                parameter[2] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[2].Value = nature;
            }
            parameter[3] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[3].Value = quantite;

            parameter[4] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[4].Value = importateur;

            parameter[5] = new SqlParameter("@Reste", SqlDbType.Int);
            parameter[5].Value = statut;
            

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierEntreposage(int vehicule, int Colis, string nature, int quantite, int importateur)
        {
            string query = "Update Magasinage set Vehicule =@Vehicule, Colis = @Colis,  Nature = @Nature, Quantite=@Quantite, Importateur = @Importateur where Colis = @Colis";

            SqlParameter[] parameter = new SqlParameter[5];

            parameter[0] = new SqlParameter("@Vehicule", SqlDbType.Int);
            parameter[0].Value = vehicule;

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = Colis;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[2] = new SqlParameter("@Nature", DBNull.Value);
                parameter[2].Value = nature;
            }
            else
            {
                parameter[2] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[2].Value = nature;
            }
            parameter[3] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[3].Value = quantite;

            parameter[4] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[4].Value = importateur;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listdeEntreposage()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Magasinage", null);
            return table;
        }

        public Boolean EffaceEntreposage(int id)
        {
            string query = "Delete from Magasinage where Id_Magasinage = @Id_Magasinage";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_Magasinage", SqlDbType.Int);
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

        public DataTable TopImportateur(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "SELECT TOP 1 Importateur  FROM Colis where Id_Colis=@Id_Colis order by id_Colis DESC";
            parameters[0] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }
        public DataTable Topdernier()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("SELECT TOP 1 Id_Colis  FROM Colis order by id_Colis DESC", null);
            return table;
        }

        public DataTable TopdernierRetrait()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("SELECT TOP 1 Id_Retrait  FROM Retrait order by id_Retrait DESC", null);
            return table;
        }

        public Boolean ModifierStock(string Nature, int Quantite, int id)
        {
            string query = "Update Magasinage set  Reste = Reste-@Quantite where Colis = @Colis";
            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Nature", SqlDbType.VarChar);
            parameter[0].Value = Nature;

            parameter[1] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[1].Value = Quantite;

            parameter[2] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[2].Value = id;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean ModifierStockPlus(int M, int Val, int id)
        {
            string query = "Update Magasinage set  Reste = Reste+@Val where Colis = @Colis";
            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[0].Value = id;

            parameter[1] = new SqlParameter("@M", SqlDbType.Int);
            parameter[1].Value = M;

            parameter[2] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[2].Value = Val;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean ModifierStockMoins(int M, int Val, int id)
        {
            string query = "Update Magasinage set Reste = @M-@Val where Colis = @Colis";
            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[0].Value = id;

            parameter[1] = new SqlParameter("@M", SqlDbType.Int);
            parameter[1].Value = M;

            parameter[2] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[2].Value = Val;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable chercherMofifier(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "SELECT Quantite FROM Retrait  WHERE id_Retrait= @Id_Retrait";
            parameters[0] = new SqlParameter("@Id_Retrait", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }

        public DataTable QuantiteMagasinage(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "SELECT Quantite FROM Magasinage  WHERE Colis= @Colis";
            parameters[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }

        public DataTable ValeurQuantite(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "Select  isnull(Sum(Quantite),0) from Retrait where colis = @Colis";
            parameters[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }

        public DataTable DerniereQuantite()
        {
            string query;
            query = "select top 2 percent reste from Magasinage  order by  Id_Magasinage DESC";
            DataTable table = new DataTable();
            table = connexion.getdata(query,null);
            return table;

        }

        public DataTable FicheMagasin()
        {
            string query;
            query = "Select*from Vue_FicheMagasin";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable EntreeJournalier()
        {
            string query;
            query = "Select * from Circulation where Type = 'Entree' and Date = Cast(GETDATE() as date)";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable SortieJournalier()
        {
            string query;
            query = "Select * from Circulation where Type = 'Sortie' and Date = Cast(GETDATE() as date) and Reste=0";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable ResteColis()
        {
            string query;
            query = "Select * from Magasinage where  Reste<>0";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public Boolean ModifierStockPlusMagasin(int Quantite, int id, int Q)
        {
            string query = "Update Magasinage set Quantite = @Quantite,  Reste =@Quantite-@Val where Colis=@Colis";
            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[0].Value = Quantite;

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = id;

            parameter[2] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[2].Value = Q;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Boolean ModifierStockMoinsModifier(int Quantite, int id, int Q)
        {
            string query = "Update Magasinage set Quantite = @Quantite,  Reste =@Quantite-@Val where Colis=@Colis";
            SqlParameter[] parameter = new SqlParameter[3];

            parameter[0] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[0].Value = Quantite;

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = id;

            parameter[2] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[2].Value = Q;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable chercherMofifierMagasin(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "SELECT Quantite FROM Magasinage  WHERE Colis = @Colis";
            parameters[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }

        public DataTable chercherReste(int id)
        {
            string query;
            SqlParameter[] parameters = new SqlParameter[1];
            query = "SELECT TOP 1 Reste FROM Magasinage  WHERE Colis = @Colis order by Colis DESC";
            parameters[0] = new SqlParameter("@Colis", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable table = new DataTable();
            table = connexion.getdata(query, parameters);
            return table;

        }
    }
}
