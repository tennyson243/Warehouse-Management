using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Entrepot.Login
{
    public partial class Controle : Form
    {
        public Controle()
        {
            InitializeComponent();
        }

        private void Controle_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxNomUtilisateur1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            BDD.Connecteur db = new BDD.Connecteur();



            string Motdepasse = TextBoxNomUtilisateur1.Text;


            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select*from Controle where Mot_de_Passe = @Mot_de_Passe", db.getconnexion());

            cmd.Parameters.Add("@Mot_de_Passe", SqlDbType.VarChar).Value = Motdepasse;

            adapter.SelectCommand = cmd;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {
                Utilisateur.GererUtilisateur gerer = new Utilisateur.GererUtilisateur();
                gerer.Show();
            }
            else
            {
                if (Motdepasse.Trim().Equals(""))
                {
                    MessageBox.Show("Mettez votre code Secret", "Code Secret vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Le Code Secret est incorrect", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextBoxNomUtilisateur1.Text = "";
                    this.Close();
                }
            }
        }
    }
}
