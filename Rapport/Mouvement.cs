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
    public partial class Mouvement : Form
    {
        public Mouvement()
        {
            InitializeComponent();
        }
        struct DataParameter
        {
            public List<View_Mouvement> Stocklist;
            public string FileName { get; set; }
        }
        struct DataParameter2
        {
            //public List<View_mouv_Aujourdhui> Stocklist2;
            public string FileName2 { get; set; }
        }

        DataParameter _InputParameter;
        private void Mouvement_Load(object sender, EventArgs e)
        {
            using (MDLString db = new MDLString())
            {
                DataGridViewCirculation.DataSource = db.View_Mouvement.ToList();

                DataGridViewCouleurs.DataSource = db.View_Mouvement.ToList();

                //DataGridViewCouleursAujourdhui.DataSource = db.View_mouv_Aujourdhui.ToList();
                //DataGridViewCirculationAujourdhui.DataSource = db.View_mouv_Aujourdhui.ToList();
            }
            Classes.Rapport rapport = new Classes.Rapport();
           

        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            //try
            //{
            //    DataGridViewCouleursAujourdhui.FirstDisplayedScrollingRowIndex = DataGridViewCouleursAujourdhui.Rows[e.Value].Index;
            //}
            //catch (Exception)
            //{


            //}
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewCouleurs.FirstDisplayedScrollingRowIndex = DataGridViewCouleurs.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCouleurs_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCouleurs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCouleursAujourdhui_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //try
            //{
            //    bunifuVScrollBar2.Maximum = DataGridViewCouleursAujourdhui.RowCount - 1;
            //}
            //catch (Exception)
            //{


            //}
        }

        private void DataGridViewCouleursAujourdhui_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //try
            //{
            //    bunifuVScrollBar2.Maximum = DataGridViewCouleursAujourdhui.RowCount - 1;
            //}
            //catch (Exception)
            //{


            //}
        }

        private void ToggleSwitchEtat_OnValuechange(object sender, EventArgs e)
        {
            if (ToggleSwitchEtat.Value == true)
            {
               PanelCouleurs.BringToFront();
            }
            else
            {
                PanelClaire.BringToFront();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<View_Mouvement> list = ((DataParameter)e.Argument).Stocklist;
            string Filename = ((DataParameter)e.Argument).FileName;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet Ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = false;
            int index = 1;
            int process = list.Count;
            Ws.Cells[1, 1] = "Type";
            Ws.Cells[1, 2] = "Date";
            Ws.Cells[1, 3] = "Plaque";
            Ws.Cells[1, 4] = "Declarant";
            Ws.Cells[1, 5] = "Importateur";
            foreach (View_Mouvement sr in list)
            {
                if (!backgroundWorker1.CancellationPending)
                {
                    backgroundWorker1.ReportProgress(index++ * 100 / process);
                    Ws.Cells[index, 1] = sr.Type.ToString();
                    Ws.Cells[index, 2] = sr.Date.ToString();
                    Ws.Cells[index, 3] = sr.Plaque.ToString();
                    Ws.Cells[index, 4] = sr.Declarant.ToString();
                    Ws.Cells[index, 5] = sr.Importateur.ToString();
                }
            }
            Ws.SaveAs(Filename, XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ToggleSwitchEtat.Value == true)
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
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

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (ToggleSwitchEtat.Value == true)
                    {
                        _InputParameter.FileName = sfd.FileName;
                        _InputParameter.Stocklist = DataGridViewCouleurs.DataSource as List<View_Mouvement>;
                        ProgressBar2.Minimum = 1;
                        ProgressBar2.Value = 0;
                        backgroundWorker1.RunWorkerAsync(_InputParameter);
                    }
                    else
                    {
                        _InputParameter.FileName = sfd.FileName;
                        _InputParameter.Stocklist = DataGridViewCirculation.DataSource as List<View_Mouvement>;
                        ProgressBar1.Minimum = 1;
                        ProgressBar1.Value = 0;
                        backgroundWorker1.RunWorkerAsync(_InputParameter);
                    }

                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                //if (sfd.ShowDialog() == DialogResult.OK)
                //{
                //    if (ToggleSwitchEtat.Value == true)
                //    {
                //        _InputParameter.FileName2 = sfd.FileName2;
                //        _InputParameter.Stocklist2 = DataGridViewCouleursAujourdhui.DataSource as List<View_mouv_Aujourdhui>;
                //        ProgressBar2.Minimum = 1;
                //        ProgressBar2.Value = 0;
                //        backgroundWorker1.RunWorkerAsync(_InputParameter);
                //    }
                //    else
                //    {
                //        _InputParameter.FileName2 = sfd.FileName2;
                //        _InputParameter.Stocklist2 = DataGridViewCirculationAujourdhui.DataSource as List<View_mouv_Aujourdhui>;
                //        ProgressBar1.Minimum = 1;
                //        ProgressBar1.Value = 0;
                //        backgroundWorker1.RunWorkerAsync(_InputParameter);
                //    }

                //}
            }
        }

        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {

            DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            printer.Title = "RAPPORT DES MOUVEMENTS";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = System.Drawing.StringAlignment.Near;
            printer.Footer = "Entrepot de douane de type B Ihusi";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            if (ToggleSwitchEtat.Value == true)
            {
                printer.PrintDataGridView(DataGridViewCouleurs);
            }
            else
            {
                printer.PrintDataGridView(DataGridViewCirculation);
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            //    DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            //    printer.Title = "RAPPORT DES MOUVEMENTS";
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
            //        printer.PrintDataGridView(DataGridViewCouleursAujourdhui);
            //    }
            //    else
            //    {
            //        printer.PrintDataGridView(DataGridViewCirculationAujourdhui);
            //    }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            DataGridViewCouleurs.DataSource = table;
            DataGridViewCirculation.DataSource = table;
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis where Circulation.Type ='Entree'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            DataGridViewCouleurs.DataSource = table;
            DataGridViewCirculation.DataSource = table;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\Entrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis where Circulation.Type ='Sortie'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            DataGridViewCouleurs.DataSource = table;
            DataGridViewCirculation.DataSource = table;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque inner join Colis on Colis.Id_Colis = Circulation.Colis where convert(varchar(25),Circulation.Date,103) between '" + DateTimePickerdudate.Value.ToString("dd-MM-yyyy") + "' and  '" + DateTimeAudate.Value.ToString("dd-MM-yyyy") + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            DataGridViewCouleurs.DataSource = table;
            DataGridViewCirculation.DataSource = table;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\Entrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Circulation.Type , convert(varchar(25),Circulation.Date,103) as Date, Colis.Designation as Colis,Securite.Plaque as Entree, Declarant.Nom as Declarant, Importateur.Nom as Importateur from Circulation inner join Declarant on Declarant.Id_Declarant = Circulation.Declarant inner join Importateur on Importateur.Id_Importateur=Circulation.Importateur inner join Securite on Securite.Id_Securite = Circulation.Plaque  inner join Colis on Colis.Id_Colis = Circulation.Colis where CONCAT(Colis.Designation,  Securite.Plaque, Declarant.Nom , Importateur.Nom) like '%" + TextBoxRecherche.Text + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            System.Data.DataTable table = new System.Data.DataTable();
            adapter.Fill(table);

            DataGridViewCouleurs.DataSource = table;
            DataGridViewCirculation.DataSource = table;
        }

        private void DateTimeAudate_onValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void DateTimePickerdudate_onValueChanged(object sender, EventArgs e)
        {

        }
    }
}
