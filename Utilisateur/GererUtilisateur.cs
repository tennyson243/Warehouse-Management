using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Utilisateur
{
    public partial class GererUtilisateur : Form
    {
        public GererUtilisateur()
        {
            InitializeComponent();
        }

        Classes.Utilisateur utilisateur = new Classes.Utilisateur();
        Classes.Controle controle = new Classes.Controle();
        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewUtilisateur.FirstDisplayedScrollingRowIndex = DataGridViewUtilisateur.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewUtilisateur_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewUtilisateur.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewUtilisateur_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
             try
            {
                bunifuVScrollBar1.Maximum = DataGridViewUtilisateur.RowCount - 1;
            }
            catch (Exception)
            {

                    
            }
            
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            string nom = textBoxnom.Text;
            string post = textBoxpostnom.Text;
            string nomuti = textBoxnomutilisateur.Text;
            string motdepasse = textBoxmotdepasse.Text;

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Les Nom n'est peux pas etre vide", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (post.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom n'est peux pas etre vide", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nomuti.Trim().Equals(""))
            {
                MessageBox.Show("Le Nom d'Utilisateur n'est peux pas etre vide", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (motdepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le mot de passe n'est peux pas etre vide", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (utilisateur.AjouterUser(nom, post, nomuti, motdepasse))
                {
                    MessageBox.Show("Utilisateur Ajouter", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
                    rafrechir();
                }
                else
                {
                    MessageBox.Show("Echec d'ajout", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void rafrechir()
        {
            textBoxid.Text = "";
            textBoxnom.Text = "";
            textBoxmotdepasse.Text = "";
            textBoxpostnom.Text = "";
            textBoxnomutilisateur.Text = "";

            textBoxid.Text = "";
            textBoxnomModifier.Text = "";
            textBoxmotdepasseModifier.Text = "";
            textBoxpostnomModifier.Text = "";
            textBoxnomutilisateurModifier.Text = "";
            TextBoxCodeSecret.Text = "";
            TextBoxIdControle.Text = "";
        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewUtilisateur.CurrentRow.Cells[0].Value);
            DataTable table = utilisateur.GetUtilisateurbyid(id);

            if(table.Rows.Count>0)
            {
                textBoxid.Text = table.Rows[0][0].ToString();
                textBoxnomModifier.Text = table.Rows[0][1].ToString();
                textBoxpostnomModifier.Text = table.Rows[0][2].ToString();
                textBoxnomutilisateurModifier.Text = table.Rows[0][3].ToString();
                textBoxmotdepasseModifier.Text = table.Rows[0][4].ToString();
            }

            PanelModifier.BringToFront();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataGridViewUtilisateur.CurrentRow.Cells[0].Value);

                if (MessageBox.Show("Etez-Vous sure de vouloir vraiment supprimer cet Utilisateur?", "Suppression Utilisateur", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (utilisateur.SupprimerUtilisateur(id))
                    {
                        MessageBox.Show("Utilisateur Supprimer", "Suppressions Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
          
                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppressions", "Suppression Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxid.Text);

            string nom = textBoxnomModifier.Text;
            string post = textBoxpostnomModifier.Text;
            string nomuti = textBoxnomutilisateurModifier.Text;
            string motdepasse = textBoxmotdepasseModifier.Text;

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Les Nom n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (post.Trim().Equals(""))
            {
                MessageBox.Show("Le postnom n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nomuti.Trim().Equals(""))
            {
                MessageBox.Show("Le Nom d'Utilisateur n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (motdepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le mot de passe n'est peux pas etre vide", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (utilisateur.ModifierUtilisateur(id, nom, post, nomuti, motdepasse))
                {
                    MessageBox.Show("Utilisateur Modifier", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
                    rafrechir();
                }
                else
                {
                    MessageBox.Show("Echec de modification", "Modification Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void ButtonList_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void GererUtilisateur_Load(object sender, EventArgs e)
        {
            DataGridViewUtilisateur.DataSource = utilisateur.listUtilisateur();
        }

        private void PanelCodeSecret_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PanelAjouter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxid.Text);

                string motdepasse = TextBoxCodeSecret.Text;

                if (motdepasse.Trim().Equals(""))
                {
                    MessageBox.Show("Le code secret n'est peux pas etre vide", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (controle.ModifierControle(id, motdepasse))
                    {
                        MessageBox.Show("Code Secret Modifier", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewCode.DataSource = controle.listControle();
                        rafrechir();
                    }
                    else
                    {
                        MessageBox.Show("Echec de Modification", "Modification Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
     

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            string motdepasse = TextBoxCodeSecret.Text;

             if (motdepasse.Trim().Equals(""))
            {
                MessageBox.Show("Le code secret n'est peux pas etre vide", "Ajout Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (controle.AjouterControle(motdepasse))
                {
                    MessageBox.Show("Code Secret Ajouter", "Ajout Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridViewCode.DataSource = controle.listControle();
                    rafrechir();
                }
                else
                {
                    MessageBox.Show("Echec d'ajout", "Ajout Utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewCode.FirstDisplayedScrollingRowIndex = DataGridViewCode.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCode_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewCode.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCode_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewCode.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxid.Text = Convert.ToInt32(DataGridViewCode.CurrentRow.Cells[0].Value).ToString();
            TextBoxCodeSecret.Text = DataGridViewCode.CurrentRow.Cells[1].Value.ToString();
        }

        private void PanelList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxIdControle.Text);

                if (MessageBox.Show("Etez-Vous sure de vouloir vraiment supprimer ce Code Secret?", "Suppression Code Secret", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (controle.SupprimerControle(id))
                    {
                        MessageBox.Show("Code Secret Supprimer", "Suppressions Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewCode.DataSource = controle.listControle();
                        rafrechir();

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppressions", "Suppression Code Secret", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Veiller choisir l'Utilisateur a modifier dans le tableau" + ex.Message, "INVALIDE ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            PanelCodeSecret.BringToFront();
        }
    }
}
