using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Red_Social_Controller;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Red_Social
{
    public partial class AgregarFotos : Form
    {
        public AgregarFotos()
        {
            InitializeComponent();
        }

        private void buttonBuscarFoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName; 
        }

        private void Subir_Click(object sender, EventArgs e)
        {
            Album alb = new Album();
            int idAlbum = alb.getAlbumId(comboBox1.Text);
            try                                       
            {                               
                FileStream fls = new FileStream(pictureBox1.ImageLocation, FileMode.Open);
                byte[] imagen = alb.convertirAbits(fls);
                alb.agregarImagen(idAlbum,imagen);
                MessageBox.Show("FOTO AGREGADA!!");
                this.Close();
            }
            catch (Exception)
            {
                label2.Text ="Debe seleccionar una imagen";
                return;
            }
        }

        private void AgregarFotos_Load(object sender, EventArgs e)
        {
            Album al = new Album();
            al.getAlbum();
            comboBox1.DataSource = al.getAlbum();
            comboBox1.DisplayMember = "album_nombre";
            comboBox1.ValueMember = "album_id";
        }

        
    }
}
