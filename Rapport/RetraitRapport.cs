using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Rapport
{
    public partial class RetraitRapport : Form
    {
        public RetraitRapport()
        {
            InitializeComponent();
        }

        struct DataParameter
        {
            public List<View_Retrait> Stocklist;
            public string FileName { get; set; }
        }
        DataParameter _InputParameter;

        private void DataGridViewRetrait_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RetraitRapport_Load(object sender, EventArgs e)
        {
            using (MDLString db = new MDLString())
            {
                DataGridViewRapportBlanc.DataSource = db.View_Retrait.ToList();
                DataGridViewRapportCouleurs.DataSource = db.View_Retrait.ToList();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<View_Retrait> list = ((DataParameter)e.Argument).Stocklist;
            string Filename = ((DataParameter)e.Argument).FileName;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet Ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = false;
            int index = 1;
            int process = list.Count;
            Ws.Cells[1, 1] = "ID";
            Ws.Cells[1, 2] = "Designation";
            Ws.Cells[1, 3] = "P.D'entree";
            Ws.Cells[1, 4] = "Nature";
            Ws.Cells[1, 5] = "Quantite";
            Ws.Cells[1, 6] = "Nom";
            Ws.Cells[1, 7] = "P.De Sortie";
            Ws.Cells[1, 8] = "Nom du Chauffeur";
            Ws.Cells[1, 9] = "Num du Chauffeur";
            Ws.Cells[1, 10] = "Date";
            foreach (View_Retrait sr in list)
            {
                if (!backgroundWorker.CancellationPending)
                {
                    backgroundWorker.ReportProgress(index++ * 100 / process);
                    Ws.Cells[index, 1] = sr.ID.ToString();
                    Ws.Cells[index, 2] = sr.Designation.ToString();
                    Ws.Cells[index, 3] = sr.Plaque.ToString();
                    Ws.Cells[index, 4] = sr.Nature.ToString();
                    Ws.Cells[index, 5] = sr.Quantite.ToString();
                    Ws.Cells[index, 6] = sr.Nom.ToString();
                    Ws.Cells[index, 7] = sr.Sortie.ToString();
                    Ws.Cells[index, 8] = sr.Nom_Chauffeur.ToString();
                    Ws.Cells[index, 9] = sr.Num_Chauffeur.ToString();
                    Ws.Cells[index, 10] = sr.Date.ToString();

                }
            }
            Ws.SaveAs(Filename, XlFileFormat.xlWorkbookDefault, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            excel.Quit();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
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

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        private void ToggleSwitchEtat_OnValuechange(object sender, EventArgs e)
        {
            if (ToggleSwitchEtat.Value == true)
            {
                PanelListCouleur.BringToFront();
            }
            else
            {
                PanelListeBlanc.BringToFront();
            }
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
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
                        _InputParameter.Stocklist = DataGridViewRapportCouleurs.DataSource as List<View_Retrait>;
                        ProgressBar2.Minimum = 1;
                        ProgressBar2.Value = 0;
                        backgroundWorker.RunWorkerAsync(_InputParameter);
                    }
                    else
                    {
                        _InputParameter.FileName = sfd.FileName;
                        _InputParameter.Stocklist = DataGridViewRapportBlanc.DataSource as List<View_Retrait>;
                        ProgressBar1.Minimum = 1;
                        ProgressBar1.Value = 0;
                        backgroundWorker.RunWorkerAsync(_InputParameter);
                    }

                }
            }
        }

        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {
            DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            printer.Title = "RAPPORT DE RETRAIT";
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
                printer.PrintDataGridView(DataGridViewRapportCouleurs);
            }
            else
            {
                printer.PrintDataGridView(DataGridViewRapportBlanc);
            }
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {

            try
            {
                DataGridViewRapportCouleurs.FirstDisplayedScrollingRowIndex = DataGridViewRapportCouleurs.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }

        }

        private void DataGridViewRapportCouleurs_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewRapportCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewRapportCouleurs_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewRapportCouleurs.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }
    }
}
