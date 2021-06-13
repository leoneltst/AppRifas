using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using SeguripassWS.Tools;
using AppRifas.Tools;

namespace AppRifas
{
    public partial class Form1 : Form
    {
        Thread thread1;
        public Form1()
        {
            
            InitializeComponent();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.FromArgb(25, Color.Black);





            cargarBoletos();
            
            thread1 = new Thread(TwitchThread.DoWork);
            thread1.Start();

            
        }
        private void cargarBoletos()
        {
            string url = "";
            flowLayoutPanel1.Controls.Clear();
            List<dynamic> LV = ToolsBoletos.ListaBoletos();
            foreach (dynamic perfil in LV)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(perfil.UrlImage.ToString()))
                        url = "https://pbs.twimg.com/media/Epjih8YXIAQxpdW.jpg";
                    else url = perfil.UrlImage.ToString();
                    createPictureBoleto(perfil.TwitchName.ToString(), url, perfil.NumeroVoleto.ToString());
                }
                catch (Exception)
                {

                }

            }
            foreach (dynamic perfil in LV)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(perfil.UrlImage.ToString()))
                        url = "https://pbs.twimg.com/media/Epjih8YXIAQxpdW.jpg";
                    else url = perfil.UrlImage.ToString();
                    createPictureBoleto(perfil.TwitchName.ToString(), url, perfil.NumeroVoleto.ToString());
                }
                catch (Exception)
                {

                }

            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void createPicture(string Name, string url)
        {

            var picture = new PictureBox
            {
                Name = Name,
                Size = new Size(flowLayoutPanel1.Size.Width / 11, flowLayoutPanel1.Size.Height / 11),
            };
            picture.ImageLocation = url;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Paint += new PaintEventHandler((sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                string text = Name;
                FontFamily fontFamily = new FontFamily("Terminator Two");
                Font Font2 = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF textSize = e.Graphics.MeasureString(text, Font2);
                PointF locationToDraw = new PointF();
                locationToDraw.X = (picture.Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = (picture.Height) - (textSize.Height);

                e.Graphics.FillRectangle(Brushes.Black, (picture.Width / 2) - (textSize.Width / 2), (picture.Height) - (textSize.Height), textSize.Width, textSize.Height);
                e.Graphics.DrawString(text, Font2, Brushes.White, locationToDraw);
            });

            flowLayoutPanel1.Controls.Add(picture);

        }
        private void createPictureBoleto(string Name, string url, string  boleto)
        {

            var picture = new PictureBox
            {
                Name = Name,
                Size = new Size(flowLayoutPanel1.Size.Width / 5, flowLayoutPanel1.Size.Height / 5),
            };
            picture.ImageLocation = url;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Paint += new PaintEventHandler((sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                string text = Name;
                FontFamily fontFamily = new FontFamily("Terminator Two");
                Font Font2 = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
                SizeF textSize = e.Graphics.MeasureString(text, Font2);
                PointF locationToDraw = new PointF();
                locationToDraw.X = (picture.Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = (picture.Height) - (textSize.Height);

                e.Graphics.FillRectangle(Brushes.Black, (picture.Width / 2) - (textSize.Width / 2), (picture.Height) - (textSize.Height), textSize.Width, textSize.Height);
                e.Graphics.DrawString(text, Font2, Brushes.White, locationToDraw);

                locationToDraw.Y = 0;

                 textSize = e.Graphics.MeasureString("Boleto " + boleto, Font2);
                locationToDraw.X = (picture.Width / 2) - (textSize.Width / 2);
                e.Graphics.FillRectangle(Brushes.Black, (picture.Width / 2) - (textSize.Width / 2), 0, textSize.Width, textSize.Height);
                e.Graphics.DrawString("Boleto "+boleto, Font2, Brushes.White, locationToDraw);
            });

            flowLayoutPanel1.Controls.Add(picture);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (FilaEventos.LE.Count>0)
            {
                dynamic Aux= FilaEventos.LE.First();
                timer1.Enabled = false;

                switch (Aux.TipoEvento.ToString())
                {
                    case "RegistrarBoleto":
                        DialogResult dialogResult = MessageBox.Show("El "+Aux.TwitchName.ToString()+ " dice que te compro el boleto " + Aux.Boleto.ToString() + ", ¿El Pana si pagó?",
                            "Este compa gasto puntos?", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string R= ConexionMySQL.ConsultarString(ConsultasSQL.RegistrarBoleto(Aux.TwitchName.ToString(), Aux.Boleto.ToString()));
                            if (R==null)
                            {
                                MessageBox.Show("cuei no salio el boleto " + Aux.Boleto.ToString() + " de " + Aux.TwitchName.ToString() + " anotalo en tu lista personal:c");
                                FilaEventos.LSalidas.Add("El boleto " + Aux.Boleto.ToString() + " No quedo por fallas tecnicas @" + Aux.TwitchName.ToString() + " F en el chat amigos.");

                            }
                            else {
                                cargarBoletos();
                                FilaEventos.LSalidas.Add("El boleto " + Aux.Boleto.ToString() + " YA ES TUYO @" + Aux.TwitchName.ToString() + " UWU <3 para ti!!!.");
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            MessageBox.Show("´che bato tramposo uwun´t");
                            FilaEventos.LSalidas.Add("Tu boleto " + Aux.Boleto.ToString() + " Fue RECHAZADO @" + Aux.TwitchName.ToString()+" UWUN´t para ti.");
                        }

                        break;

                    case "URLChange":
                        cargarBoletos();
                        break;
                    default:
                        break;
                }
                FilaEventos.LE.RemoveAt(0);
                timer1.Enabled = true;
            }
        }
    }



}
