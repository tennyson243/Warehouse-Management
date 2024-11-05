using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Classes
{
    class Colis
    {
        BDD.Connecteur connexion = new BDD.Connecteur();



        public Boolean AjouterVoiture(string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image, string TypemArch)
        {
            string query = "insert into Colis (Designation, Plaque, Importateur, Manifeste, Declarant, Plomb, Quantite, Nature, Date, Photo, TypeMarchandise) values (@Designation, @Plaque, @Importateur, @Manifeste, @Declarant, @Plomb, @Quantite, @Nature, @Date, @Photo, @TypeMarchandise)";

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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[10] = new SqlParameter("@TypeMarchandise", DBNull.Value);
                parameter[10].Value = TypemArch;
            }
            else
            {
                parameter[10] = new SqlParameter("@TypeMarchandise", SqlDbType.VarChar);
                parameter[10].Value = TypemArch;
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

        public Boolean ModifierVoiture(int id, string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image, string TypemArch)
        {
            string query = "Update Colis set Designation=@Designation, Plaque =@Plaque, Importateur=@Importateur, Manifeste = @Manifeste, Declarant = @Declarant, Plomb= @Plomb, Quantite = @Quantite, Nature = @Nature, Date = @Date, Photo= @Photo, TypeMarchandise=@TypeMarchandise where Id_Colis = @Id_Colis";

            SqlParameter[] parameter = new SqlParameter[12];

            parameter[10] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameter[10].Value = id;

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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[11] = new SqlParameter("@TypeMarchandise", DBNull.Value);
                parameter[11].Value = TypemArch;
            }
            else
            {
                parameter[11] = new SqlParameter("@TypeMarchandise", SqlDbType.VarChar);
                parameter[11].Value = TypemArch;
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

        public DataTable listvoiture()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from Colis", null);
            return table;
        }

        public DataTable ListDesColisTotalEntree()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select colis.Designation from Colis group by Colis.Designation", null);
            return table;
        }

        public DataTable ListDesColisTotalSortie()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select * from TotalSortie", null);
            return table;
        }

        public DataTable listdistinctColis()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("select dISTINCT(DESIGNATION) as Colis from colis", null);
            return table;
        }

        public DataTable listcolisJointure()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Colis, Designation,Securite.Plaque as Plaque, Importateur.Nom as Importateur , Manifeste, Declarant.Nom as Declarant, Plomb, Quantite, Nature, Colis.Date as Date from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant", null);
            return table;
        }

        public DataTable top10daheboard()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select TOP 10  id_Colis, Designation,Securite.Plaque as Plaque, Importateur.Nom as Importateur , Manifeste, Declarant.Nom as Declarant, Plomb, Quantite, Nature, Colis.Date as Date, Colis.Photo as Photo from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant ORDER BY Id_Colis desc", null);
            return table;
        }
        public DataTable listcolisJointureModifier()
        {
            DataTable table = new DataTable();
            table = connexion.getdata("Select id_Colis as ID, Securite.Plaque as Plaque, Quantite, Nature from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant", null);
            return table;
        }

        public Boolean EffaceColis(int id)
        {
            string query = "Delete from Colis where Id_Colis = @Id_Colis";
            SqlParameter[] parameter = new SqlParameter[1];

            parameter[0] = new SqlParameter("@Id_Colis", SqlDbType.Int);
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

        public DataTable getColisbyid(int id)
        {
            DataTable table = new DataTable();
            string query = "Select*from Colis where Id_Colis = @Id_Colis";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }

        public Boolean AjoutParTransaction(string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image, int colis, string type, int retrais, int vehicule)
        {
            string query = @"
                             begin transaction;
                             Insert into Magasinage(Vehicule, Colis, Nature, Quantite, Importateur, Reste) values(@Plaque, @Colis, @Nature, @Quantite, @Importateur, @Quantite);
                             Insert into Circulation(Type, Declarant, Importateur, Plaque, Date, Reste, Colis, Retrais) values(@Type, @Declarant, @Importateur, @Plaque, @Date, @Quantite, @Colis, @Retrais);
                             Update Securite set Date_Sortie=@Date, Statut='En Magasin' where Id_Securite=@Vehicule;
                             commit transaction";

            SqlParameter[] parameter = new SqlParameter[14];


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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.DateTime);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            parameter[10] = new SqlParameter("@Colis", SqlDbType.Int);
            parameter[10].Value = colis;

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

            parameter[12] = new SqlParameter("@Retrais", SqlDbType.Int);
            parameter[12].Value = retrais;

            parameter[13] = new SqlParameter("@Vehicule", SqlDbType.Int);
            parameter[13].Value = vehicule;


            if (connexion.setdata(query, parameter) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierColisParTransactionPlus(int id, string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image, int v)
        {
            string query = @"
                        begin transaction;

                        Update Magasinage set Quantite = @Quantite, Reste =@Quantite-@Val where Colis=@Colis;

                        Update Colis set Designation=@Designation, Plaque =@Plaque, Importateur=@Importateur, Manifeste = @Manifeste, Declarant = @Declarant, Plomb= @Plomb, Quantite = @Quantite, Nature = @Nature, Date = @Date, Photo= @Photo where Id_Colis = @Id_Colis;

                        commit transaction;";

            SqlParameter[] parameter = new SqlParameter[11];

            parameter[11] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameter[11].Value = id;

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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            parameter[10] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[10].Value = v;




            if (connexion.setdata(query, parameter) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierColisParTransactionMoins(int id, string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image, int v)
        {
            string query = @"
                        begin transaction;

                        Update Magasinage set Quantite = @Quantite,  Reste =@Quantite-@Val where Colis=@Colis

                        Update Colis set Designation=@Designation, Plaque =@Plaque, Importateur=@Importateur, Manifeste = @Manifeste, Declarant = @Declarant, Plomb= @Plomb, Quantite = @Quantite, Nature = @Nature, Date = @Date, Photo= @Photo where Id_Colis = @Id_Colis;

                        commit transaction;";

            SqlParameter[] parameter = new SqlParameter[11];

            parameter[10] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameter[10].Value = id;

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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            parameter[10] = new SqlParameter("@Val", SqlDbType.Int);
            parameter[10].Value = v;


            if (connexion.setdata(query, parameter) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ModifierColisParTransactionEgale(int id, string Designation, int plaque, int importateur, string manifeste, int declarant, string plomb, int stockdeclarer, string nature, DateTime date, Byte[] image)
        {
            string query = @"
                        begin transaction;

                        Update Colis set Designation=@Designation, Plaque =@Plaque, Importateur=@Importateur, Manifeste = @Manifeste, Declarant = @Declarant, Plomb= @Plomb, Quantite = @Quantite, Nature = @Nature, Date = @Date, Photo= @Photo where Id_Colis = @Id_Colis;

                        Update Magasinage set Vehicule =@Plaque, Colis = @Id_Colis,  Nature = @Nature, Quantite=@Quantite, Importateur = @Importateur where Colis = @Id_Colis;

                        commit transaction;";

            SqlParameter[] parameter = new SqlParameter[10];

            parameter[10] = new SqlParameter("@Id_Colis", SqlDbType.Int);
            parameter[10].Value = id;

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

            parameter[1] = new SqlParameter("@Plaque", SqlDbType.Int);
            parameter[1].Value = plaque;

            parameter[2] = new SqlParameter("@Importateur", SqlDbType.Int);
            parameter[2].Value = importateur;

            if (string.IsNullOrEmpty(manifeste))
            {
                parameter[3] = new SqlParameter("@Manifeste", DBNull.Value);
                parameter[3].Value = manifeste;
            }
            else
            {
                parameter[3] = new SqlParameter("@Manifeste", SqlDbType.VarChar);
                parameter[3].Value = manifeste;
            }

            parameter[4] = new SqlParameter("@Declarant", SqlDbType.Int);
            parameter[4].Value = declarant;


            if (string.IsNullOrEmpty(plomb))
            {
                parameter[5] = new SqlParameter("@Plomb", DBNull.Value);
                parameter[5].Value = plomb;
            }
            else
            {
                parameter[5] = new SqlParameter("@Plomb", SqlDbType.VarChar);
                parameter[5].Value = plomb;
            }

            parameter[6] = new SqlParameter("@Quantite", SqlDbType.Int);
            parameter[6].Value = stockdeclarer;

            if (string.IsNullOrEmpty(nature))
            {
                parameter[7] = new SqlParameter("@Nature", DBNull.Value);
                parameter[7].Value = nature;
            }
            else
            {
                parameter[7] = new SqlParameter("@Nature", SqlDbType.VarChar);
                parameter[7].Value = nature;
            }

            parameter[8] = new SqlParameter("@Date", SqlDbType.Date);
            parameter[8].Value = date;

            parameter[9] = new SqlParameter("@Photo", SqlDbType.Binary);
            parameter[9].Value = image;

            if (connexion.setdata(query, parameter) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable Getdata(int id)
        {
            DataTable table = new DataTable();
            string query = "select Date_Entree from Securite where id_Securite=@id_Securite";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id_Securite", SqlDbType.Int);
            parameters[0].Value = id;
            table = connexion.getdata(query, parameters);
            return table;
        }
    }
}



//< add name = "Gestion_Entrepot.Properties.Settings.EntrepotConnectionString" connectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Entrepot.mdf;Integrated Security=True;Connect Timeout=30" providerName = "System.Data.SqlClient" />
     
//         < add name = "Gestion_Entrepot.Properties.Settings.VueConnexionString" connectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=&quot;C:\Users\JEAN MARIE\Documents\Entrepot.mdf&quot;;Integrated Security=True;Connect Timeout=30" providerName = "System.Data.SqlClient" />