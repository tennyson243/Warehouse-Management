using MessagingToolkit.QRCode.Codec;
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

namespace Gestion_Entrepot.Retrait
{
    public partial class GererRetrait : Form
    {
        public GererRetrait()
        {
            InitializeComponent();
        }

        Classes.Colis colis = new Classes.Colis();
        Classes.Retrait retrait = new Classes.Retrait();
        Classes.Securites securites = new Classes.Securites();
        Classes.Declarant declarant = new Classes.Declarant();
        Classes.Magasinage magasinage = new Classes.Magasinage();
        Classes.Circulation circulation = new Classes.Circulation();
        Classes.Check check = new Classes.Check();
        private void GererRetrait_Load(object sender, EventArgs e)
        {
            // IniatialiserUsercontrole();

            data();
            data1();
            AutoTexteNom();
            AutoTexteNum();
            AutoTextePlaque();
            ComboBoxPlaque.DataSource = colis.listvoiture();
            ComboBoxPlaque.DisplayMember = "Designation";
            ComboBoxPlaque.ValueMember = "Id_Colis";
            ComboBoxPlaque.SelectedItem = null;

            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
            DataGridViewList.DataSource = retrait.list();
            LabelDeclarantID.Hide();
            LabelPlaqueID.Hide();
            LabelIDColis.Hide();
            LabelIdMagasinage.Hide();
            pictureBoxQr.Hide();
        }

        string Bon;

        public void IniatialiserUsercontrole()
        {
            flowLayoutPanelStock.Controls.Clear();
            DataTable table = retrait.LireLesitems();

            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    UserControlStock[] listItems = new UserControlStock[table.Rows.Count];
                    for (int i = 0; i < 1; i++)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            listItems[i] = new UserControlStock();
                            listItems[i].labelId.Text = row["id_Colis"].ToString();
                            listItems[i].labelNom.Text = row["Nature"].ToString();
                            listItems[i].labelQunatite.Text = row["Quantite"].ToString();
                            //listItems[i].NUDQ.Value = Convert.ToInt32(row["Quantite"].ToString());
                            byte[] couverture = (byte[])row["Photo"];
                            MemoryStream ms = new MemoryStream(couverture);
                            byte[] image = ms.ToArray();
                            listItems[i].pictureBoxStock.Image = Image.FromStream(ms);
                            flowLayoutPanelStock.Controls.Add(listItems[i]);


                            listItems[i].OnSelected += (ss, ee) =>
                            {

                                var vwd = (UserControlStock)ss;
                                foreach (DataGridViewRow item in GRID.Rows)
                                {
                                    //if (item.Cells[0].Value.ToString()== row["Nature"].ToString())
                                    //{
                                    //    item.Cells[1].Value = int.Parse(item.Cells[1].Value.ToString());
                                    //    return;
                                    //}
                                    // MessageBox.Show(vwd.labelQunatite.Text = row["Quantite"].ToString());

                                }
                                GRID.Rows.Add(new object[] { row["Nature"].ToString(), vwd.NUDQ.Value });
                                //calculeTotal();
                            };


                        }

                    }


                }
            }
        }

        private void TextBoxRechercher_TextChanged(object sender, EventArgs e)
        {
            //foreach (var item in flowLayoutPanelStock.Controls)
            //{
            //    var wdg = (UserControlStock)item;
            //    wdg.Visible = wdg.labelNom.Text.ToLower().Contains(TextBoxRechercher.Text.Trim().ToLower());
            //}

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void calculeTotal()
        {
            double tot = 0;
            foreach (DataGridViewRow items in GRID.Rows)
            {
                tot += double.Parse(items.Cells[1].Value.ToString());
            }
            LabelTot.Text = tot.ToString();
        }

        private void ComboBoxPlaque_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void data()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Designation FROM Retrait order by id_Retrait DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet table12 = new DataSet();
            adapter.Fill(table12);

            if (table12.Tables[0].Rows.Count > 0)
            {
                string tmp = table12.Tables[0].Rows[0]["Designation"].ToString().Substring(7, 4);
                int new_id = Convert.ToInt32(tmp) + 1;
                LabelDesignationSortie.Text = "SORTIE-" + new_id.ToString("0000");

                //MessageBox.Show(tmp);

            }
            else
            {
                LabelDesignationSortie.Text = "SORTIE-0001";
            }
        }

        public void data1()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Num_Bon FROM CheckSecurite order by id DESC", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet table12 = new DataSet();
            adapter.Fill(table12);

            if (table12.Tables[0].Rows.Count > 0)
            {
                string tmp = table12.Tables[0].Rows[0]["Num_Bon"].ToString().Substring(4, 4);
                int new_id = Convert.ToInt32(tmp) + 1;
                Bon = "BON-" + new_id.ToString("0000");

                //MessageBox.Show(tmp);

            }
            else
            {
                Bon= "BON-0001";
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (GRID.Rows.Count < 1)
            {
                MessageBox.Show("Le Panier est Vide Il y a Rien a Supprimer", "Panier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (MessageBox.Show("Voulez-vous supprimez cette article du panier??", "Panier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        int rowindex = GRID.CurrentCell.RowIndex;
                        GRID.Rows.RemoveAt(rowindex);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Veillez Selectionner Une ligne dans le tableau", "Panier", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
            }

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            GRID.Rows.Clear();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        public void rafrechir()
        {
           
            LabelDeclarant.Text = "-";
            LabelEntree.Text = "-";
            LabelImportateur.Text = "-";
            LabelManifeste.Text = "-";
            TextBoxNumChauffeurAjouter.Text = "";
            TextBoxPlaqueDeSortie.Text = "";
            TextBoxNomChauffeur.Text = "";
            GRID.Rows.Clear();
            ComboBoxPlaque.SelectedItem = null;
            ComboBoxPlaque.Enabled = true;
        }

        private void DataGridViewModifier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void PanelModifier_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
            DataGridViewList.DataSource = retrait.list();
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
            DataGridViewList.DataSource = retrait.list();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            PanelList.BringToFront();
            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
            DataGridViewList.DataSource = retrait.list();
        }

        private void DataGridViewModifier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridViewRetraitModifier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void DataGridViewRetraitModifier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            int idretrait = Convert.ToInt32(TextBoxid.Text);
            int quantite = Convert.ToInt32(TextBoxQuantite.Text);
            int Q = 0;
            int M = 0;
            DataTable tb = magasinage.chercherMofifier(idretrait);
            if (tb.Rows.Count > 0)
            {
                Q = Convert.ToInt32(tb.Rows[0][0]);

            }

            int idColis = Convert.ToInt32(LabelIDColis.Text);
            DataTable bleta = magasinage.chercherMofifierMagasin(idColis);
            if (bleta.Rows.Count > 0)
            {
                M = Convert.ToInt32(bleta.Rows[0][0]);

            }

            int Val2 = quantite - Q;
            int Val3 = Q - Val2;
            int Val1 = M - Val3;

            //int plus = Q + Val3;
            //LabelTestplus.Text = M.ToString();
            //LabelTestMoins.Text = Val3.ToString();

           
           //MessageBox.Show(Convert.ToInt32(plus).ToString());
        }

        private void DataGridViewList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void DataGridViewList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2TextBox8_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select id_Retrait, Designation, Securite.Plaque, Retrait.Nature, Retrait.Quantite, Declarant.Nom, Retrait.Sortie,  Nom_Chauffeur, Num_Chauffeur, Retrait.Date from Retrait inner join Securite on Securite.Id_Securite=Retrait.Entree inner join Declarant on Declarant.Id_Declarant = Retrait.Declarant  where CONCAT( Designation, Securite.Plaque, Retrait.Nature,Declarant.Nom, Retrait.Sortie,  Nom_Chauffeur) like '%" + TextBoxRechercheList.Text + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            DataGridViewList.DataSource = table;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void DataGridViewList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string designation = DataGridViewList.CurrentRow.Cells[1].Value.ToString();
            Print.PrintEffetPersonnel personnelle = new Print.PrintEffetPersonnel(designation);
            personnelle.Show();
        }

        private void ComboBoxPlaque_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if (ComboBoxPlaque.SelectedItem == null)
            {
                flowLayoutPanelStock.Controls.Clear();
            }
            else
            {
                flowLayoutPanelStock.Controls.Clear();
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
                SqlCommand cmd = new SqlCommand("Select id_Colis, Magasinage.Reste as Reste, Colis.Nature, Colis.Photo as Photo, Designation from Colis inner join Securite on Securite.Id_Securite = Colis.Plaque inner join Importateur on Importateur.Id_Importateur = Colis.Importateur inner join Declarant on Declarant.Id_Declarant = Colis.Declarant inner join Magasinage on Magasinage.Colis = Colis.Id_Colis  where Designation like '%" + ComboBoxPlaque.Text.ToString() + "%'", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        UserControlStock[] listItems = new UserControlStock[table.Rows.Count];
                        for (int i = 0; i < 1; i++)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                listItems[i] = new UserControlStock();
                                listItems[i].labelId.Text = row["id_Colis"].ToString();
                                listItems[i].labelNom.Text = row["Nature"].ToString();
                                listItems[i].labelQunatite.Text = row["Reste"].ToString();
                                //listItems[i].NUDQ.Value = Convert.ToInt32(row["Quantite"].ToString());
                                byte[] couverture = (byte[])row["Photo"];
                                MemoryStream ms = new MemoryStream(couverture);
                                byte[] image = ms.ToArray();
                                listItems[i].pictureBoxStock.Image = Image.FromStream(ms);
                                flowLayoutPanelStock.Controls.Add(listItems[i]);


                                listItems[i].OnSelected += (ss, ee) =>
                                {

                                    var vwd = (UserControlStock)ss;
                                    foreach (DataGridViewRow item in GRID.Rows)
                                    {
                                        //if (item.Cells[0].Value.ToString()== row["Nature"].ToString())
                                        //{
                                        //    item.Cells[1].Value = int.Parse(item.Cells[1].Value.ToString());
                                        //    return;
                                        //}
                                        // MessageBox.Show(vwd.labelQunatite.Text = row["Quantite"].ToString());

                                    }
                                    int var = Convert.ToInt32(vwd.labelQunatite.Text);
                                    if (vwd.NUDQ.Value > var)
                                    {
                                        MessageBox.Show("LE STOCK DE CE PRODUIT EST  INFERIEUR A LA QUANTITE DEMANDER", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else if (vwd.NUDQ.Value == 0)
                                    {
                                        MessageBox.Show("Impossible d'enregistrer une Sortie ou la Quantite Egale a Zero", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else if (var == 0)
                                    {
                                        MessageBox.Show("Ce colis n'existe plus dans ce magasin!! Tous les Produits ont ete Retirer!!", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
   
                                    else
                                    {
                                        GRID.Rows.Add(new object[] { row["id_Colis"].ToString(), row["Nature"].ToString(), vwd.NUDQ.Value });

                                        LabelTot.Text = (from DataGridViewRow row1 in GRID.Rows
                                                         where row1.Cells[2].FormattedValue.ToString() != string.Empty
                                                         select Convert.ToInt32(row1.Cells[2].FormattedValue)).Sum().ToString();
                                    }
                                    
                                    if(GRID.Rows.Count>0)
                                    {
                                        ComboBoxPlaque.Enabled = false;
                                    }
                                    
                                };


                            }

                        }


                    }

                }
            }





            try
            {
                if (ComboBoxPlaque.SelectedValue != null)
                {
                    int ida = Convert.ToInt32(ComboBoxPlaque.SelectedValue.ToString());
                    DataTable data = retrait.Getdata(ida);

                    if (data.Rows.Count > 0)
                    {
                        string value = data.Rows[0][0].ToString();
                        int val = Convert.ToInt32(value);

                        DataTable retour = retrait.Getdata(val);

                        if (retour.Rows.Count > 0)
                        {
                            LabelEntree.Text = data.Rows[0][1].ToString();
                            LabelImportateur.Text = data.Rows[0][2].ToString();
                            LabelDeclarant.Text = data.Rows[0][3].ToString();
                            LabelManifeste.Text = data.Rows[0][4].ToString();
                            LabelPlaqueID.Text = data.Rows[0][5].ToString();
                            LabelDeclarantID.Text = data.Rows[0][6].ToString();
                        }

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

        private void bunifuVScrollBar2_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {

            try
            {
                GRID.FirstDisplayedScrollingRowIndex = GRID.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void GRID_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = GRID.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void GRID_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar2.Maximum = GRID.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

            string Designation = LabelDesignationSortie.Text;
            int plaque = Convert.ToInt32(LabelPlaqueID.Text);
            string Sortie = TextBoxPlaqueDeSortie.Text;
            string NomCh = TextBoxNomChauffeur.Text;
            string NumCh = TextBoxNumChauffeurAjouter.Text;

            QRCoder.QRCodeGenerator QR = new QRCoder.QRCodeGenerator();
            var Mydata = QR.CreateQrCode(Designation, QRCoder.QRCodeGenerator.ECCLevel.H);
            var code = new QRCoder.QRCode(Mydata);
            pictureBoxQr.Image = code.GetGraphic(200);

            MemoryStream ms = new MemoryStream();
            pictureBoxQr.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Byte[] Photo =new byte[ms.Length];
            ms.Position = 0;
            ms.Read(Photo, 0, Photo.Length);

            //QRCodeEncoder enc = new QRCodeEncoder();
            //enc.QRCodeScale = 1;
            //Bitmap qrcode = enc.Encode(Designation);
            //pictureBoxQr.Image = qrcode as Image;

            //MemoryStream ms = new MemoryStream();
            //pictureBoxQr.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //Byte[] image = ms.ToArray();

            if (NomCh.Trim().Equals(""))
            {
                MessageBox.Show("La Nom du chauffeur n'est peux pas etre vide", "Effectuer un RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (NumCh.Trim().Equals(""))
            {
                MessageBox.Show("Le Numero du chauffeur n'est peux pas etre vide", "Effectuer un RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (Sortie.Trim().Equals(""))
            {
                MessageBox.Show("La Plaque du Sortie n'est peux pas etre vide", "Effectuer un RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (GRID.Rows.Count < 1)
            {
                MessageBox.Show("Votre Panier est Vide!!! Veiller y ajouter des Articles", "Effectuer un RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                for (int i = 0; i < GRID.Rows.Count; i++)
                {
                    int quantite = Convert.ToInt32(GRID.Rows[i].Cells[2].Value);
                    string nature = GRID.Rows[i].Cells[1].Value.ToString();
                    int id = Convert.ToInt32(GRID.Rows[i].Cells[0].Value);
                    int Declarant = Convert.ToInt32(LabelDeclarantID.Text);
                    DateTime Date = DatepickerDateSorties.Value;

                    //DataTable table = magasinage.Topdernier();
                    //int colis = 0;
                    //if (table.Rows.Count > 0)
                    //{
                    //    colis = Convert.ToInt32(table.Rows[0][0]);

                    //}
                    //DataTable bleta = magasinage.TopImportateur(colis);
                    //int Importateur = 0;
                    //if (bleta.Rows.Count > 0)
                    //{
                    //    Importateur = Convert.ToInt32(bleta.Rows[0][0]);
                    //}
                    //string Type = "Sortie";

                    //DataTable tb = magasinage.chercherReste(id);
                    //int reste = 0;
                    //if (tb.Rows.Count > 0)
                    //{
                    //    reste = Convert.ToInt32(tb.Rows[0][0]);
                    //}


                    //if (retrait.AjouterRetraitParTransaction(Designation, id, plaque, nature, quantite, Declarant, Sortie, NomCh, NumCh, Date,reste,Type,retrais))
                    //{
                    //    MessageBox.Show("Retrait Effectuer Avec Succes", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Echec de Retrait", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}

                    if (retrait.AjouterRetrait(Designation, id, plaque, nature, quantite, Declarant, Sortie, NomCh, NumCh, Date, Photo))
                    {
                       
                        DataTable table = magasinage.Topdernier();
                        int colis = 0;
                        if (table.Rows.Count > 0)
                        {
                            colis = Convert.ToInt32(table.Rows[0][0]);

                        }
                        DataTable bleta = magasinage.TopImportateur(colis);
                        int Importateur = 0;
                        if (bleta.Rows.Count > 0)
                        {
                            Importateur = Convert.ToInt32(bleta.Rows[0][0]);
                        }
                        string Type = "Sortie";
                       

                        if (magasinage.ModifierStock(nature, quantite, id))
                        {
                           // MessageBox.Show("Fiche Magasin Mise a Jour", "Mise a jour du Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            DataTable tb = magasinage.chercherReste(id);
                            int reste = 0;
                            if (tb.Rows.Count > 0)
                            {
                                reste = Convert.ToInt32(tb.Rows[0][0]);
                            }

                            DataTable trt = retrait.TopdernierRetrais();

                            int retrais = 0;
                            if (trt.Rows.Count > 0)
                            {
                                retrais = Convert.ToInt32(trt.Rows[0][0]);
                            }
                            if (circulation.AjouterCirculation(Type, Declarant, Importateur, plaque, Date, reste, id, retrais))
                            {
                                //MessageBox.Show("Circulation Enregistrer", "Circulation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                pictureBox2.Image = Image.FromFile("../../Acteurs/Non.png");
                                MemoryStream mss = new MemoryStream();
                                pictureBox2.Image.Save(mss, pictureBox2.Image.RawFormat);
                                Byte[] Controle = mss.ToArray();
                                string Validteur = "En Attente";

                                if (check.AjouterCheck(Bon, Designation, nature, quantite, Validteur, Date, Date, Controle))
                                {

                                }
                                else
                                {
                                    MessageBox.Show("Controle Non Enregistrer", "Check_Securite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Circulation Non Enregistrer", "Circulation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Ficher de Magasin Non Mise a Jour", "Mise a jour du Fiche Magasin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Echec de Retrait", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                MessageBox.Show("Retrait Effectuer Avec Succes", "RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rafrechir();
                data();
                data1();
                string Des = "";
                DataTable bletata = retrait.TopdernierDesignationRetrais();
                
                if (bletata.Rows.Count > 0)
                {
                    Des = bletata.Rows[0][0].ToString();

                }
                
                Print.PrintEffetPersonnel personne = new Print.PrintEffetPersonnel(Des);
                personne.Show();

                
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            data();
            TextBoxNomChauffeur.Text = "";
            TextBoxNumChauffeurAjouter.Text = "";
            TextBoxPlaqueDeSortie.Text = "";
            ComboBoxPlaque.SelectedItem = null;

            GRID.Rows.Clear();

        }

        private void bunifuTileButton2_Click_1(object sender, EventArgs e)
        {
            PanelAjouter.BringToFront();
        }

        private void bunifuTileButton3_Click_1(object sender, EventArgs e)
        {
            PanelModifier.BringToFront();
            LabelIDColis.Hide();
            LabelIdMagasinage.Hide();
            TextBoxid.Text = "";
            TextBoxDesignationModifier.Text = "";
            TextBoxNom.Text = "";
            TextBoxDeclarant.Text = "";
            TextBoxNum.Text = "";
            TextBoxNature.Text = "";
            TextBoxSortie.Text = "";
            TextBoxQuantite.Text = "";
        }

        private void bunifuTileButton4_Click_1(object sender, EventArgs e)
        {
            PanelList.BringToFront();
        }

        private void ButtonModif_Click(object sender, EventArgs e)
        {

            string Designation = TextBoxDesignationModifier.Text;
            int id = Convert.ToInt32(TextBoxid.Text);
            string Sortie = TextBoxSortie.Text;
            string NomCh = TextBoxNom.Text;
            string NumCh = TextBoxNum.Text;
            int quantite = Convert.ToInt32(TextBoxQuantite.Text);
            string nature = TextBoxNature.Text;
            DateTime Date = DatepickerModifier.Value;

            int idretrait = Convert.ToInt32(TextBoxid.Text);
            int QuantiteRetrais = 0;
            int SommeQuantiteRetraisParIDColis = 0;
            DataTable table = magasinage.chercherMofifier(idretrait);
            if (table.Rows.Count > 0)
            {
                QuantiteRetrais = Convert.ToInt32(table.Rows[0][0].ToString());

            }

            int idColis = Convert.ToInt32(LabelIDColis.Text);
            DataTable bleta = magasinage.ValeurQuantite(idColis);
            if (bleta.Rows.Count > 0)
            {
                SommeQuantiteRetraisParIDColis = Convert.ToInt32(bleta.Rows[0][0].ToString());

            }
            int QuantiteMagasinage = 0;
            DataTable tb = magasinage.QuantiteMagasinage(idColis);
            if (tb.Rows.Count > 0)
            {
                QuantiteMagasinage = Convert.ToInt32(tb.Rows[0][0].ToString());

            }

            int Val2 = quantite - SommeQuantiteRetraisParIDColis;
            int Val3 = QuantiteRetrais - Val2;
            int Val1 = SommeQuantiteRetraisParIDColis - Val3;

         
            int plus = quantite - QuantiteRetrais;

            if (Designation.Trim().Equals(""))
            {
                MessageBox.Show("La designation n'est peux pas etre vide", "Effectuer un RETRAIT", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (NomCh.Trim().Equals(" "))
            {
                MessageBox.Show("La Nom du chauffeur n'est peux pas etre vide", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (NumCh.Trim().Equals(" "))
            {
                MessageBox.Show("Le Numero du chauffeur n'est peux pas etre vide", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (Sortie.Trim().Equals(" "))
            {
                MessageBox.Show("La Plaque du Sortie n'est peux pas etre vide", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (quantite > QuantiteRetrais)
                {  if (plus > QuantiteMagasinage)
                    {
                        MessageBox.Show("La Quantite Demander est Superieur a La Valeur Initiale de ce Colis", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else 
                    {
                        int Val = quantite - QuantiteRetrais;
                        if (retrait.ModifierRetraitParTransactionPlus(idColis, id, Designation, nature, quantite, Sortie, NomCh, NumCh, Date, Val))
                        {
                            MessageBox.Show("RETRAIT MISE A JOUR", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rafrechir();
                            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
                            DataGridViewList.DataSource = retrait.list();
                        }
                        else
                        {
                            MessageBox.Show("Echec de Mise a Jour du Retrais", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                   
                }
                else if (quantite < QuantiteRetrais)
                {
                    int Val4 = QuantiteRetrais - quantite;

                    if(Val4>QuantiteMagasinage)
                    {
                        MessageBox.Show("Imposible de faire la mise a jour du colis avec cette Quantite car ,La Quantite Demander + la somme des retrais deja Effectuer pour ce colis donne une valeurs Superieur a la Quantite Initiale de ce Colis", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        if (retrait.ModifierRetraitParTransactionMoins(idColis, id, Designation, nature, quantite, Sortie, NomCh, NumCh, Date, Val4))

                        {
                            MessageBox.Show("RETRAIT MISE A JOUR", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rafrechir();
                            DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
                            DataGridViewList.DataSource = retrait.list();
                        }
                        else
                        {
                            MessageBox.Show("Echec de Mise a Jour du Retrais", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("STOCK MISE A JOUR", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (retrait.ModifierRetrait(id, Designation, nature, quantite, Sortie, NomCh, NumCh, Date))
                    {
                        MessageBox.Show("Retrait Modifier", "Modification Retrait", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rafrechir();
                        DataGridViewRetraitModifier.DataSource = retrait.listRetraitJointureModifier();
                        DataGridViewList.DataSource = retrait.list();
                    }
                    else
                    {
                        MessageBox.Show("Echec de Modification de Retrait", "Modification retrait", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_Retrait = Convert.ToInt32(DataGridViewRetraitModifier.CurrentRow.Cells[0].Value.ToString());

            DataTable bleta = retrait.ListByeID(Id_Retrait);

            if (bleta.Rows.Count > 0)
            {
                TextBoxid.Text = bleta.Rows[0][0].ToString();
                LabelIDColis.Text = bleta.Rows[0][1].ToString();
                TextBoxDesignationModifier.Text = bleta.Rows[0][2].ToString();
                TextBoxPlaqueEntree.Text = bleta.Rows[0][3].ToString();
                TextBoxNature.Text = bleta.Rows[0][4].ToString();
                TextBoxQuantite.Text = bleta.Rows[0][5].ToString();
                TextBoxDeclarant.Text = bleta.Rows[0][6].ToString();
                TextBoxSortie.Text = bleta.Rows[0][7].ToString();
                TextBoxNom.Text = bleta.Rows[0][8].ToString();
                TextBoxNum.Text = bleta.Rows[0][9].ToString();
                DatepickerModifier.Value = (DateTime)bleta.Rows[0][10];
                LabelIdMagasinage.Text = bleta.Rows[0][11].ToString();

                try
                {
                    byte[] couverture = (byte[])bleta.Rows[0][12];
                    MemoryStream ms = new MemoryStream(couverture);
                    byte[] image = ms.ToArray();
                    pictureBox1.Image = Image.FromStream(ms);
                }
                catch
                {

                }


            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuVScrollBar3_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewRetraitModifier.FirstDisplayedScrollingRowIndex = DataGridViewRetraitModifier.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar3.Maximum = DataGridViewRetraitModifier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar3.Maximum = DataGridViewRetraitModifier.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuDataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
        }

      

        private void bunifuVScrollBar1_Scroll(object sender, Bunifu.UI.WinForms.BunifuVScrollBar.ScrollEventArgs e)
        {
            try
            {
                DataGridViewList.FirstDisplayedScrollingRowIndex = DataGridViewList.Rows[e.Value].Index;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewList.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void DataGridViewList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                bunifuVScrollBar1.Maximum = DataGridViewList.RowCount - 1;
            }
            catch (Exception)
            {


            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);

            PanelModifier.BringToFront();
            DataTable bleta = retrait.ListByeID(id);

            if (bleta.Rows.Count > 0)
            {
                TextBoxid.Text = bleta.Rows[0][0].ToString();
                LabelIDColis.Text = bleta.Rows[0][1].ToString();
                TextBoxDesignationModifier.Text = bleta.Rows[0][2].ToString();
                TextBoxPlaqueEntree.Text = bleta.Rows[0][3].ToString();
                TextBoxNature.Text = bleta.Rows[0][4].ToString();
                TextBoxQuantite.Text = bleta.Rows[0][5].ToString();
                TextBoxDeclarant.Text = bleta.Rows[0][6].ToString();
                TextBoxSortie.Text = bleta.Rows[0][7].ToString();
                TextBoxNom.Text = bleta.Rows[0][8].ToString();
                TextBoxNum.Text = bleta.Rows[0][9].ToString();
                DatepickerModifier.Value = (DateTime)bleta.Rows[0][10];
                LabelIdMagasinage.Text = bleta.Rows[0][11].ToString();



            }
            else
            {
                MessageBox.Show("C'est ID N'Existe Pas, Veillez Selectionner  un autre ID", "ID introuvable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(DataGridViewList.CurrentRow.Cells[0].Value);
                if (MessageBox.Show("Etes vous sure de vouloir vraiment supprimer cette Enregistrement", "Suppression Enregistrement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (retrait.EffaceRetrait(id))
                    {
                        MessageBox.Show("ENREGISTREMENT Supprimer", "Suppression enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DataGridViewList.DataSource = retrait.list();

                    }
                    else
                    {
                        MessageBox.Show("Echec de Suppression", "Suppression Enregsitrement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalide ID");
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Retrait.ListRetrait list = new Retrait.ListRetrait();
            list.Show();
        }

        private void DataGridViewList_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string designation = DataGridViewList.CurrentRow.Cells[1].Value.ToString();
            Print.PrintEffetPersonnel personne = new Print.PrintEffetPersonnel(designation);
            personne.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Designation = LabelDesignationSortie.Text;
            QRCodeEncoder enc = new QRCodeEncoder();
            enc.QRCodeScale = 1;
            Bitmap qrcode = enc.Encode(Designation);
            pictureBoxQr.Image = qrcode as Image;
        }

        private void PanelAjouter_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxNomChauffeur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBoxNumChauffeurAjouter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar)&& !(e.KeyChar==43))
            {
                e.Handled = true;
            }
        }

        public void AutoTexteNom()
        {
            TextBoxNomChauffeur.PlaceholderText = "Nom du Chauffeur";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Nom_Chauffeur from Retrait", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
            while (sdr.Read())
            {
                autotext.Add(sdr.GetString(0));
            }
            TextBoxNomChauffeur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TextBoxNomChauffeur.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TextBoxNomChauffeur.AutoCompleteCustomSource = autotext;
            con.Close();
        }

        public void AutoTexteNum()
        {
           TextBoxNumChauffeurAjouter.PlaceholderText = "Num du Chauffeur";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Num_Chauffeur from Retrait", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
            while (sdr.Read())
            {
                autotext.Add(sdr.GetString(0));
            }
            TextBoxNumChauffeurAjouter.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TextBoxNumChauffeurAjouter.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TextBoxNumChauffeurAjouter.AutoCompleteCustomSource = autotext;
            con.Close();
        }

        public void AutoTextePlaque()
        {
            TextBoxPlaqueDeSortie.PlaceholderText = "Plaque de Sortie";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30");
            SqlCommand cmd = new SqlCommand("Select Sortie from Retrait", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
            while (sdr.Read())
            {
                autotext.Add(sdr.GetString(0));
            }
            TextBoxPlaqueDeSortie.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TextBoxPlaqueDeSortie.AutoCompleteSource = AutoCompleteSource.CustomSource;
            TextBoxPlaqueDeSortie.AutoCompleteCustomSource = autotext;
            con.Close();
        }

        private void TextBoxNumChauffeurAjouter_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
        

