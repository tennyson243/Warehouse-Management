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
    public partial class PrintEtatduStock : Form
    {
        public PrintEtatduStock()
        {
            InitializeComponent();
        }

        Print.EtatDuStock stock = new EtatDuStock();
        private void PrintEtatduStock_Load(object sender, EventArgs e)
        {
            SqlConnection connextion = new SqlConnection();
            connextion.ConnectionString = ConfigurationManager.ConnectionStrings["Gestion_Entrepot.Properties.Settings.StatistiqueString"].ToString();

            string query = "Select * from View_Stock_Restant";
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connextion);
            adapter.Fill(ds, "View_Stock_Restant");
            DataTable dt = ds.Tables["View_Stock_Restant"];
            stock.SetDataSource(ds.Tables["View_Stock_Restant"]);
            crystalReportViewer1.ReportSource = stock;
            crystalReportViewer1.Refresh();
        }
    }
}
