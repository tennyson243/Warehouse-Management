using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Rapport
{
    public partial class EffetPersonnelle : Form
    {
        string Designation;
        public EffetPersonnelle(string Des)
        {
            InitializeComponent();
            this.Designation = Des;
        }


        public PrintDocument PD = new PrintDocument();
        public PrintPreviewDialog PPD = new PrintPreviewDialog();
        int longpaper;
        Classes.Retrait retrait = new Classes.Retrait();

        public void changerlongeurPapier()
        {
            int rowcount;
            longpaper = 0;
            rowcount = GridBonEffetPersonnelle.Rows.Count;
            longpaper = rowcount * 55;
            longpaper = longpaper + 300;
        }


        private void EffetPersonnelle_Load(object sender, EventArgs e)
        {
            DataTable table = retrait.EffetPersonnelle(Designation);
            labelDeclarant.Text = table.Rows[0][6].ToString();
            labelNomChaffeur.Text = table.Rows[0][8].ToString();
            labelPlaquedeSortie.Text = table.Rows[0][7].ToString();
            labelPlaqueDentree.Text = table.Rows[0][3].ToString();
            labelNumChauffeur.Text = table.Rows[0][9].ToString();
            LabelDate.Text = table.Rows[0][10].ToString();

            GridBonEffetPersonnelle.DataSource = retrait.EffetPersonnelleTable(Designation);

            LabelTotal.Text = (from DataGridViewRow row in GridBonEffetPersonnelle.Rows
                               where row.Cells[2].FormattedValue.ToString() != string.Empty
                               select Convert.ToInt32(row.Cells[2].FormattedValue)).Sum().ToString();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            //Print.PrintEffetPersonnel personnel = new Print.PrintEffetPersonnel();
            //personnel.Show();
        }

        private void print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panelprint = (Guna.UI2.WinForms.Guna2ShadowPanel)pnl;
            getprintarea(pnl);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage_1);
            changerlongeurPapier();
            printPreviewDialog1.ShowDialog();
        }


        //private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    Bitmap bm = new Bitmap(this.panelprint.Width, this.panelprint.Height);
        //    panelprint.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.panelprint.Width, this.panelprint.Height));
        //    e.Graphics.DrawImage(bm, 0, 0);

        //    
        //}

        private Bitmap memoryImage;

        private void getprintarea(Panel pnl)
        {
            memoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));

        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            Font f8 = new Font("Calibri", 8, FontStyle.Regular);
            Font f10 = new Font("Calibri", 10, FontStyle.Regular);
            Font f10b = new Font("Calibri", 10, FontStyle.Bold);
            Font f14 = new Font("Calibri", 14, FontStyle.Bold);
            Font f16 = new Font("Calibri", 16, FontStyle.Bold);

            Bitmap bm = new Bitmap(this.panelprint.Width, this.panelprint.Height);
            panelprint.DrawToBitmap(bm, new System.Drawing.Rectangle(0, 0, this.panelprint.Width, this.panelprint.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            Rectangle pagearea = e.PageBounds;
            //e.Graphics.DrawImage(memoryImage, (pagearea.Width / 2) - (this.panelprint.Width / 2), this.panelprint.Location.Y);

            int LeftMarg = printDocument1.DefaultPageSettings.Margins.Left;
            int CenterMarg = printDocument1.DefaultPageSettings.PaperSize.Width / 2;
            int RightMarg = printDocument1.DefaultPageSettings.PaperSize.Width;

            StringFormat right = new StringFormat();
            StringFormat center = new StringFormat();
            StringFormat left = new StringFormat();
            right.Alignment = StringAlignment.Far;
            center.Alignment = StringAlignment.Center;
            left.Alignment = StringAlignment.Near;

            //string line;
            //line = "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
            //e.Graphics.DrawString("Ma Pharmacie", f14, Brushes.Black, LeftMarg, 5, left);
            //e.Graphics.DrawString("Goma Av des orchidee N27", f10, Brushes.Black, CenterMarg, 25, center);
            //e.Graphics.DrawString("Telephone +243 975316498 ", f8, Brushes.Black, CenterMarg, 40, center);
            //e.Graphics.DrawString("NOUS VOUS OFFRONS L'EXCELLENCE A GOMA ", f16, Brushes.Black, CenterMarg, 60, center);

        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            PageSettings pagesetup = new PageSettings();
            pagesetup.PaperSize = new PaperSize("custom", 458, 520);
            printDocument1.DefaultPageSettings = pagesetup;
        }

        private void panelprint_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
