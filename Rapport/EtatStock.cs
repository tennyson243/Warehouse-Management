using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Gestion_Entrepot.Rapport
{
    public partial class EtatStock : Form
    {
        public EtatStock()
        {
            InitializeComponent();
        }
        
        Classes.Rapport rapport = new Classes.Rapport();

        struct DataParameter
        {
            public List<View_Stock_Restant> Stocklist;
            public string FileName { get; set; }
        }
        DataParameter _InputParameter;
        private void EtatStock_Load(object sender, EventArgs e)
        {
            using (MDLString db = new MDLString())
            {
                DataGridViewEtat.DataSource = db.View_Stock_Restant.ToList();
                DataGridViewEtatCouleurs.DataSource = db.View_Stock_Restant.ToList();
            }


        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
          
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<View_Stock_Restant> list = ((DataParameter)e.Argument).Stocklist;
            string Filename = ((DataParameter)e.Argument).FileName;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet Ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = false;
            int index = 1;
            int process = list.Count;
            Ws.Cells[1, 1] = "Plaque";
            Ws.Cells[1, 2] = "Designation";
            Ws.Cells[1, 3] = "Nature";
            Ws.Cells[1, 4] = "Quantite";
            Ws.Cells[1, 5] = "Importateur";
            Ws.Cells[1, 6] = "Reste";
            foreach (View_Stock_Restant sr in list)
            {
                if (!backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(index++ * 100 / process);
                    Ws.Cells[index, 1] = sr.Plaque.ToString();
                    Ws.Cells[index, 2] = sr.Designation.ToString();
                    Ws.Cells[index, 3] = sr.Nature.ToString();
                    Ws.Cells[index, 4] = sr.Quantite.ToString();
                    Ws.Cells[index, 5] = sr.Importateur.ToString();
                    Ws.Cells[index, 6] = sr.Reste.ToString();

                }
            }
            Ws.SaveAs(Filename, XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(ToggleSwitchEtat.Value==true)
            {
                ProgressBar2.Value = e.ProgressPercentage;
                labelProgrese2.Text = string.Format("Progression...{0}", e.ProgressPercentage);
                ProgressBar2.Update();
            }
            else
            {
                ProgressBar1.Value = e.ProgressPercentage;
                LabelProgression.Text = string.Format("Progression...{0}", e.ProgressPercentage);
                ProgressBar1.Update();
            }
            
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error==null)
            {
                if (ToggleSwitchEtat.Value == true)
                {
                    Thread.Sleep(100);
                    labelProgrese2.Text = "Effectuer";
                    MessageBox.Show("Vos donnees on ete Exporter avec Succes Vers Excel", "Exporter vers Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Thread.Sleep(100);
                    LabelProgression.Text = "Effectuer";
                    MessageBox.Show("Vos donnees on ete Exporter avec Succes Vers Excel", "Exporter vers Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
        }

        private void DataGridViewEtat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            

        }

        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {
           
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.DataGridViewEtat.Width, this.DataGridViewEtat.Height);
            DataGridViewEtat.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.DataGridViewEtat.Width, this.DataGridViewEtat.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewEtatCouleurs.FirstDisplayedScrollingRowIndex = DataGridViewEtatCouleurs.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewEtatCouleurs_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void DataGridViewEtatCouleurs_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewEtatCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewEtatCouleurs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewEtatCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void ToggleSwitchEtat_OnValuechange(object sender, EventArgs e)
        {
            
        }

        private void guna2CircleButton6_Click_1(object sender, EventArgs e)
        {
            //    DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            //    printer.Title = "ETAT DU STOCK";
            //    printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            //    printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            //    printer.PageNumbers = true;
            //    printer.PageNumberInHeader = false;
            //    printer.PorportionalColumns = true;
            //    printer.HeaderCellAlignment = System.Drawing.StringAlignment.Near;
            //    printer.Footer = "Entrepot de douane de type B Ihusi";
            //    printer.FooterSpacing = 15;
            //    printer.printDocument.DefaultPageSettings.Landscape = true;
            //    if (ToggleSwitchEtat.Value == true)
            //    {
            //        printer.PrintDataGridView(DataGridViewEtatCouleurs);
            //    }
            //    else
            //    {
            //        printer.PrintDataGridView(DataGridViewEtat);
            //    }

            Print.PrintEtatduStock etat = new Print.PrintEtatduStock();
            etat.Show();
        }

        private void guna2CircleButton5_Click_1(object sender, EventArgs e)
        {
            
            if (backgroundWorker.IsBusy)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (ToggleSwitchEtat.Value == true)
                    {
                        _InputParameter.FileName = sfd.FileName;
                        _InputParameter.Stocklist = DataGridViewEtatCouleurs.DataSource as List<View_Stock_Restant>;
                        ProgressBar2.Minimum = 1;
                        ProgressBar2.Value = 0;
                        backgroundWorker.RunWorkerAsync(_InputParameter);
                    }
                    else
                    {
                        _InputParameter.FileName = sfd.FileName;
                        _InputParameter.Stocklist = DataGridViewEtat.DataSource as List<View_Stock_Restant>;
                        ProgressBar1.Minimum = 1;
                        ProgressBar1.Value = 0;
                        backgroundWorker.RunWorkerAsync(_InputParameter);
                    }

                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ToggleSwitchEtat_OnValuechange_1(object sender, EventArgs e)
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select * from View_Stock_Restant where CONCAT(Plaque,Designation, Importateur,Nature) like '%" + TextBoxRecherche.Text + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);
            DataGridViewEtatCouleurs.DataSource = table;
            DataGridViewEtat.DataSource = table;
        }
    }
}
