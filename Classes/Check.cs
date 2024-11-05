using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Check
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterCheck(string NumBon, string Designation, string Nature, int Quantite, string Validteur, DateTime Retrait, DateTime Sortie, byte [] Controle)
        {
            string query = "Insert into CheckSecurite (Num_Bon, Designation, Nature, Quantite, Validation, Date_Retrait, Date_Sortie, Controle) values (@Num_Bon, @Designation, @Nature, @Quantite, @Validation, @Date_Retrait, @Date_Sortie, @Controle)";

            SqlParameter[] parameter = new SqlParameter[8];

            if (string.IsNullOrEmpty(NumBon))
            {
                parameter[0] = new SqlParameter("@Num_Bon", DBNull.Value);
                parameter[0].Value = NumBon;
            }
            else
            {
                parameter[0] = new SqlParameter("@Num_Bon", SqlDbType.VarChar);
                parameter[0].Value = NumBon;
            }

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[1] = new SqlParameter("@Designation", DBNull.Value);
                parameter[1].Value = Designation;
            }
            else
            {
                parameter[1] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[1].Value = Designation;
            }

            if (string.IsNullOrEmpty(Nature))
            {
                parameter[2] = new SqlParameter("@Nature", DBNull.Value);
                parameter[2].Value = Nature;
            }
            else
            {
                parameter[2] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[2].Value = Nature;
            }
            parameter[3] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[3].Value = Quantite;

            if (string.IsNullOrEmpty(Validteur))
            {
                parameter[4] = new SqlParameter("@Validation", DBNull.Value);
                parameter[4].Value = Validteur;
            }
            else
            {
                parameter[4] = new SqlParameter("@Validation", SqlDbType.VarChar);
                parameter[4].Value = Validteur;
            }

            parameter[5] = new SqlParameter("@Date_Retrait", SqlDbType.Date);
            parameter[5].Value = Retrait;

            parameter[6] = new SqlParameter("@Date_Sortie", SqlDbType.Date);
            parameter[6].Value = Sortie;

            parameter[7] = new SqlParameter("@Controle", SqlDbType.VarBinary);
            parameter[7].Value = Controle;

           

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierCheck(string Designation, string Check, DateTime Sortie, byte[] Controle)
        {
            string query = "Update CheckSecurite set Validation=@Validation, Date_Sortie = @Date_Sortie, Controle = @Controle  Where Designation = @Designation";

            SqlParameter[] parameter = new SqlParameter[4];

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[1] = new SqlParameter("@Designation", DBNull.Value);
                parameter[1].Value = Designation;
            }
            else
            {
                parameter[1] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[1].Value = Designation;
            }
            if (string.IsNullOrEmpty(Check))
            {
                parameter[0] = new SqlParameter("@Validation", DBNull.Value);
                parameter[0].Value = Check;
            }
            else
            {
                parameter[0] = new SqlParameter("@Validation", SqlDbType.VarChar);
                parameter[0].Value = Check;
            }
            parameter[2] = new SqlParameter("@Date_Sortie", SqlDbType.DateTime);
            parameter[2].Value = Sortie;

            parameter[3] = new SqlParameter("@Controle", SqlDbType.VarBinary);
            parameter[3].Value = Controle;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listCheck()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from CheckSecurite", null);
            return table;
        }

        public DataTable GetdataRetrait(string Designation)
        {
            DataTable table = new DataTable();
            string query = "select Designation, Nature, Quantite from Retrait where Designation=@Designation";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
            parameters[0].Value = Designation;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetdataCheckSecurite(string Designation)
        {
            DataTable table = new DataTable();
            string query = "select Designation, Nature, Quantite from CheckSecurite where Designation=@Designation";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
            parameters[0].Value = Designation;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable GetdataDesignation(string Designation)
        {
            DataTable table = new DataTable();
            string query = "select * from CheckSecurite where Designation=@Designation and Validation ='Confirmer'";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
            parameters[0].Value = Designation;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}
