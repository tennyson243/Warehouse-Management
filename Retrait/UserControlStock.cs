using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Retrait
{
    public partial class UserControlStock : UserControl
    {

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public UserControlStock()
        {
            InitializeComponent();
        }

        public event EventHandler OnSelected= null;
        protected virtual void pictureBoxStock_Click(EventArgs e)
        {
            OnSelected?.Invoke(this, e);

        }



        
        public void CheckBoxUser_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {

         

        }

        public int id { get => Convert.ToInt32(labelId.Text); set => labelId.Text = Convert.ToInt32(value).ToString(); }
        public string Nature { get => labelNom.Text; set => labelNom.Text = value; }
        public string Quantite { get => labelNom.Text; set => labelNom.Text = value; }
        
        public Image Icon { get => pictureBoxStock.Image; set => pictureBoxStock.Image = value; }
        private void CheckBoxUser_Click(object sender, EventArgs e)
        {
          
        }

        Retrait.GererRetrait retrait = new GererRetrait();
       // private void pictureBoxStock_Click(object sender, EventArgs e)
       // {

            //foreach (DataGridViewRow item in retrait.DataGridViewRetrait.Rows)
            //{
            //    if (item.Cells[0].Value.ToString()==labelNom.Text) ;
            //    {
            //       item.Cells[1].Value = int.Parse(item.Cells[1].Value.ToString());
            //    }
            //}
            //retrait.DataGridViewRetrait.Rows.Add(new object[] { labelNom.Text, labelQunatite.Text });

       // }

        public void data1(int id,string Nom)
        {
            id = Convert.ToInt32( labelId.Text);
            Nom = labelNom.Text;
        }

        private void pictureBoxStock_Click(object sender, EventArgs e)
        {
            OnSelected?.Invoke(this, e);
        }
    }
}
