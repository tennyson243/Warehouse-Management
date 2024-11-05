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
    public partial class Login_Form : Form
    {
        private PanneauPrincipal.PanneauPrincipal dp = null;
        public Login_Form()
        {
            InitializeComponent();
        }

        public Login_Form(PanneauPrincipal.PanneauPrincipal sourceform)
        {
            dp = sourceform as PanneauPrincipal.PanneauPrincipal;
            InitializeComponent();
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBoxNomUtilisateur1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Form_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BDD.Connecteur db = new BDD.Connecteur();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select*from Controle ", db.getconnexion());
            adapter.SelectCommand = cmd;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {
                PanelControle.BringToFront();
            }
            else
            {
                PanelCreeCompte.BringToFront();
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {
            PanelLogin.BringToFront();
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (TextBoxMotdepasse1.UseSystemPasswordChar)
            {
                TextBoxMotdepasse1.UseSystemPasswordChar = false;
            }
            else
            {
                TextBoxMotdepasse1.UseSystemPasswordChar = true;
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            BDD.Connecteur db = new BDD.Connecteur();



            string nomutilisateur = TextBoxNomUtilisateur1.Text;
            string motdepasse = TextBoxMotdepasse1.Text;

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select*from Utilisateur where nom_Utilisateur = @nom_utilisateur and Mot_de_Passe = @Mot_de_Passe", db.getconnexion());

            cmd.Parameters.Add("@Nom_Utilisateur", SqlDbType.VarChar).Value = nomutilisateur;
            cmd.Parameters.Add("@Mot_de_Passe", SqlDbType.VarChar).Value = motdepasse;

            adapter.SelectCommand = cmd;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {
                dp.Enabled = true;
                this.Close();
            }
            else
            {
                if (nomutilisateur.Trim().Equals(""))
                {
                    MessageBox.Show("Mettez votre nom d'utlisateur pour vous conncter", "Nom d'utilisateur vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (motdepasse.Trim().Equals(""))
                {
                    MessageBox.Show("Mettez votre mot de passe pour vous conncter", "Mot de passe vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("Le nom d'utilisateur ou le mot de passe est incorrect", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            BDD.Connecteur db = new BDD.Connecteur();

            string Motdepasse = TextBoxCodeSecret.Text;


            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select*from Controle where Mot_de_Passe = @Mot_de_Passe", db.getconnexion());

            cmd.Parameters.Add("@Mot_de_Passe", SqlDbType.VarChar).Value = Motdepasse;

            adapter.SelectCommand = cmd;
            adapter.Fill(table);


            if (table.Rows.Count > 0)
            {
                PanelCreeCompte.BringToFront();
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
                    TextBoxCodeSecret.Text = "";
                    PanelLogin.BringToFront();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PanelLogin.BringToFront();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {

        }
    }

}

