using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Rapport
{
    public partial class Statistique : Form
    {
        public Statistique()
        {
            InitializeComponent();
        }
        struct DataParameter
        {
            public List<DataSetStatistique> Stocklist;
            public string FileName { get; set; }
        }
        DataParameter _InputParameter;

        Classes.Rapport rapport = new Classes.Rapport();
        private void Statistique_Load(object sender, EventArgs e)
        {
            GridDate.DataSource = rapport.Statistique();
            DataGridViewEtat.DataSource = rapport.Statistique();

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {

        }

        private void GridDate_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = GridDate.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void GridDate_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            try
            {
                bunifuVScrollBar1.Maximum = GridDate.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<DataSetStatistique> list = ((DataParameter)e.Argument).Stocklist;
            string Filename = ((DataParameter)e.Argument).FileName;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet Ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = false;
            int index = 1;
            int process = list.Count;
            Ws.Cells[1, 1] = "Date";
            Ws.Cells[1, 2] = "Entree";
            Ws.Cells[1, 3] = "Sortie";
            foreach (DataSetStatistique sr in list)
            {
                //if (!backgroundWorker.CancellationPending)
                //{
                //    backgroundWorker.ReportProgress(index++ * 100 / process);
                //    Ws.Cells[index, 1] = sr.DataS.ToString();
                //    Ws.Cells[index, 2] = sr.Entree.ToString();
                //    Ws.Cells[index, 3] = sr.Sortie.ToString();
                //}
            }
            Ws.SaveAs(Filename, XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                //ProgressBar2.Value = e.ProgressPercentage;
                //labelProgrese2.Text = string.Format("Progression...{0}", e.ProgressPercentage);
                //ProgressBar2.Update();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                    //Thread.Sleep(100);
                    //labelProgrese2.Text = "Effectuer";
                    //MessageBox.Show("Vos donnees on ete Exporter avec Succes Vers Excel", "Exporter vers Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            //if (backgroundWorker.IsBusy)
            //    return;
            //using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            //{
            //    if (sfd.ShowDialog() == DialogResult.OK)
            //    {
            //            _InputParameter.FileName = sfd.FileName;
            //            _InputParameter.Stocklist = GridDate.DataSource as List<vue_Statistique>;
            //            ProgressBar2.Minimum = 1;
            //            ProgressBar2.Value = 0;
            //            backgroundWorker.RunWorkerAsync(_InputParameter);
            //    }
            //}
        }

        private void ToggleSwitchEtat_OnValuechange(object sender, EventArgs e)
        {
            if (ToggleSwitchEtat.Value == true)
            {
                PanelListeCouleurs.BringToFront();
            }
            else
            {
                PanelListeBlanc.BringToFront();
            }
        }

        private void bunifuVScrollBar1_Scroll_1(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                GridDate.FirstDisplayedScrollingRowIndex = GridDate.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select * from Statistique where Date_Entree between '" + DateTimePickerdudate.Value.ToString("dd-MM-yyyy") + "' and  '" + DateTimeAudate.Value.ToString("dd-MM-yyyy") + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            GridDate.DataSource = table;
            DataGridViewEtat.DataSource = table;
        }
    }
}
