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
    public partial class PrintSuiviStock : Form
    {
        public PrintSuiviStock()
        {
            InitializeComponent();
        }

        Print.SuivisStock stock = new SuivisStock();
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void PrintSuiviStock_Load(object sender, EventArgs e)
        {
            SqlConnection connextion = new SqlConnection();
            connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.StatistiqueString"].ToString();

            string query = "Select * from View_FicheMagasin";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            adapter.Fill(ds, "View_FicheMagasin");
            DataTable dt = ds.Tables["View_FicheMagasin"];
            stock.SetDataSource(ds.Tables["View_FicheMagasin"]);
            crystalReportViewer1.ReportSource = stock;
            crystalReportViewer1.Refresh();
        }
    }
}
