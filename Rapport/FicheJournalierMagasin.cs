using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Rapport
{
    public partial class FicheJournalierMagasin : Form
    {
        public FicheJournalierMagasin()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        Classes.Magasinage magasinage = new Classes.Magasinage();
        private void FicheJournalierMagasin_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Stock Initial", typeof(int));
            table.Columns.Add("Entree", typeof(int));
            table.Columns.Add("Sortie", typeof(int));
            table.Columns.Add("Stock Actuelle", typeof(int));


            Gridrapport.DataSource = table;
            //DataGridViewColumn dgic = new DataGridViewColumn();
            //dgic = (DataGridViewColumn)Gridrapport.Columns[0];
            //dgic.Width = 100;
            //DataGridViewColumn dgi = new DataGridViewColumn();
            //dgi = (DataGridViewColumn)Gridrapport.Columns[2];
            //dgi.Width = 80;
            data();


        }

        public void data()
        {
            int Restant = Convert.ToInt32(magasinage.ResteColis().Rows.Count);
            int Entree = Convert.ToInt32(magasinage.EntreeJournalier().Rows.Count);
            int Sortie = Convert.ToInt32(magasinage.SortieJournalier().Rows.Count);
            int StkIni = Restant - Entree + Sortie;
            table.Rows.Add(StkIni, Entree, Sortie, Restant);
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                Gridrapport.FirstDisplayedScrollingRowIndex = Gridrapport.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void Gridrapport_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            try
            {
                bunifuVScrollBar1.Maximum = Gridrapport.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void Gridrapport_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = Gridrapport.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }
    }
}
