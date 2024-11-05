using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.PanneauPrincipal
{
    public partial class PanneauPrincipal : Form
    {
        private Chart.Dashboard model;
        public PanneauPrincipal()
        {
            InitializeComponent();
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();

            model = new Chart.Dashboard();
            LoadData();
        }

        
        private void LoadData()
        {
            Classes.Colis colis = new Classes.Colis();
            int entree = Convert.ToInt32(colis.ListDesColisTotalEntree().Rows.Count.ToString());
            int Sortie = Convert.ToInt32(colis.ListDesColisTotalSortie().Rows.Count.ToString());
            labelTotalEntreeCompte.Text = entree.ToString();
            labelTotalSortieComplet.Text = Sortie.ToString();
            labelTotalRestant.Text = Convert.ToInt32( entree-Sortie).ToString();
            var refreshData = model.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if (refreshData == true)
            {
                lblNumOrders.Text = model.TotalEntree.ToString() + " Colis";
                lblTotalRevenue.Text = model.TotalRevenue.ToString() + " Colis";
                //lblTotalProfit.Text = "$" + model.TotalProfit.ToString();
                //labelTotalEntreeCompte.Text = model.NumEntree.ToString();
                labelTotalColisEntree.Text = model.EntreeTotal.ToString() + " Paquet" ;
                labelPaquetRestant.Text = model.EntreeReste.ToString() + " Paquet";
                //labelTotalSortieComplet.Text = model.NumSortie.ToString();
                labelColisSortie.Text = model.TotalSortie.ToString() + " Paquet";
                labelTotalDeclarant.Text = model.TotalDeclarant.ToString();
                labelTotalImportateur.Text = model.TotalImportateur.ToString();
                //labelTotalRestant.Text = model.NumReste.ToString();
       
                chartGrossRevenue.DataSource = model.GrossRevenueList;
                chartGrossRevenue.Series[0].XValueMember = "Date";
                chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
               
                chartGrossRevenue.DataBind();

                chartEntreeRevenue.DataSource = model.GrossEntreeList;
                chartEntreeRevenue.Series[0].XValueMember = "DateEntree";
                chartEntreeRevenue.Series[0].YValueMembers = "TotalEntree";

                chartEntreeRevenue.DataBind();

                chartTopProducts.DataSource = model.TopProductsList;
                chartTopProducts.Series[0].XValueMember = "Key";
                chartTopProducts.Series[0].YValueMembers = "Value";
                chartTopProducts.DataBind();

                //dgvUnderstock.DataSource = model.UnderstockList;
                //dgvUnderstock.Columns[0].HeaderText = "Item";
                //dgvUnderstock.Columns[1].HeaderText = "Units";
                Console.WriteLine("Loaded view :)");
            }
            else Console.WriteLine("View not loaded, same query");
        }
        private void DisableCustomDates()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOkCustomDate.Visible = false;
        }

        Classes.Rapport rapport = new Classes.Rapport();
        public void changerform(object form)
        {
            if (Contenaire.Controls.Count > 0) Contenaire.Controls.Clear();

            Form fm = form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            Contenaire.Controls.Add(fm);
            Contenaire.Tag = fm;
            fm.Show();
        }
        public static int Restuction = 0;
        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            //if(Restuction==0)
            //{
            //    Restuction++;
            //    Securite.AddSecurite securite = new Securite.AddSecurite();
            //    securite.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Ce panneau est deja Ouvert!!!", "Securite", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            Securite.AddSecurite securite = new Securite.AddSecurite();
            securite.Show();
        }

        private void PanneauPrincipal_Load(object sender, EventArgs e)
        {
            //DataGridViewColumn IM = new DataGridViewColumn();
            //dataGridViewColis.DataSource = rapport.ColisDashboard();
            //DataGridViewImageColumn dgicc = new DataGridViewImageColumn();
            //dgicc = (DataGridViewImageColumn)dataGridViewColis.Columns[0];
            //dgicc.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //IM = (DataGridViewColumn)dataGridViewColis.Columns[3];
            //IM.Width = 180;
            //IniatialiserUsercontrole();
            

        }
       

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void bunifuCards2_MouseEnter(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.FromArgb(180,0,0,0);
        }

        private void bunifuCards2_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards2.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            Acteurs.Acteurs acteurs = new Acteurs.Acteurs();
            acteurs.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Colis.Marchandise marchandise = new Colis.Marchandise();
            marchandise.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Retrait.GererRetrait retrait = new Retrait.GererRetrait();
            retrait.Show();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            Rapport.FicheMagasin ficheMagasin = new Rapport.FicheMagasin();
            ficheMagasin.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            PanelDashebord dash = new PanelDashebord();
            LoadData();
            changerform(dash);
        }

        private void bunifuCards9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        //public void IniatialiserUsercontrole()
        //{
        //    flowLayoutData.Controls.Clear();
        //    DataTable table = rapport.ListImageRetrais();

        //    if (table != null)
        //    {
        //        if (table.Rows.Count > 0)
        //        {
        //            UserControlInitialiser[] listItems = new UserControlInitialiser[table.Rows.Count];
        //            for (int i = 0; i < 1; i++)
        //            {
        //                foreach (DataRow row in table.Rows)
        //                {
        //                    listItems[i] = new UserControlInitialiser();
        //                    listItems[i].labelDesignation.Text = row["Designation"].ToString();
        //                    listItems[i].labelNature.Text = row["Nature"].ToString();
        //                    listItems[i].labelQuantite.Text = row["Quantite"].ToString() + " Boites";
        //                    listItems[i].labelDate.Text = row["Date"].ToString();
        //                    //listItems[i].NUDQ.Value = Convert.ToInt32(row["Quantite"].ToString());
        //                    byte[] couverture = (byte[])row["Photo"];
        //                    MemoryStream ms = new MemoryStream(couverture);
        //                    byte[] image = ms.ToArray();
        //                    listItems[i].panelimage1.BackgroundImage = Image.FromStream(ms);
        //                    flowLayoutData.Controls.Add(listItems[i]);

        //                }

        //            }


        //        }
        //    }
        //}

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
           
        }

        private void dataGridViewColis_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
          
        }

        private void dataGridViewColis_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
           
        }

        private void dataGridViewColis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            BDD.Connecteur db = new BDD.Connecteur();
            

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select*from Controle ", db.getconnexion());
            adapter.SelectCommand = cmd;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {
                Login.Controle controle = new Login.Controle();
                controle.Show();
            }
            else
            {
                Utilisateur.GererUtilisateur gerer = new Utilisateur.GererUtilisateur();
                gerer.Show();
            }

          
        }

        private void PanneauPrincipal_Shown(object sender, EventArgs e)
        {
            this.Enabled = false;
            Login.Login_Form pn = new Login.Login_Form(this);
            pn.Show();
        }

        private void bunifuCards1_Click(object sender, EventArgs e)
        {
            Colis.ListColis list = new Colis.ListColis();
            list.Show();
        }

        private void bunifuCards2_Click(object sender, EventArgs e)
        {
            Retrait.ListRetrait list = new Retrait.ListRetrait();
            list.Show();
        }

        private void bunifuCards15_Click(object sender, EventArgs e)
        {
            Rapport.EtatStock etat = new Rapport.EtatStock();
            etat.Show();
        }

        private void bunifuCards3_Click(object sender, EventArgs e)
        {
            InfoDashboard.InfoDeclarant declarant = new InfoDashboard.InfoDeclarant();
            declarant.Show();
        }

        private void bunifuCards4_Click(object sender, EventArgs e)
        {
            InfoDashboard.InfoImportateur importateur = new InfoDashboard.InfoImportateur();
            importateur.Show();
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDates();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCustomDate_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOkCustomDate.Visible = true;
        }

        private void chartEntreeRevenue_Click(object sender, EventArgs e)
        {

        }

        private void chartGrossRevenue_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCards1_MouseEnter(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.FromArgb(180, 0, 0, 0);
        }

        private void bunifuCards1_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void bunifuCards15_MouseEnter(object sender, EventArgs e)
        {

            bunifuCards15.BackColor = Color.FromArgb(180, 0, 0, 0);
        }

        private void bunifuCards15_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards15.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void bunifuCards3_MouseEnter(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.FromArgb(180, 0, 0, 0);
        }

        private void bunifuCards3_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards3.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void bunifuCards4_MouseEnter(object sender, EventArgs e)
        {
            bunifuCards4.BackColor = Color.FromArgb(180, 0, 0, 0);
        }

        private void bunifuCards4_MouseLeave(object sender, EventArgs e)
        {
            bunifuCards4.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void btnOkCustomDate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
