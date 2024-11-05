using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Print
{
    public partial class PrintEffetPersonnel : Form
    {
        string Designation;
        public PrintEffetPersonnel(string Des)
        {
            InitializeComponent();
            this.Designation = Des;
        }

        Classes.Retrait retrait = new Classes.Retrait();
        Print.EffetPersonnelle effet = new EffetPersonnelle();
        Print.Personnelle Perso = new Personnelle();
        Print.EffetRevision revison = new EffetRevision();
        private void PrintEffetPersonnel_Load(object sender, EventArgs e)
        {

            ComboBoxRetrait.DataSource = retrait.list();
            ComboBoxRetrait.DisplayMember = "Designation";
            ComboBoxRetrait.ValueMember = "Id_Retrait";
            ComboBoxRetrait.Text = Designation;

           
            SqlConnection connextion = new SqlConnection();
            connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.StatistiqueString"].ToString();

            string query = "Select id_Retrait as ID, Retrait.Designation as Designation, Colis.Designation as Colis , Securite.Plaque as Plaque, Retrait.Nature as Nature, Retrait.Quantite as Quantite, Declarant.Nom as Declarant, Retrait.Sortie as Plaque_Sortie,  Nom_Chauffeur, Num_Chauffeur, Convert(varchar(25), Retrait.Date,103) as Date, QRCODE from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant inner join Colis on Colis.Id_Colis = Retrait.Colis  where Retrait.Designation like '%" + ComboBoxRetrait.Text.ToString() + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            adapter.Fill(ds, "View_Effet_Personel");
            DataTable dt = ds.Tables["View_Effet_Personel"];
            Perso.SetDataSource(ds.Tables["View_Effet_Personel"]);
            crystalReportViewer1.ReportSource = Perso;
            crystalReportViewer1.Refresh();
        }

        private void ComboBoxRetrait_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ComboBoxRetrait_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection connextion = new SqlConnection();
            connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.StatistiqueString"].ToString();

            string query = "Select id_Retrait as ID, Retrait.Designation as Designation, Colis.Designation as Colis , Securite.Plaque as Plaque, Retrait.Nature as Nature, Retrait.Quantite as Quantite, Declarant.Nom as Declarant, Retrait.Sortie as Plaque_Sortie,  Nom_Chauffeur, Num_Chauffeur, Convert(varchar(25), Retrait.Date,103) as Date, QRCODE from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant inner join Colis on Colis.Id_Colis = Retrait.Colis where Retrait.Designation like '%" + ComboBoxRetrait.Text.ToString() + "%'";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            adapter.Fill(ds, "View_Effet_Personel");
            DataTable dt = ds.Tables["View_Effet_Personel"];
            Perso.SetDataSource(ds.Tables["View_Effet_Personel"]);
            crystalReportViewer1.ReportSource = Perso;
            crystalReportViewer1.Refresh();
        }
    }
}
