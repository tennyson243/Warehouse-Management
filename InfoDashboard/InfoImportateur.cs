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

namespace Gestion_Entrepot.InfoDashboard
{
    public partial class InfoImportateur : Form
    {
        public InfoImportateur()
        {
            InitializeComponent();
        }

        private void InfoImportateur_Load(object sender, EventArgs e)
        {
            Classes.Importateur importateur = new Classes.Importateur();
            DataTable Listedelivre = importateur.listImportateurCool();

            ListViewItem[] items = new ListViewItem[Listedelivre.Rows.Count];
            string[] titre = new string[Listedelivre.Rows.Count];

            //Boucle pour mettre des images dans l'images views
            for (int i = 0; i < Listedelivre.Rows.Count; i++)
            {
                byte[] image = (byte[])Listedelivre.Rows[i][4];

                //Ajouter les images dans la liste des images
                MemoryStream ms = new MemoryStream(image);
                imageList_Entree.Images.Add(Image.FromStream(ms));

                //Ajouter les titres dans la partie titre
                titre[i] = Listedelivre.Rows[i][1].ToString();

            }

            //Boucles pour initialiser les donneees dans la liste vieuws

            listView_eNTREE.View = View.LargeIcon;
            imageList_Entree.ImageSize = new Size(175, 250);
            listView_eNTREE.LargeImageList = imageList_Entree;



            for (int j = 0; j < imageList_Entree.Images.Count; j++)
            {
                listView_eNTREE.Items.Add(new ListViewItem() { Text = titre[j], ImageIndex = j });
            }
        }
    }
}
