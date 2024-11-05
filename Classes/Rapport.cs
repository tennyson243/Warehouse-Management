using System.Data;

namespace Gestion_Entrepot.Classes
{
    class Rapport
    {
        BDD.Connecteur connexion = new BDD.Connecteur();
        public DataTable Statistique()
        {
            string query;
            query = "select * from View_Statistique";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable Circulation()
        {
            string query;
            query = "Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable CirculationAujourdhui()
        {
            string query;
            query = "Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis where Circulation.Date = Cast(GETDATE() as date)";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable ResteStock()
        {
            string query;
            query = "Select Securite.Plaque, Colis.Designation, Magasinage.Nature, Magasinage.Quantite, iif(Importateur.Nom ='-',' ',Importateur.Nom)as Importateur,   Magasinage.Reste  from Magasinage inner join Securite on Securite.Id_Securite = Magasinage.Vehicule inner join Colis on Colis.Id_Colis = Magasinage.Colis inner join Importateur on Importateur.Id_Importateur = Magasinage.Importateur";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable ListImageRetrais()
        {
            string query;
            query = "Select top 10 id_Retrait, Retrait.Designation as Designation, Colis.Designation, Securite.Plaque as Plaque, Retrait.Nature as Nature, Retrait.Quantite as Quantite, CONVERT(varchar(25),Retrait.Date, 103) as Date, Colis.Photo as Photo from Retrait inner join Colis on Colis.Id_Colis=Retrait.Colis inner join Securite on Securite.Id_Securite=Retrait.Entree order by Id_Retrait Desc";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

        public DataTable ColisDashboard()
        {
            string query;
            query = "Select top 5  Colis.Photo, Designation,  Securite.Plaque, Importateur.Nom as Importateur, Declarant.Nom as Declarant, Nature, Quantite from Colis inner join Securite on Securite.Id_Securite=Colis.Plaque inner join Importateur on Importateur.Id_Importateur=Colis.Importateur inner join Declarant on Declarant.Id_Declarant= Colis.Declarant order by Id_Colis Desc";
            DataTable table = new DataTable();
            table = connexion.getdata(query, null);
            return table;

        }

    }
}
