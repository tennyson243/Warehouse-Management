using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Acteurs
{
    public partial class DetailsImportateur : Form
    {
        int importateurID;
        public DetailsImportateur(int id)
        {
            InitializeComponent();
            this.importateurID = id;
        }

        Classes.Importateur importateur = new Classes.Importateur();

        private void DetailsImportateur_Load(object sender, EventArgs e)
        {
            DataTable table = importateur.getImportateurbyid(importateurID);
            labelid.Text = table.Rows[0][0].ToString();
            labelNom.Text = table.Rows[0][1].ToString();
            labelAdresse.Text = table.Rows[0][2].ToString();
            labelVille.Text = table.Rows[0][3].ToString();
            labelPays.Text = table.Rows[0][4].ToString();
            labelTelephone.Text = table.Rows[0][5].ToString();

            try
            {
                byte[] couverture = (byte[])table.Rows[0][6];
                MemoryStream ms = new MemoryStream(couverture);
                byte[] image = ms.ToArray();
                PictureDetails.Image = Image.FromStream(ms);
            }
            catch
            {

            }

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
