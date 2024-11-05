using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Gestion_Entrepot.Print
{
    public partial class PrintFicheMagasin : Form
    {
        public PrintFicheMagasin()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void PrintFicheMagasin_Load(object sender, EventArgs e)
        {

            //FicheMagasinReport fm = new FicheMagasinReport();
            //SqlConnection connextion = new SqlConnection();
            //connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.EntrepotConnectionString"].ToString();

            //string query = "Select Convert(varchar(20), Colis.Date,103) as Date_Chargement, iif(Circulation.Reste>0,'-', Convert(varchar(20), Circulation.Date,103)) as Date_Dechargement,  Securite.Plaque, Importateur.Nom as Importateur, Declarant.Nom as Declarant, Colis.Nature, Colis.Quantite from Colis inner join Retrait on Retrait.Colis = Colis.Id_Colis inner join Importateur on Colis.Importateur = Importateur.Id_Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Magasinage on Magasinage.Colis = Colis.Id_Colis inner join Circulation on Circulation.Colis = Colis.Id_Colis";
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            //adapter.Fill(ds, "FicheMagasin");
            //DataTable dt = ds.Tables["FicheMagasin"];
            //fm.SetDataSource(ds.Tables["FicheMagasin"]);
            //crystalReportViewer1.ReportSource = fm;
            //crystalReportViewer1.Refresh();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

            //FicheMagasinReport fm = new FicheMagasinReport();
            //SqlConnection connextion = new SqlConnection();
            //connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.EntrepotConnectionString"].ToString();

            //string query = "Select Convert(varchar(20), Colis.Date,103) as Date_Chargement, iif(Circulation.Reste>0,'-', Convert(varchar(20), Circulation.Date,103)) as Date_Dechargement,  Securite.Plaque, Importateur.Nom as Importateur, Declarant.Nom as Declarant, Colis.Nature, Colis.Quantite from Colis inner join Retrait on Retrait.Colis = Colis.Id_Colis inner join Importateur on Colis.Importateur = Importateur.Id_Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Magasinage on Magasinage.Colis = Colis.Id_Colis inner join Circulation on Circulation.Colis = Colis.Id_Colis  where Colis.Date between  '" + DateTimePickerdudate.Value.ToString("yyyy-MM-dd") + "' and  '" + DateTimeAudate.Value.ToString("yyyy-MM-dd") + "'";
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            //adapter.Fill(ds, "FicheMagasin");
            //DataTable dt = ds.Tables["FicheMagasin"];
            //fm.SetDataSource(ds.Tables["FicheMagasin"]);
            //crystalReportViewer1.ReportSource = fm;
            //crystalReportViewer1.Refresh();
        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            //FicheMagasinReport fm = new FicheMagasinReport();
            //SqlConnection connextion = new SqlConnection();
            //connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.EntrepotConnectionString"].ToString();

            //string query = "Select Convert(varchar(20), Colis.Date,103) as Date_Chargement, iif(Circulation.Reste>0,'-', Convert(varchar(20), Circulation.Date,103)) as Date_Dechargement,  Securite.Plaque, Importateur.Nom as Importateur, Declarant.Nom as Declarant, Colis.Nature, Colis.Quantite from Colis inner join Retrait on Retrait.Colis = Colis.Id_Colis inner join Importateur on Colis.Importateur = Importateur.Id_Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Magasinage on Magasinage.Colis = Colis.Id_Colis inner join Circulation on Circulation.Colis = Colis.Id_Colis";
            //DataSet ds = new DataSet();
            //SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            //adapter.Fill(ds, "FicheMagasin");
            //DataTable dt = ds.Tables["FicheMagasin"];
            //fm.SetDataSource(ds.Tables["FicheMagasin"]);
            //crystalReportViewer1.ReportSource = fm;
            //crystalReportViewer1.Refresh();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
