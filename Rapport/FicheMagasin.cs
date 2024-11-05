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
    public partial class FicheMagasin : Form
    {
        public FicheMagasin()
        {
            InitializeComponent();
        }

        Classes.Rapport rapport = new Classes.Rapport();
        private void FicheMagasin_Load(object sender, EventArgs e)
        {
            Classes.Magasinage magasinage = new Classes.Magasinage();
          

          
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Print.PrintFicheMagasin pr = new Print.PrintFicheMagasin();
            pr.Show();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
          

        }

        private void ButtonModify_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Rapport.FicheJournalierMagasin magasin = new FicheJournalierMagasin();
            magasin.Show();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Rapport.Stock_Magasin magasin = new Stock_Magasin();
            magasin.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            Rapport.Statistique magasin = new Statistique();
            magasin.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Rapport.Mouvement mv = new Rapport.Mouvement();
            mv.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Rapport.EtatStock etat = new EtatStock();
            etat.Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Retrait.ListRetrait list = new Retrait.ListRetrait();
            list.Show();
        }
    }
}
