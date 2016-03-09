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
    public partial class PerfilAmigo : Form
    {
        public PerfilAmigo()
        {
            InitializeComponent();
        }

        private void PerfilAmigo_Load(object sender, EventArgs e)
        {
            Usuario usu = new Usuario();
            comboBoxAlias.DataSource = usu.getUsers();
            comboBoxAlias.DisplayMember = "alias";
        }

        private void buttonVerPerfil_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            Usuario us= new Usuario();
            int id = us.getId(comboBoxAlias.Text);
            if (us.revisarPerfil(id))
            {
                tabControlPerfil.Visible = true;
                groupBox1.Enabled = false;
            }
            else
            {
                label2.Text = "Perfil Privado";
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Image = null;
            label3.Text = "";
            dataGridView1.DataSource = null;
            tabControlPerfil.Visible = false;
            groupBox1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int id = us.getId(comboBoxAlias.Text);
            label3.Text = "Perfil de: " + us.getAlias(id) + " !!";
            Imagen im = new Imagen();
            pictureBox1.Image = im.mostrarImagen(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Amigo ami = new Amigo();
            Usuario us = new Usuario();
            int idUsuario = us.getId(comboBoxAlias.Text);
            dataGridView1.DataSource = ami.ObtenerAmigos(idUsuario);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            Amigo ami = new Amigo();
            int idUsuario = us.getId(comboBoxAlias.Text);
            dataGridView1.DataSource = ami.Solicitudes(idUsuario);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idUsuario = us.getId(comboBoxAlias.Text);
            Mensaje men = new Mensaje();
            dataGridView1.DataSource = men.getMesagges(idUsuario);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idUsuario = us.getId(comboBoxAlias.Text);
            Mensaje men = new Mensaje();
            dataGridView1.DataSource = men.getMesaggesWritted(idUsuario);


        }

       




    }
}
