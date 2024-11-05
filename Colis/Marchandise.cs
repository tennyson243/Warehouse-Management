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

namespace Gestion_Entrepot.Colis
{
    public partial class Marchandise : Form
    {
        public Marchandise()
        { InitializeComponent();
        }

        BDD.Connecteur connecteur = new BDD.Connecteur();
        DataTable table = new DataTable();
        Classes.Colis marchandise = new Classes.Colis();
        Classes.Declarant declarant = new Classes.Declarant();
        Classes.Importateur importateur = new Classes.Importateur();
        Classes.Securites securites = new Classes.Securites();
        Classes.Magasinage magasinage = new Classes.Magasinage();
        Classes.Circulation circulation = new Classes.Circulation();
        
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (TextBoxNature.Text.Trim().Equals(""))
            {
                MessageBox.Show("Veiller Specifier le Nom ou La Nature de L'element a Ajouter dans Le Panier S'il vous Plait", "Ajouter Au Panier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TextBoxNature.BorderColor = Color.Red;
                TextBoxNature.BorderThickness = 3;
            }
            else if (NumericUpDownQuantite.Value == 0)
            {
                MessageBox.Show("Vous ne Pouvez pas Ajouter Une Quantite inferieur ou Egale a Zero dans le panier", "Ajouter Au Panier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
               
            }
            else if (RadioButtonDiversMses.Checked == false && RadioButtonHomogene.Checked==false && RadioButtonMagerwa.Checked==false)
            {
                MessageBox.Show("Veiller Specifier le Type de la Marchandise S'il vous plait", "Ajouter Au Panier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                
            }
            else
            {
                string TypeMarchandise = "";
                if (RadioButtonDiversMses.Checked == true)
                {
                    TypeMarchandise = "Div.Mses";
                }
                else if (RadioButtonHomogene.Checked == true)
                {
                    TypeMarchandise = "Homogene";
                }
                else
                {
                    TypeMarchandise = "Magerwa";
                }
                MemoryStream ms = new MemoryStream();
                PictureBoxColisAjouter.Image.Save(ms, PictureBoxColisAjouter.Image.RawFormat);
                Byte[] image = ms.ToArray();

                DataGridViewPanier.Rows.Add(TextBoxNature.Text, NumericUpDownQuantite.Value, image, TypeMarchandise);
                TextBoxNature.Text = "";
                PictureBoxColisAjouter.Image = Image.FromFile("../../Resources/man-3022704_19201.jpg");
                NumericUpDownQuantite.Value = 0;
                RadioButtonDiversMses.Checked = false;
                RadioButtonHomogene.Checked = false;
                RadioButtonMagerwa.Checked = false;
            }
           


        }
        private void Marchandise_Load(object sender, EventArgs e)
        {
            panier();
            data();
            AutoTexte();
            InitialiseCombo();
            DataGridViewList.RowTemplate.Height = 80;
            DataGridViewColisModifier.DataSource = marchandise.listcolisJointureModifier();
            DataGridViewList.DataSource = marchandise.listcolisJointure();
       

        }
        public void panier()
        {
            DataGridViewTextBoxColumn na = new DataGridViewTextBoxColumn();
            na.HeaderText = "Nature";
            DataGridViewTextBoxColumn Qa = new DataGridViewTextBoxColumn();
            Qa.HeaderText = "Quantite";
            //DataGridViewPanier.Add("Nature", typeof(string));
            //DataGridViewPanier.Add("Quantite", typeof(int));
            DataGridViewImageColumn dgvimag = new DataGridViewImageColumn();
            dgvimag.HeaderText = "Image";
            DataGridViewTextBoxColumn Ty = new DataGridViewTextBoxColumn();
            Qa.HeaderText = "Type";
            dgvimag.ImageLayout = DataGridViewImageCellLayout.Stretch;
            DataGridViewPanier.Columns.Add(na);
            DataGridViewPanier.Columns.Add(Qa);
            DataGridViewPanier.Columns.Add(dgvimag);
            DataGridViewPanier.Columns.Add(Ty);

        }

        public void data()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Designation  FROM Colis order by id_Colis DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet table = new DataSet();
            adapter.Fill(table);

            if (table.Tables[0].Rows.Count > 0)
            {
                string tmp = table.Tables[0].Rows[0]["Designation"].ToString().Substring(6, 4);
                int new_id = Convert.ToInt32(tmp) + 1;
                LabelDesignation.Text = "COLIS-" + new_id.ToString("0000");

                //MessageBox.Show(tmp);

            }
            else
            {
                LabelDesignation.Text = "COLIS-0001";
            }
        }

        public void rafrechir()
        {
            data();
            TextBoxNature.Text = "";
            PictureBoxColisAjouter.Image = Image.FromFile("../../Resources/man-3022704_19201.jpg");
            NumericUpDownQuantite.Value = 0;
            TextBoxPlomb.Text = "";
            TextBoxManifeste.Text = "";
            CheckBoxManifeste.Checked = true;
            CheckBoxPlomb.Checked = true;
            ComboBoxDeclarant.SelectedItem = null;
            ComboBoxImportateur.SelectedItem = null;
            ComboBoxPlaque.SelectedItem = null;
            DataGridViewPanier.Rows.Clear();
            DataGridViewColisModifier.DataSource = marchandise.listcolisJointureModifier();
            DataGridViewList.DataSource = marchandise.listcolisJointure();
            ComboBoxPlaque.DataSource = securites.listsecuriteOk();
            ComboBoxPlaque.DisplayMember = "Plaque";
            ComboBoxPlaque.ValueMember = "Id";
            ComboBoxPlaque.SelectedItem = null;

        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           

        }

        private void TextBoxPlomb_TextChanged(object sender, EventArgs e)
        {
            CheckBoxPlomb.Checked = false;
        }

        private void TextBoxManifeste_TextChanged(object sender, EventArgs e)
        {
            CheckBoxManifeste.Checked = false;
        }

        private void CheckBoxManifeste_CheckedChanged(object sender, EventArgs e)
        {

            if (CheckBoxManifeste.Checked == true)
            {
                TextBoxManifeste.Text = "";
                TextBoxManifeste.Enabled = false;
            }
            else
            {
                TextBoxManifeste.Enabled = true;

            }

        }

        private void CheckBoxManifeste_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private void CheckBoxPlomb_CheckedChanged(object sender, EventArgs e)
        {

            if (CheckBoxPlomb.Checked == true)
            {
                TextBoxPlomb.Text = "";
                TextBoxPlomb.Enabled = false;
            }
            else
            {
                TextBoxPlomb.Enabled = true;

            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBoxid.Text);

            DataTable table = marchandise.getColisbyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void Initialiserdonnee(DataTable data)
        {


            TextBoxid.Text = data.Rows[0][0].ToString();
            TextBoxDesignationModifier.Text = data.Rows[0][1].ToString();
            ComboBoxPlaqueModifier.SelectedValue = data.Rows[0][2];
            ComboBoxImportateurModifier.SelectedValue = data.Rows[0][3];

            string vl = data.Rows[0][4].ToString();
            if (vl == "Inconnue")
            {
                CheckBoxManifesteModifier.Checked = true;
            }
            else
            {
                TextBoxManifesteModifier.Text = vl;
                CheckBoxManifesteModifier.Checked = false;

            }

            ComboBoxDeclarantModifier.SelectedValue = data.Rows[0][5];

            string pl = data.Rows[0][6].ToString();

            if (pl == "Inconnue")
            {
                CheckBoxPlombModifier.Checked = true;
            }
            else
            {
                TextBoxPlombModifier.Text = pl;
                CheckBoxPlombModifier.Checked = false;

            }

            NumericUpDownQuantiteModifier.Value = Convert.ToInt32(data.Rows[0][7]);

            TextBoxNatureModifier.Text = data.Rows[0][8].ToString();
            DatepickerDateModifier.Value = (DateTime)data.Rows[0][9];
            try
            {
                byte[] couverture = (byte[])data.Rows[0][10];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureBoxColisModifier.Image = Image.FromStream(ms);
            }
            catch
            {

            }
        }

        private void bunifuTileButton5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton6_Click(object sender, EventArgs e)
        {

        }

        public void InitialiseCombo()
        {
            ComboBoxDeclarant.DataSource = declarant.list();
            ComboBoxDeclarant.DisplayMember = "Nom";
            ComboBoxDeclarant.ValueMember = "Id_Declarant";
            ComboBoxDeclarant.SelectedItem = null;

            ComboBoxImportateur.DataSource = importateur.listImportateur();
            ComboBoxImportateur.DisplayMember = "Nom";
            ComboBoxImportateur.ValueMember = "Id";
            ComboBoxImportateur.SelectedItem = null;

            ComboBoxPlaque.DataSource = securites.listsecuriteOk();
            ComboBoxPlaque.DisplayMember = "Plaque";
            ComboBoxPlaque.ValueMember = "Id";
            ComboBoxPlaque.SelectedItem = null;


            ComboBoxDeclarantModifier.DataSource = declarant.list();
            ComboBoxDeclarantModifier.DisplayMember = "Nom";
            ComboBoxDeclarantModifier.ValueMember = "Id_Declarant";
            ComboBoxDeclarantModifier.SelectedItem = null;

            ComboBoxImportateurModifier.DataSource = importateur.listImportateur();
            ComboBoxImportateurModifier.DisplayMember = "Nom";
            ComboBoxImportateurModifier.ValueMember = "Id";
            ComboBoxImportateurModifier.SelectedItem = null;

            ComboBoxPlaqueModifier.DataSource = securites.listsecurite();
            ComboBoxPlaqueModifier.DisplayMember = "Plaque";
            ComboBoxPlaqueModifier.ValueMember = "Id";
            ComboBoxPlaqueModifier.SelectedItem = null;

            if (CheckBoxManifeste.Checked == true)
            {
                TextBoxManifeste.Text = "";
                TextBoxManifeste.Enabled = false;
            }
            else
            {
                TextBoxManifeste.Enabled = true;
            }

            if (CheckBoxPlomb.Checked == true)
            {
                TextBoxPlomb.Text = "";
                TextBoxPlomb.Enabled = false;
            }
            else
            {
                TextBoxPlomb.Enabled = true;

            }
        }

        private void DataGridViewColisModifier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void bunifuTileButton8_Click(object sender, EventArgs e)
        {
        }
    
    


        private void DataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void TextBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void find(string value)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select id_Colis, Designation,Securite.Plaque, Importateur.Nom, Manifeste, Declarant.Nom, Plomb, Quantite, Nature, Colis.Date from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant  where CONCAT( Designation,Securite.Plaque, Importateur.Nom, Manifeste, Declarant.Nom, Plomb, Nature) like '%" + value + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewList.DataSource = table;

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuTileButton7_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
            DataGridViewList.DataSource = marchandise.listcolisJointure();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < DataGridViewPanier.Rows.Count; j++)
            {
                DataGridViewRow removerows = DataGridViewPanier.Rows[j];
                DataGridViewPanier.Rows.Remove(removerows);
            }
            rafrechir();
            //for (int i = 0; i < DataGridViewPanier.Rows.Count; i++)
            //{
            //    DataGridViewPanier.Rows.Remove(DataGridViewPanier.Rows[i]);

            //}

        }

        private void DataGridViewPanier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
           

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxColisAjouter.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPf = new OpenFileDialog();
            OPf.Filter = "Choisissez une image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (OPf.ShowDialog() == DialogResult.OK)
            {
                PictureBoxColisModifier.Image = Image.FromFile(OPf.FileName);
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Colis.ListColis colis = new ListColis();
            colis.Show();
        }

        public void AutoTexte()
        {
            TextBoxNature.PlaceholderText = "Nature";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Nature from Colis", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
            while(sdr.Read())
            {
                autotext.Add(sdr.GetString(0));
            }
            TextBoxNature.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TextBoxNature.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TextBoxNature.AutoCompleteCustomSource = autotext;
            con.Close();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
           

        }

        private void DataGridViewImportateur_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous supprimez cette article du panier??", "Panier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int rowindex = DataGridViewPanier.CurrentCell.RowIndex;
                DataGridViewPanier.Rows.RemoveAt(rowindex);
            }
        }

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {

            try
            {
                DataGridViewPanier.FirstDisplayedScrollingRowIndex = DataGridViewPanier.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewPanier_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewPanier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewPanier_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewPanier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewColisModifier.CurrentRow.Cells[0].Value);

            DataTable table = marchandise.getColisbyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewColisModifier.FirstDisplayedScrollingRowIndex = DataGridViewColisModifier.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewColisModifier_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewColisModifier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewColisModifier_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = DataGridViewColisModifier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select id_Colis, Designation,Securite.Plaque, Importateur.Nom, Manifeste, Declarant.Nom, Plomb, Quantite, Nature, Colis.Date from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant where Colis.Date between  '" + DateTimePickerdudate.Value.ToString("yyyy-MM-dd") + "' and  '" + DateTimeAudate.Value.ToString("yyyy-MM-dd") + "'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewList.DataSource = table;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            find(TextBoxRechercher.Text);
        }

        private void bunifuDataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewColisModifier.CurrentRow.Cells[0].Value);
        }

        private void bunifuVScrollBar3_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewList.FirstDisplayedScrollingRowIndex = DataGridViewList.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewList.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewList.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
            PanelModifier.BringToFront();
            DataTable table = marchandise.getColisbyid(id);


            if (table.Rows.Count > 0)
            {
                Initialiserdonnee(table);
            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
                if (MessageBox.Show("Vous-lez vous vraiment supprimer cette Enregistrement", "Suppression Enregistrement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (marchandise.EffaceColis(id))
                    {
                        MessageBox.Show("ENREGISTREMENT Supprimer", "Suppression enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewList.DataSource = marchandise.listcolisJointure();

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppression", "Suppression Enregsitrement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vous n'est pouvez pas Supprimer ce Colis, Car  il est lier Avec d'autres information dans la base de donnee! Commencer d'abord par supprimer cette liaisons", "COLIS", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DataGridViewList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuTileButton2_Click_1(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void bunifuTileButton3_Click_1(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
        }

        private void bunifuTileButton4_Click_1(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(TextBoxid.Text);
                string designation = TextBoxDesignationModifier.Text;
                int plaque = Convert.ToInt32(ComboBoxPlaqueModifier.SelectedValue);
                int importateur = Convert.ToInt32(ComboBoxImportateurModifier.SelectedValue);
                string manifeste = "";
                if (CheckBoxManifesteModifier.Checked == true)
                {
                    manifeste = "Inconnue";
                }
                else
                {
                    manifeste = TextBoxManifesteModifier.Text;
                }

                int declarant = Convert.ToInt32(ComboBoxDeclarantModifier.SelectedValue);
                string plomb = "";

                if (CheckBoxPlombModifier.Checked == true)
                {
                    plomb = "Inconnue";

                }
                else
                {
                    plomb = TextBoxPlombModifier.Text;
                }

                DateTime date = DatepickerDateModifier.Value;

                int quantite = Convert.ToInt32(NumericUpDownQuantiteModifier.Value);
                string nature = TextBoxNatureModifier.Text.ToString();

                MemoryStream ms = new MemoryStream();
                PictureBoxColisModifier.Image.Save(ms, PictureBoxColisModifier.Image.RawFormat);
                Byte[] image = ms.ToArray();

                int Q = 0;
                int v = 0;
                DataTable table = magasinage.chercherMofifierMagasin(id);
                if (table.Rows.Count > 0)
                {
                    Q = Convert.ToInt32(table.Rows[0][0].ToString());

                }

                DataTable tb = magasinage.ValeurQuantite(id);
                if (tb.Rows.Count < 0)
                {
                    v = 0;
                }
                else
                {
                    v = Convert.ToInt32(tb.Rows[0][0]);
                }

                if (ComboBoxPlaqueModifier.SelectedValue == null)
                {
                    MessageBox.Show("Veiller Choisir Une Plaque de Voiture S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (ComboBoxImportateurModifier.SelectedValue == null)
                {
                    MessageBox.Show("Veiller Choisir Un Importateur S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (ComboBoxDeclarantModifier.SelectedValue == null)
                {
                    MessageBox.Show("Veiller Choisir Un Declarant S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (designation.Trim().Equals(""))
                {
                    MessageBox.Show("La designation n'est peux pas etre vide", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (nature.Trim().Equals(" "))
                {
                    MessageBox.Show("La Nature n'est peux pas etre vide", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else

                {
                    if (quantite - v < 0)
                    {
                        MessageBox.Show("Impossible d'Effectuer Cette Modification Car: La Quantite Entree + La somme des Retrait des Colis deja effectuer donne une Valeur Negative du stock restant du stock Initiale de ce Colis", "Enregistrement Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (quantite == Q)
                        {
                            if(marchandise.ModifierColisParTransactionEgale(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image))
                            {
                                MessageBox.Show("COLIS MISE A JOUR", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                           
                            //if (marchandise.ModifierVoiture(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image))
                            //{
                            //    if (magasinage.ModifierEntreposage(plaque, id, nature, quantite, importateur))
                            //    {
                            //        MessageBox.Show("Fiche Magasin Mise a Jour", "Enregistrement Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("Echec de Mise A jour Fiche Magasin", "Enregistrement Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}

                        }
                        else if (quantite > Q)
                        {
                            if(marchandise.ModifierColisParTransactionPlus(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image,v))
                            {
                                MessageBox.Show("COLIS MISE A JOUR", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //if (magasinage.ModifierStockPlusMagasin(quantite, id, v))
                            //{
                            //    MessageBox.Show("Fiche Magasin Mise a Jour", "Enregistrement Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    if (marchandise.ModifierVoiture(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image))
                            //    {
                            //        MessageBox.Show("STOCK MISE A JOUR", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    }
                            //}
                        }
                        else if (quantite < Q)
                        {
                            if(marchandise.ModifierColisParTransactionMoins(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image, v))
                            {
                                MessageBox.Show("COLIS MISE A JOUR", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                                
                            //    if (magasinage.ModifierStockMoinsModifier(quantite, id, v))
                            //    {
                            //        MessageBox.Show("Fiche Magasin Mise a Jour", "Enregistrement Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        if (marchandise.ModifierVoiture(id, designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image))
                            //        {
                            //            MessageBox.Show("STOCK MISE A JOUR", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("Echec de Modification Colis", "Modification Colis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //        }
                            //    }
                        }

                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            Classes.Retrait retrait = new Classes.Retrait();


            string designation = LabelDesignation.Text;
            int plaque = Convert.ToInt32(ComboBoxPlaque.SelectedValue);
            int importateur = Convert.ToInt32(ComboBoxImportateur.SelectedValue);
            string manifeste = "";
            
            if (CheckBoxManifeste.Checked == true)
            {
                manifeste = "Inconnue";
            }
            else
            {
                manifeste = TextBoxManifeste.Text;
            }

          

            int declarant = Convert.ToInt32(ComboBoxDeclarant.SelectedValue);
            string plomb = "";

            if (CheckBoxPlomb.Checked == true)
            {
                plomb = "Inconnue";

            }
            else
            {
                plomb = TextBoxPlomb.Text;
            }

            DateTime date = DatepickerDateEntree.Value;

            if (DataGridViewPanier.Rows.Count < 1)
            {
                MessageBox.Show("Votre Panier est Vide!!! Veiller y ajouter des Articles", "MAGASINAGE", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                for (int i = 0; i < DataGridViewPanier.Rows.Count; i++)
                {

                    int quantite = Convert.ToInt32(DataGridViewPanier.Rows[i].Cells[1].Value);
                    string nature = DataGridViewPanier.Rows[i].Cells[0].Value.ToString();

                    byte[] couverture = (byte[])DataGridViewPanier.Rows[i].Cells[2].Value;
                    MemoryStream ms = new MemoryStream(couverture);
                    byte[] image = ms.ToArray();
                    string TypeMarchandise = DataGridViewPanier.Rows[i].Cells[3].Value.ToString();

                    if (ComboBoxPlaque.SelectedValue == null)
                    {
                        MessageBox.Show("Veiller Choisir Une Plaque de Voiture S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (ComboBoxImportateur.SelectedValue == null)
                    {
                        MessageBox.Show("Veiller Choisir Un Importateur S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (ComboBoxDeclarant.SelectedValue == null)
                    {
                        MessageBox.Show("Veiller Choisir Un Declarant S'il vous plait!", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (designation.Trim().Equals(""))
                    {
                        MessageBox.Show("La designation n'est peux pas etre vide", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (nature.Trim().Equals(" "))
                    {
                        MessageBox.Show("La Nature n'est peux pas etre vide", "Enregistrer une Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        int colis=0;
                        string Type="";
                        int retrais = 0;
                        
                        if(marchandise.AjouterVoiture(designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image, TypeMarchandise))
                        {
                            DataTable table = magasinage.Topdernier();

                            if (table.Rows.Count > 0)
                            {
                                colis = Convert.ToInt32(table.Rows[0][0]);
                                Type = "Entree";
                            }

                            if (marchandise.AjoutParTransaction(designation, plaque, importateur, manifeste, declarant, plomb, quantite, nature, date, image, colis, Type, retrais,plaque))
                            {
                               // MessageBox.Show("Entreposage Effectuer", "Enregistrement Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Echec d'Enregistrement Entreposage", "Enregistrement Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Echec d'ajout colis", "Enregistrement Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                }
                MessageBox.Show("Entreposage Effectuer", "Enregistrement Entreposage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rafrechir();
            }
        }

        private void DataGridViewPanier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ComboBoxPlaque_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (ComboBoxPlaque.SelectedValue != null)
                {
                    int ida = Convert.ToInt32(ComboBoxPlaque.SelectedValue.ToString());
                    DataTable data = marchandise.Getdata(ida);

                    if (data.Rows.Count > 0)
                    {
                        DatepickerDateEntree.Value = (DateTime)data.Rows[0][0];

                    }
                    else
                    {
                        MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception)
            {

               
            }
        }

        private void TextBoxNature_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TextBoxNature_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxNature_MouseEnter(object sender, EventArgs e)
        {
            TextBoxNature.BorderColor = Color.FromArgb(213, 218, 223);
            TextBoxNature.BorderThickness = 3;
        }
    }
}
