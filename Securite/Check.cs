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
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.Speech.Synthesis;

namespace Gestion_Entrepot.Securite
{
    
    public partial class Check : Form
    {
        SpeechSynthesizer voice;
        private SqlConnection con = new SqlConnection();
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        public Check()
        {
            InitializeComponent();
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30";
        }

        Classes.Check check = new Classes.Check();
        private void Check_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo Device in CaptureDevice)
            {
                ComboBoxDevice.Items.Add(Device.Name);
            }
            ComboBoxDevice.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();

            voice = new SpeechSynthesizer();
        }

        private void ButtonOpenCamera_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[ComboBoxDevice.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
            timer1.Start();

        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            PictureBoxCheck.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Check_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning == true)
                FinalFrame.Stop();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PictureBoxCheck.Image != null)
            {
                BarcodeReader reader = new BarcodeReader();
                Result result = reader.Decode((Bitmap)PictureBoxCheck.Image);
                if (result != null)
                {
                    string decode = result.ToString().Trim();
                    string Designation = decode;

                    BDD.Connecteur db = new BDD.Connecteur();
                    DataTable table1 = new DataTable();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("Select Designation , Validation from CheckSecurite where Designation = @Designation", db.getconnexion());

                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designation;

                    adapter.SelectCommand = cmd;
                    adapter.Fill(table1);

                    if (table1.Rows.Count > 0)
                    {
                        if (decode != null)
                        {
                            DataTable table = check.GetdataRetrait(Designation);
                            DataTable bleta = check.GetdataCheckSecurite(Designation);
                            DataTable Des = check.GetdataDesignation(Designation);

                            if(Des.Rows.Count>0)
                            {
                                if (FinalFrame.IsRunning == true)
                                {
                                    FinalFrame.Stop();
                                    voice.SelectVoiceByHints(VoiceGender.Female);
                                    voice.SpeakAsync("Attention!Ce bon d'effet personnel a deja ete utiliser!");
                                    MessageBox.Show("Ce BON D'EFFET PERSONNEL est deja Confirmer", "Check-Securite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Close();
                                }
                                
                            }
                            else
                            {
                                if (table.Rows.Count == bleta.Rows.Count)
                                {
                                    if (FinalFrame.IsRunning == true)
                                    {
                                        FinalFrame.Stop();

                                        MessageBox.Show("Autorisation Confirmer", "Verification Bon De Sortie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    pictureBox1.Image = Image.FromFile("../../Acteurs/ok.png");
                                    MemoryStream mss = new MemoryStream();
                                    pictureBox1.Image.Save(mss, pictureBox1.Image.RawFormat);
                                    Byte[] Controle = mss.ToArray();
                                    string Validteur = "Confirmer";
                                    DateTime Sortie = DateTime.Now;

                                    if (check.ModifierCheck(Designation, Validteur, Sortie, Controle))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Echec de Modifcation Check-Securite", "Check-Securite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }    
                        }
                    }
                    else
                    {

                        if(FinalFrame.IsRunning == true)
                        {
                            FinalFrame.Stop();
                            voice.SelectVoiceByHints(VoiceGender.Female);
                            voice.SpeakAsync("Attention!Ce bon d'effet personnel n'est pas reconnue dans cet entrepot!");
                            MessageBox.Show("BON DE SORTIE NON RECONNU!!!", "BON DE SORTIE ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            this.Close();
                        }
                          
                        
                    }

                }
            }

           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
