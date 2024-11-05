using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Retrait
    {
        BDD.Connecteur connexion = new BDD.Connecteur();

        public Boolean AjouterRetrait(string Designation,int Colis, int plaqueentre, string nature, int Quantite, int Declarant, string plaqueesortie, string nomchaufeur, string numchauffeur, DateTime date, byte [] Qrcode)
        {
            string query = "insert into Retrait (Designation, Colis, Entree,  Nature, Quantite, Declarant, Sortie, Nom_Chauffeur, Num_Chauffeur, Date, QRCODE) values (@Designation,@Colis, @Entree,@Nature, @Quantite, @Declarant, @Sortie, @Nom_Chauffeur, @Num_Chauffeur, @Date, @QRCODE)";

            SqlParameter[] parameter = new SqlParameter[11];

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[0] = new SqlParameter("@Designation", DBNull.Value);
                parameter[0].Value = Designation;
            }
            else
            {
                parameter[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[0].Value = Designation;
            }

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = Colis;

            parameter[2] = new SqlParameter("@Entree", SqlDbType.Int);
            parameter[2].Value = plaqueentre;


            if (string.IsNullOrEmpty(nature))
            {
                parameter[3] = new SqlParameter("@Nature", DBNull.Value);
                parameter[3].Value = nature;
            }
            else
            {
                parameter[3] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[3].Value = nature;
            }
            parameter[4] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[4].Value = Quantite;

            parameter[5] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[5].Value = Declarant;


            if (string.IsNullOrEmpty(plaqueesortie))
            {
                parameter[6] = new SqlParameter("@Sortie", DBNull.Value);
                parameter[6].Value = plaqueesortie;
            }
            else
            {
                parameter[6] = new SqlParameter("@Sortie", SqlDbType.VarChar);
                parameter[6].Value = plaqueesortie;
            }


            if (string.IsNullOrEmpty(nomchaufeur))
            {
                parameter[7] = new SqlParameter("@Nom_Chauffeur", DBNull.Value);
                parameter[7].Value = nomchaufeur;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nom_Chauffeur", SqlDbType.VarChar);
                parameter[7].Value = nomchaufeur;
            }

            if (string.IsNullOrEmpty(numchauffeur))
            {
                parameter[8] = new SqlParameter("@Num_Chauffeur", DBNull.Value);
                parameter[8].Value = numchauffeur;
            }
            else
            {
                parameter[8] = new SqlParameter("@Num_Chauffeur", SqlDbType.VarChar);
                parameter[8].Value = numchauffeur;
            }
            parameter[9] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[9].Value = date;

            parameter[10] = new SqlParameter("@QRCODE", SqlDbType.Binary);
            parameter[10].Value = Qrcode;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierRetrait(int id, string Designation,  string nature, int Quantite, string plaqueesortie, string nomchaufeur, string numchauffeur, DateTime date)
        {
            string query = "Update Retrait set Designation=@Designation, Nature=@Nature, Quantite=@Quantite, Sortie=@Sortie, Nom_Chauffeur = @Nom_Chauffeur, Num_Chauffeur = @Num_Chauffeur, Date = @Date where Id_Retrait = @Id_Retrait ";

            SqlParameter[] parameter = new SqlParameter[8];

            parameter[7] = new SqlParameter("@Id_Retrait", SqlDbType.Int);
            parameter[7].Value = id;

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[0] = new SqlParameter("@Designation", DBNull.Value);
                parameter[0].Value = Designation;
            }
            else
            {
                parameter[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[0].Value = Designation;
            }
       


            if (string.IsNullOrEmpty(nature))
            {
                parameter[1] = new SqlParameter("@Nature", DBNull.Value);
                parameter[1].Value = nature;
            }
            else
            {
                parameter[1] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[1].Value = nature;
            }
            parameter[2] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[2].Value = Quantite;



            if (string.IsNullOrEmpty(plaqueesortie))
            {
                parameter[3] = new SqlParameter("@Sortie", DBNull.Value);
                parameter[3].Value = plaqueesortie;
            }
            else
            {
                parameter[3] = new SqlParameter("@Sortie", SqlDbType.VarChar);
                parameter[3].Value = plaqueesortie;
            }


            if (string.IsNullOrEmpty(nomchaufeur))
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", DBNull.Value);
                parameter[4].Value = nomchaufeur;
            }
            else
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", SqlDbType.VarChar);
                parameter[4].Value = nomchaufeur;
            }

            if (string.IsNullOrEmpty(numchauffeur))
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", DBNull.Value);
                parameter[5].Value = numchauffeur;
            }
            else
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", SqlDbType.VarChar);
                parameter[5].Value = numchauffeur;
            }
            parameter[6] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[6].Value = date;

            if (connexion.setdata(query, parameter) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable listRetrait()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Retrait", null);
            return table;
        }

        public DataTable listRetraitJointureModifier()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Retrait as ID, Retrait.Nature as Nature, Retrait.Quantite, Retrait.Sortie as P_Sortie, Nom_Chauffeur as Chauf, Num_Chauffeur as Num_Chau, Retrait.Date as Date from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant ", null);
            return table;
        }

        public DataTable TopdernierRetrais()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("SELECT TOP 1 Id_Retrait  FROM Retrait order by Id_Retrait DESC", null);
            return table;
        }

        public Boolean EffaceRetrait(int id)
        {
            string query = "Delete from Retrait where Id_Retrait = @Id_Retrait";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("Id_Retrait", SqlDbType.Int);
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

        public DataTable LireLesitems()
        {
            DataTable table = new DataTable();
            string query = "Select id_Colis, Quantite, Nature, Colis.Photo as Photo from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable list()
        {
            DataTable table = new DataTable();
            string query = "Select id_Retrait, Designation, Securite.Plaque, Retrait.Nature, Retrait.Quantite, Declarant.Nom, Retrait.Sortie,  Nom_Chauffeur, Num_Chauffeur, Retrait.Date from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant ";
            table = connexion.getdata(query, null);
            return table;
        }

        public DataTable getItemsbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select Declarant.Nom as Declarant, Nature, Colis.Photo as Photo from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant where Id_Colis= @Id_Colis";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable ListByeID(int id)
        {
            DataTable table = new DataTable();
            string query = "Select id_Retrait, Retrait.Colis, Designation, Securite.Plaque, Retrait.Nature, Retrait.Quantite, Declarant.Nom, Retrait.Sortie,  Nom_Chauffeur, Num_Chauffeur, Retrait.Date, Magasinage.Id_Magasinage, QRCODE  from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant inner join Magasinage on Magasinage.Colis=Retrait.Colis where id_Retrait= @id_Retrait";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Retrait", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
        public DataTable Getdata(int id)
        {
            DataTable table = new DataTable();
            string query = "Select id_Colis,Securite.Plaque,Importateur.Nom,  Declarant.Nom as Declarant, Colis.Manifeste, Colis.Plaque, Colis.Declarant  from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant where Id_Colis= @Id_Colis";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable EffetPersonnelle(string Designation)
        {
            DataTable table = new DataTable();
            string query = "Select id_Retrait, Retrait.Designation,Colis.Designation, Securite.Plaque, Retrait.Nature, Retrait.Quantite, Declarant.Nom as Declarant, Retrait.Sortie,  Nom_Chauffeur, Num_Chauffeur, Convert(varchar(25), Retrait.Date,103) from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant inner join Colis on Colis.Id_Colis = Retrait.Colis where Retrait.Designation= @Designation";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
            parameters[0].Value = Designation;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public DataTable EffetPersonnelleTable(string Designation)
        {
            DataTable table = new DataTable();
            string query = "Select Colis.Designation as Colis, Retrait.Nature, Retrait.Quantite from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant inner join Colis on Colis.Id_Colis = Retrait.Colis where Retrait.Designation= @Designation";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
            parameters[0].Value = Designation;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public Boolean AjouterRetraitParTransaction(string Designation, int Colis, int plaqueentre, string nature, int Quantite, int Declarant, string plaqueesortie, string nomchaufeur, string numchauffeur, DateTime date, int reste, string type, int Retrais)
        {
            string query = @"
                begin transaction;

                insert into Retrait(Designation, Colis, Entree, Nature, Quantite, Declarant, Sortie, Nom_Chauffeur, Num_Chauffeur, Date) values(@Designation, @Colis, @Entree, @Nature, @Quantite, @Declarant, @Sortie, @Nom_Chauffeur, @Num_Chauffeur, @Date);

                Update Magasinage set Reste = Reste - @Quantite where Colis = @Colis;

                Insert into Circulation(Type, Declarant, Importateur, Plaque, Date, Reste, Colis, Retrais) values(@Type, @Declarant, @Importateur, @Plaque, @Date, @Reste, @Colis, @Retrais);

                commit transaction;";

            SqlParameter[] parameter = new SqlParameter[12];

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[0] = new SqlParameter("@Designation", DBNull.Value);
                parameter[0].Value = Designation;
            }
            else
            {
                parameter[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[0].Value = Designation;
            }

            parameter[1] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[1].Value = Colis;

            parameter[2] = new SqlParameter("@Entree", SqlDbType.Int);
            parameter[2].Value = plaqueentre;


            if (string.IsNullOrEmpty(nature))
            {
                parameter[3] = new SqlParameter("@Nature", DBNull.Value);
                parameter[3].Value = nature;
            }
            else
            {
                parameter[3] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[3].Value = nature;
            }
            parameter[4] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[4].Value = Quantite;

            parameter[5] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[5].Value = Declarant;


            if (string.IsNullOrEmpty(plaqueesortie))
            {
                parameter[6] = new SqlParameter("@Sortie", DBNull.Value);
                parameter[6].Value = plaqueesortie;
            }
            else
            {
                parameter[6] = new SqlParameter("@Sortie", SqlDbType.VarChar);
                parameter[6].Value = plaqueesortie;
            }


            if (string.IsNullOrEmpty(nomchaufeur))
            {
                parameter[7] = new SqlParameter("@Nom_Chauffeur", DBNull.Value);
                parameter[7].Value = nomchaufeur;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nom_Chauffeur", SqlDbType.VarChar);
                parameter[7].Value = nomchaufeur;
            }

            if (string.IsNullOrEmpty(numchauffeur))
            {
                parameter[8] = new SqlParameter("@Num_Chauffeur", DBNull.Value);
                parameter[8].Value = numchauffeur;
            }
            else
            {
                parameter[8] = new SqlParameter("@Num_Chauffeur", SqlDbType.VarChar);
                parameter[8].Value = numchauffeur;
            }
            parameter[9] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[9].Value = date;

            parameter[10] = new SqlParameter("@Reste", SqlDbType.Date);
            parameter[10].Value = reste;

            if (string.IsNullOrEmpty(type))
            {
                parameter[11] = new SqlParameter("@Type", DBNull.Value);
                parameter[11].Value = type;
            }
            else
            {
                parameter[11] = new SqlParameter("@Type", SqlDbType.VarChar);
                parameter[11].Value = type;
            }

            parameter[10] = new SqlParameter("@Retrais", SqlDbType.Int);
            parameter[10].Value = Retrais;

            if (connexion.setdata(query, parameter) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierRetraitParTransactionPlus(int idcolis, int id, string Designation, string nature, int Quantite, string plaqueesortie, string nomchaufeur, string numchauffeur, DateTime date, int Val)
        {
            string query = @"
                begin transaction;

                Update Magasinage set  Reste = Reste-@Val where Colis = @Colis;

                Update Circulation set  Reste = Reste-@Val where Colis = @Colis and Retrais = @Id_Retrait;

                Update Retrait set Designation=@Designation, Nature=@Nature, Quantite=@Quantite, Sortie=@Sortie, Nom_Chauffeur = @Nom_Chauffeur, Num_Chauffeur = @Num_Chauffeur, Date = @Date where Id_Retrait = @Id_Retrait ;

                commit transaction;";

            SqlParameter[] parameter = new SqlParameter[10];

            parameter[9] = new SqlParameter("@Id_Retrait", SqlDbType.Int);
            parameter[9].Value = id;

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[0] = new SqlParameter("@Designation", DBNull.Value);
                parameter[0].Value = Designation;
            }
            else
            {
                parameter[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[0].Value = Designation;
            }



            if (string.IsNullOrEmpty(nature))
            {
                parameter[1] = new SqlParameter("@Nature", DBNull.Value);
                parameter[1].Value = nature;
            }
            else
            {
                parameter[1] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[1].Value = nature;
            }
            parameter[2] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[2].Value = Quantite;



            if (string.IsNullOrEmpty(plaqueesortie))
            {
                parameter[3] = new SqlParameter("@Sortie", DBNull.Value);
                parameter[3].Value = plaqueesortie;
            }
            else
            {
                parameter[3] = new SqlParameter("@Sortie", SqlDbType.VarChar);
                parameter[3].Value = plaqueesortie;
            }


            if (string.IsNullOrEmpty(nomchaufeur))
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", DBNull.Value);
                parameter[4].Value = nomchaufeur;
            }
            else
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", SqlDbType.VarChar);
                parameter[4].Value = nomchaufeur;
            }

            if (string.IsNullOrEmpty(numchauffeur))
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", DBNull.Value);
                parameter[5].Value = numchauffeur;
            }
            else
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", SqlDbType.VarChar);
                parameter[5].Value = numchauffeur;
            }
            parameter[6] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[6].Value = date;

            parameter[7] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[7].Value = Val;

            parameter[8] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[8].Value = idcolis;

            if (connexion.setdata(query, parameter) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierRetraitParTransactionMoins(int idcolis, int id, string Designation, string nature, int Quantite, string plaqueesortie, string nomchaufeur, string numchauffeur, DateTime date, int Val)
        {
            string query = @"
                begin transaction;

                Update Magasinage set  Reste = Reste+@Val where Colis = @Colis;

                Update Circulation set  Reste = Reste+@Val where Colis = @Colis and Retrais = @Id_Retrait;

                Update Retrait set Designation=@Designation, Nature=@Nature, Quantite=@Quantite, Sortie=@Sortie, Nom_Chauffeur = @Nom_Chauffeur, Num_Chauffeur = @Num_Chauffeur, Date = @Date where Id_Retrait = @Id_Retrait ;

                commit transaction;";

            SqlParameter[] parameter = new SqlParameter[10];

            parameter[9] = new SqlParameter("@Id_Retrait", SqlDbType.Int);
            parameter[9].Value = id;

            if (string.IsNullOrEmpty(Designation))
            {
                parameter[0] = new SqlParameter("@Designation", DBNull.Value);
                parameter[0].Value = Designation;
            }
            else
            {
                parameter[0] = new SqlParameter("@Designation", SqlDbType.VarChar);
                parameter[0].Value = Designation;
            }



            if (string.IsNullOrEmpty(nature))
            {
                parameter[1] = new SqlParameter("@Nature", DBNull.Value);
                parameter[1].Value = nature;
            }
            else
            {
                parameter[1] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[1].Value = nature;
            }
            parameter[2] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[2].Value = Quantite;



            if (string.IsNullOrEmpty(plaqueesortie))
            {
                parameter[3] = new SqlParameter("@Sortie", DBNull.Value);
                parameter[3].Value = plaqueesortie;
            }
            else
            {
                parameter[3] = new SqlParameter("@Sortie", SqlDbType.VarChar);
                parameter[3].Value = plaqueesortie;
            }


            if (string.IsNullOrEmpty(nomchaufeur))
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", DBNull.Value);
                parameter[4].Value = nomchaufeur;
            }
            else
            {
                parameter[4] = new SqlParameter("@Nom_Chauffeur", SqlDbType.VarChar);
                parameter[4].Value = nomchaufeur;
            }

            if (string.IsNullOrEmpty(numchauffeur))
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", DBNull.Value);
                parameter[5].Value = numchauffeur;
            }
            else
            {
                parameter[5] = new SqlParameter("@Num_Chauffeur", SqlDbType.VarChar);
                parameter[5].Value = numchauffeur;
            }
            parameter[6] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[6].Value = date;

            parameter[7] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[7].Value = Val;

            parameter[8] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[8].Value = idcolis;

            if (connexion.setdata(query, parameter) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable TopdernierDesignationRetrais()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("SELECT TOP 1 Designation  FROM Retrait order by Id_Retrait DESC", null);
            return table;
        }
    }
}
