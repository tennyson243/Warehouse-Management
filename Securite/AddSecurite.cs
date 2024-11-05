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

namespace Gestion_Entrepot.Securite
{
    public partial class AddSecurite : Form
    {
        public AddSecurite()
        {
            InitializeComponent();
        }

        Classes.Securites securite = new Classes.Securites();
        Classes.Check check = new Classes.Check();
        private void AddSecurite_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);

            DataGridViewSecurite.RowTemplate.Height = 80;
            DataGridViewListe.RowTemplate.Height = 90;
            DataGridViewSecurite.DataSource = securite.listdelasecurite();
            DataGridViewColumn ida = new DataGridViewColumn();
            ida = (DataGridViewColumn)DataGridViewSecurite.Columns[0];
            ida.Visible = false;

            DataGridViewListe.DataSource = securite.listsecurite();

            DataGridViewColumn idaq = new DataGridViewColumn();
            idaq = (DataGridViewColumn)DataGridViewListe.Columns[0];
            idaq.Visible = false;

            DataGridViewCheck.DataSource = check.listCheck();
            DataGridViewImageColumn dgic = new DataGridViewImageColumn();
            dgic = (DataGridViewImageColumn)DataGridViewCheck.Columns[8];
            dgic.ImageLayout = DataGridViewImageCellLayout.Normal;
            TextBoxId.Hide();

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonParcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if(OPf.ShowDialog()==DialogResult.OK)
            {
                PictureBoxSecurite.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void ButtonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                string plaque = TextBoxPlaque.Text;
                string type = ComboBoxType.Text;
                DateTime entree = DatepickerDateEntree.Value;
                DateTime sortie = DatepickerDateEntree.Value;
                MemoryStream ms = new MemoryStream();
                string statut = "Parking";
                string Chauffeur = TextBoxNomChauffeur.Text;

                PictureBoxSecurite.Image.Save(ms, PictureBoxSecurite.Image.RawFormat);
                Byte[] image = ms.ToArray();

                if (plaque.Trim().Equals(""))
                {
                    MessageBox.Show("La plaque ne dois pas etre Vide", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (securite.AjouterSecurite(plaque, type, entree, sortie, statut, image,Chauffeur))
                    {
                        MessageBox.Show("Voiture Enregistrer", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Actualiser();
                    }
                    else
                    {
                        MessageBox.Show("Echec d'enregistrement", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show("Cette Plaque de Voiture Existe deja dans la base de donnee " + ex, "Duplication de valeur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }

        public void Actualiser()
        {
            TextBoxPlaque.Text = "";
            TextBoxNomChauffeur.Text = "";
            //DatepickerDateEntree.Value = DateTime.Now;
            PictureBoxSecurite.Image = Image.FromFile("../../Resources/truck-306852_1280.png");
            DataGridViewSecurite.DataSource = securite.listdelasecurite();
            

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void ButtonModify_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void DataGridViewSecurite_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_voiture = Convert.ToInt32( DataGridViewSecurite.CurrentRow.Cells[0].Value.ToString());

            DataTable Voituredata = securite.getSecuriteparid(Id_voiture);

            TextBoxId.Text = Voituredata.Rows[0][0].ToString();
            TextBoxPlaqueModifier.Text = Voituredata.Rows[0][1].ToString();
            ComboBoxTypeModifier.Text = Voituredata.Rows[0][2].ToString();
            DatepickerDateEntreeModifier.Value = (DateTime)Voituredata.Rows[0][3];
            DatepickerDateSortieModifier.Value = (DateTime)Voituredata.Rows[0][4];
            ComboBoxStatut.Text = Voituredata.Rows[0][5].ToString();


            //Initialiser la couvertur du livre

            try
            {
                byte[] couverture = (byte[])Voituredata.Rows[0][6];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxSecuriteModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }


        }

        private void ButtonParcourirModifier_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxSecuriteModifier.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            
           
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void find(string value)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\Entrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("select Id_Securite as Id, Plaque, Type, Date_Entree, Date_Sortie, Statut, Photo from securite  where CONCAT(Plaque,  Statut) like '%" + value + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewListe.DataSource = table;

        }

        private void ButtonList_Click(object sender, EventArgs e)
        {
            PanelListe.BringToFront();
        }

        private void TextBoxRechercher_TextChanged_1(object sender, EventArgs e)
        {
            find(TextBoxRechercher.Text);
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("select Id_Securite as Id, Plaque, Type, Date_Entree, Date_Sortie, Statut, Photo from securite  where Date_Entree between  '" + DateTimePickerdudate.Value.ToString("yyyy-MM-dd") + "' and  '" + DateTimeAudate.Value.ToString("yyyy-MM-dd") + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewListe.DataSource = table;
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewListe.FirstDisplayedScrollingRowIndex = DataGridViewListe.Rows[e.Value].Index;
            }
            catch (Exception)
            {

                
            } 
        }

        private void DataGridViewListe_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewListe.RowCount - 1;
            }
            catch (Exception)
            {

               
            }
            
        }

        private void DataGridViewListe_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewListe.RowCount - 1;
            }
            catch (Exception)
            {

                    
            }
            
        }
        public void data2()
        {
            //var db = BDD.Connecteur connexion();
            //IEnumerable<Vue_Securite> se
        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewSecurite.FirstDisplayedScrollingRowIndex = DataGridViewListe.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewSecurite_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewSecurite.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewSecurite_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewSecurite.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void ButtonAdd_Click_1(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void ButtonModify_Click_1(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();

        }

        private void ButtonList_Click_1(object sender, EventArgs e)
        {
            PanelListe.BringToFront();
        }

        private void DataGridViewListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string plaque = TextBoxPlaque.Text;
                string type = ComboBoxType.Text;
                DateTime entree = DatepickerDateEntree.Value;
                DateTime sortie = DatepickerDateEntree.Value;
                MemoryStream ms = new MemoryStream();
                string statut = "Parking";
                string Chauffeur = TextBoxNomChauffeur.Text;

                PictureBoxSecurite.Image.Save(ms, PictureBoxSecurite.Image.RawFormat);
                Byte[] image = ms.ToArray();

                if (plaque.Trim().Equals(""))
                {
                    MessageBox.Show("La plaque ne dois pas etre Vide", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(Chauffeur.Trim().Equals(""))
                {
                    MessageBox.Show("Le Nom du Chauffeur ne dois pas etre Vide", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (securite.AjouterSecurite(plaque, type, entree, sortie, statut, image, Chauffeur))
                    {
                        MessageBox.Show("Voiture Enregistrer", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Actualiser();
                    }
                    else
                    {
                        MessageBox.Show("Echec d'enregistrement", "Enregistrement Voiture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cette Plaque de Voiture Existe deja dans la base de donnee " + ex, "Duplication de valeur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DataGridViewListe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridViewSecurite_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_voiture = Convert.ToInt32(DataGridViewSecurite.CurrentRow.Cells[0].Value.ToString());

            DataTable Voituredata = securite.getSecuriteparid(Id_voiture);

            TextBoxId.Text = Voituredata.Rows[0][0].ToString();
            TextBoxPlaqueModifier.Text = Voituredata.Rows[0][1].ToString();
            ComboBoxTypeModifier.Text = Voituredata.Rows[0][2].ToString();
            DatepickerDateEntreeModifier.Value = (DateTime)Voituredata.Rows[0][3];
            DatepickerDateSortieModifier.Value = (DateTime)Voituredata.Rows[0][4];
            ComboBoxStatut.Text = Voituredata.Rows[0][5].ToString();
            TextBoxNomModifier.Text = Voituredata.Rows[0][7].ToString();

            try
            {
                byte[] couverture = (byte[])Voituredata.Rows[0][6];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxSecuriteModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }
        }

        private void DataGridViewListe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_voiture = Convert.ToInt32(DataGridViewListe.CurrentRow.Cells[0].Value.ToString());

            DataTable Voituredata = securite.getSecuriteparid(Id_voiture);

            TextBoxId.Text = Voituredata.Rows[0][0].ToString();
            TextBoxPlaqueModifier.Text = Voituredata.Rows[0][1].ToString();
            ComboBoxTypeModifier.Text = Voituredata.Rows[0][2].ToString();
            DatepickerDateEntreeModifier.Value = (DateTime)Voituredata.Rows[0][3];
            DatepickerDateSortieModifier.Value = (DateTime)Voituredata.Rows[0][4];
            ComboBoxStatut.Text = Voituredata.Rows[0][5].ToString();
            TextBoxNomModifier.Text = Voituredata.Rows[0][7].ToString();


            //Initialiser la couvertur du livre

            try
            {
                byte[] couverture = (byte[])Voituredata.Rows[0][6];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxSecuriteModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }

            PanelModifier.BringToFront();
        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxId.Text);
                string plaque = TextBoxPlaqueModifier.Text;
                string type = ComboBoxTypeModifier.Text;
                DateTime entree = DatepickerDateEntreeModifier.Value;
                DateTime sortie = DatepickerDateSortieModifier.Value;
                MemoryStream ms = new MemoryStream();
                string statut = ComboBoxStatut.Text;
                string Chauffeur = TextBoxNomModifier.Text;

                PictureBoxSecuriteModifier.Image.Save(ms, PictureBoxSecurite.Image.RawFormat);
                Byte[] image = ms.ToArray();

                if (plaque.Trim().Equals(""))
                {
                    MessageBox.Show("La plaque ne dois pas etre Vide", "Modification Voiture", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (securite.ModifierSecurite(id, plaque, type, entree, sortie, statut, image, Chauffeur))
                    {
                        MessageBox.Show("Voiture Modifier", "Modification Voiture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Actualiser();
                    }
                    else
                    {
                        MessageBox.Show("Echec de modification", "Modification Voiture", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalide ID");
            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TextBoxId.Text);
                if (MessageBox.Show("Vouslez vous vraiment supprimer cette enregistrement", "Suppression Voiture", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (securite.EffaceSecurite(id))
                    {
                        MessageBox.Show("ENREGISTREMENT Supprimer", "Suppression ENREGISTREMENT", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppression", "Suppression ENREGITREMENT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalide ID");
            }
        }

        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {
            PanelCheck.BringToFront();
            DataGridViewCheck.DataSource = check.listCheck();
        }

        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            Securite.Check check = new Check();
            check.Show();
        }

        private void bunifuVScrollBar3_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewCheck.FirstDisplayedScrollingRowIndex = DataGridViewCheck.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCheck_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar3.Maximum = DataGridViewCheck.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewCheck_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar3.Maximum = DataGridViewCheck.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            PanneauPrincipal.PanneauPrincipal principal = new PanneauPrincipal.PanneauPrincipal();
         
        }
    }
}
