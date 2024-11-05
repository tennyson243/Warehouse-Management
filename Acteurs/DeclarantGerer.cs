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

namespace Gestion_Entrepot.Acteurs
{
    public partial class DeclarantGerer : Form
    {
        public DeclarantGerer()
        {
            InitializeComponent();
        }

        Classes.Declarant declarant = new Classes.Declarant();
        private void DeclarantGerer_Load(object sender, EventArgs e)
        {
            DataGridViewImportateur.RowTemplate.Height = 80;
            DataGridViewColumn IM = new DataGridViewColumn();
            DataGridViewImportateur.DataSource = declarant.listDeclarant();
            IM = (DataGridViewColumn)DataGridViewImportateur.Columns[0];
            IM.Width = 60;
           
        }

        private void ButtonParcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxDeclarant.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);

            DataTable table = declarant.getDeclarantbyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxIDEclarantModifier.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            find(TextBoxRechercher.Text);
        }

        private void DataGridViewImportateur_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = Convert.ToInt32(DataGridViewImportateur.CurrentRow.Cells[0].Value);
            DetailsImportateur details = new DetailsImportateur(id);
            details.Show();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            
        }

        public void Actualiser()
        {
            TextBoxNom.Text = "";
            TextBoxAdresse.Text = "";
            TextBoxPays.Text = "";
            TextBoxVille.Text = "";
            TextBoxTelephone.Text = "";
            PictureBoxDeclarant.Image = Image.FromFile("../../Resources/african-american-3177784_1920.jpg");
        }

        public void Initialiserdonnee(DataTable data)
        {


            TextBoxid.Text = data.Rows[0][0].ToString();
            TextBoxNomModifier.Text = data.Rows[0][1].ToString();
            TextBoxAdresseModifer.Text = data.Rows[0][2].ToString();
            TextBoxVilleModifier.Text = data.Rows[0][3].ToString();
            TextBoxPaysModifier.Text = data.Rows[0][4].ToString();
            TextBoxTelephoneModifier.Text = data.Rows[0][5].ToString();

            try
            {
                byte[] couverture = (byte[])data.Rows[0][6];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxIDEclarantModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }

        }
        public void find(string value)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("select Id_DEclarant as Id, Nom, Adresse,Pays, Ville,Telephone, Photo from Declarant  where CONCAT(Nom,  Pays, Ville) like '%" + value + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewImportateur.DataSource = table;

        }

        private void ButtonAddImpo_Click(object sender, EventArgs e)
        {

            PanelAjouter.BringToFront();
            PanelAjouter.BackColor = Color.FromArgb(186,228, 229);
        }

        private void ButtonModifyimpo_Click(object sender, EventArgs e)
        {
            PanelModify.BringToFront();
            panelmenu.BackColor = Color.FromArgb(78, 205, 196);
        }

        private void ButtonListImpo_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
            panelmenu.BackColor = Color.FromArgb(20, 205, 200);
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxIDEclarantModifier.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void PictureBoxIDEclarantModifier_Click(object sender, EventArgs e)
        {

        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewImportateur.FirstDisplayedScrollingRowIndex = DataGridViewImportateur.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewImportateur_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewImportateur.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewImportateur_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewImportateur.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void ButtonAddImpo_Click_1(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void ButtonModifyimpo_Click_1(object sender, EventArgs e)
        {
            PanelModify.BringToFront();
        }

        private void ButtonListImpo_Click_1(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);
            string nom = TextBoxNomModifier.Text;
            string adresse = TextBoxAdresseModifer.Text;
            string ville = TextBoxVilleModifier.Text;
            string pays = TextBoxPaysModifier.Text;
            string telephone = TextBoxTelephoneModifier.Text;

            MemoryStream ms = new MemoryStream();
            PictureBoxIDEclarantModifier.Image.Save(ms, PictureBoxIDEclarantModifier.Image.RawFormat);
            Byte[] image = ms.ToArray();

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom ne dois pas etre Vide", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (adresse.Trim().Equals(""))
            {
                MessageBox.Show("L'Adresse ne dois pas etre Vide", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ville.Trim().Equals(""))
            {
                MessageBox.Show("Le nom de la ville ne dois pas etre Vide", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pays.Trim().Equals(""))
            {
                MessageBox.Show("Le nom du pays ne dois pas etre Vide", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (telephone.Trim().Equals(""))
            {
                MessageBox.Show("Le numero de telephone ne dois pas etre Vide", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (declarant.ModifierDeclarant(id, nom, adresse, pays, ville, telephone, image))
                {
                    MessageBox.Show("Importateur Modifier", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Actualiser();
                    PanelList.BringToFront();
                    DataGridViewImportateur.DataSource = declarant.listDeclarant();

                }
                else
                {
                    MessageBox.Show("Echec de modification", "Modification Importateur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxid.Text);
                if (MessageBox.Show("Vouslez vous vraiment supprimer cet Importateur", "Suppression Importateur", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (declarant.EffaceDeclarant(id))
                    {
                        MessageBox.Show("IMPORTATEUR Supprimer", "Suppression Importateur", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppression", "Suppression Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalide ID");
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            PanelModify.BringToFront();
            panelmenu.BackColor = Color.RosyBrown;

            int id = Convert.ToInt32(DataGridViewImportateur.CurrentRow.Cells[0].Value);

            DataTable table = declarant.getDeclarantbyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataGridViewImportateur.CurrentRow.Cells[0].Value);
                if (MessageBox.Show("Vouslez vous vraiment supprimer cet Importateur", "Suppression Importateur", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (declarant.EffaceDeclarant(id))
                    {
                        MessageBox.Show("IMPORTATEUR Supprimer", "Suppression Importateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppression", "Suppression Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalide ID");
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            string nom = TextBoxNom.Text;
            string adresse = TextBoxAdresse.Text;
            string ville = TextBoxVille.Text;
            string pays = TextBoxPays.Text;
            string telephone = TextBoxTelephone.Text;

            MemoryStream ms = new MemoryStream();
            PictureBoxDeclarant.Image.Save(ms, PictureBoxDeclarant.Image.RawFormat);
            Byte[] image = ms.ToArray();

            if (nom.Trim().Equals(""))
            {
                MessageBox.Show("Le nom ne dois pas etre Vide", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (adresse.Trim().Equals(""))
            {
                MessageBox.Show("L'Adresse ne dois pas etre Vide", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (ville.Trim().Equals(""))
            {
                MessageBox.Show("Le nom de la ville ne dois pas etre Vide", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pays.Trim().Equals(""))
            {
                MessageBox.Show("Le nom du pays ne dois pas etre Vide", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (telephone.Trim().Equals(""))
            {
                MessageBox.Show("Le numero de telephone ne dois pas etre Vide", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (declarant.AjouterDeclarant(nom, adresse, pays, ville, telephone, image))
                {
                    MessageBox.Show("Importateur Ajouter", "Ajout Importateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Actualiser();
                }
                else
                {
                    MessageBox.Show("Echec de modification", "Modification Voiture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxNom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxAdresse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxVille_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxPays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !(e.KeyChar == 43))
            {
                e.Handled = true;
            }
        }

        private void TextBoxAdresseModifer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxNomModifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxVilleModifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxPaysModifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxTelephoneModifier_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !(e.KeyChar == 43))
            {
                e.Handled = true;
            }
        }
    }
}
