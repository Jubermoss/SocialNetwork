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
using System.Collections;
using System.Collections.ObjectModel;





namespace Red_Social
{
    public partial class Form1 : Form
    {
        
        

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCargarFoto_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
            pictureBox1.ImageLocation = ofdImagen.FileName; 
        }

        private void buttonAuntenticar_Click(object sender, EventArgs e)
        {     
            labelErrorAutent.Text = "";
            Usuario us = new Usuario();
            StringDictionary Alias = new StringDictionary();//Validar Formato Alias
            Alias.Add("Alias",textBoxAutAlias.Text);
            try
            {
                us.validarAlias(Alias);
            }
            catch (Exception ex)
            {
                labelErrorAutent.Text = ex.Message;
                return;
            }

            StringDictionary Password = new StringDictionary();//Validar Formato Password
            Password.Add("Password", textBoxAutPass.Text);
            try
            {
                us.validarPassword(Password);
            }
            catch (Exception ex)
            {
                labelErrorAutent.Text = ex.Message;
                return;
            }

            try
            {
                us.Autenticar(textBoxAutAlias.Text, textBoxAutPass.Text);
            }
            catch (Exception ex)
            {
                labelErrorAutent.Text = ex.Message;
                return;
            }
            tabControlMenu.Visible = true;
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            
           
        }

       
        private void buttonCrearUsuario_Click(object sender, EventArgs e)
        {
            labelErrorCrear.Text = "";
            
            Usuario us = new Usuario();

            StringDictionary Alias = new StringDictionary();//Validar Formato Alias
            Alias.Add("Alias", textBoxAlias.Text);
            try
            {
                us.validarAlias(Alias);
            }
            catch (Exception ex)
            {
                labelErrorCrear.Text = ex.Message; 
                return;
            }
            
            StringDictionary Password = new StringDictionary();//Validar Formato Password
            Password.Add("Password", textBoxPass.Text);
            try
            {
                us.validarPassword(Password);
            }
            catch (Exception ex)
            {
                labelErrorCrear.Text = ex.Message;
                return;
            }


            int perfil = Form1.conversion(comboBoxPerfil.Text);//Validar el perfil
            try 
            {
                us.validarPerfil(perfil);
            }
            catch (Exception ex)
            {
                labelErrorCrear.Text = ex.Message;
                return;
            }
                          
            Imagen im = new Imagen();
            try                                       //Validar existencia de imagen
            {                               
                FileStream fls = new FileStream(pictureBox1.ImageLocation, FileMode.Open);
                byte[] imagen = im.convertirAbits(fls);
                us.asignarImagen(imagen);
            }
            catch (Exception)
            {
                labelErrorCrear.Text ="Debe seleccionar una imagen";
                return;
            }

            try
            {
                us.verificarDisponibilidad(textBoxAlias.Text);// Validar disponibilidad de Alias
            }
            catch (Exception ex)
            {
                labelErrorCrear.Text = ex.Message;
                return;    
            }

            if (us.CrearUsuario())
            {
                labelErrorCrear.Text = "Usuario Creado";
                return;
            }
            else 
            {
                labelErrorCrear.Text = "No se pudo crear la cuenta";
                return;
            }
        }



        private void buttonHcerAmigos_Click(object sender, EventArgs e)
        {
            groupBoxHcerAmigos.Enabled = true;
            Usuario usu = new Usuario();
            comboBoxHacerAmigos.DataSource = usu.getUsers();
            comboBoxHacerAmigos.DisplayMember = "alias";
            comboBoxHacerAmigos.ValueMember = "usuario_id";
        }

     


        private void buttonCerrarSesion_Click(object sender, EventArgs e)
        {
          
            labelBienvenida.Text = "";
            pictureBoxPerfil.Image = null;
            dataGridView3.DataSource = null;
            groupBox2.Enabled = true;
            groupBox1.Enabled = true;
            tabControlMenu.Visible= false;

            comboBoxEliminarAmigo.DisplayMember = "";
            comboBoxSolicitudes.DisplayMember = "";
            dataGridViewAmigos.DataSource = "";
            dataGridViewSolicitudesP.DataSource = "";
            


        }

        private void tabPagePerfil_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int id = us.getId(textBoxAutAlias.Text);
            labelBienvenida.Text = "Bienvenido(a) " + us.getAlias(id) + " !!";
            Imagen im = new Imagen();
            pictureBoxPerfil.Image=im.mostrarImagen(id);






        }









        public static int conversion(string Perfil)
        {
            int valor = 3;
            if (Perfil == "Publico") valor = 1;
            else if (Perfil == "Privado") valor = 0;
            else valor = 3;
            return valor;
        }



        private void buttonVisitarPerfil_Click(object sender, EventArgs e)
        {
            PerfilAmigo forma = new PerfilAmigo();
            forma.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AgregarFotos fotos = new AgregarFotos();
            fotos.Show();
        }

        private void buttonCrearAlbum_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;

        }

        private void buttonAgregarAlbum_Click(object sender, EventArgs e)
        {
            labelmensajeAlbum.Text = "";
            Album al = new Album();


            StringDictionary Nombre = new StringDictionary();//Validar Formato Nombre
            Nombre.Add("Nombre", textBoxNombreAlbum.Text);
            try
            {
                al.validarNombre(Nombre);
            }
            catch (Exception ex)
            {
                labelmensajeAlbum.Text = ex.Message;
                return;
            }


            int Tipo = Form1.conversion(comboBoxTipoAlbum.Text);//Validar El tipo de Album
            try
            {
                al.validarTipo(Tipo);
            }
            catch (Exception ex)
            {
                labelmensajeAlbum.Text = ex.Message;
                return;
            }

            try
            {
                al.verificarDisponibilidad(textBoxNombreAlbum.Text);// Validar disponibilidad del Nombre
            }
            catch (Exception ex)
            {
                labelmensajeAlbum.Text = ex.Message;
                return;
            }

            Usuario us = new Usuario();
            int id = us.getId(textBoxAutAlias.Text); //se obtiene el propietario del album
            al.asiganarPropietario(id);

            if (al.crearAlbum())
            {
                MessageBox.Show("Album Creado");
                groupBox3.Enabled = false;
                return;
            }
            else
            {
                labelErrorCrear.Text = "No se pudo crear el Album";
                return;
            }


        }

        private void buttonCambiarContraseña_Click(object sender, EventArgs e)
        {
            groupBoxNvaContraseña.Visible= true;
            buttonCambiarContraseña.Visible = false;
        }

        private void buttonNuevaContraseña_Click(object sender, EventArgs e)
        {
            labelNvaContraseña.Text="";
           
            Usuario us= new Usuario();

            StringDictionary Password = new StringDictionary();//Validar Formato Password
            Password.Add("Password", textBoxNvaContraseña.Text);
            try
            {
                us.validarPassword(Password);
            }
            catch (Exception ex)
            {
                labelNvaContraseña.Text = ex.Message;
                return;
            }

            int id = us.getId(textBoxAutAlias.Text); //se obtiene el propietario del album
            if (us.asignarContraseña(id))
            {
                MessageBox.Show("Haz cambiado tu contraseña");
                textBoxNvaContraseña.Text = "";
                groupBoxNvaContraseña.Visible= false;
                buttonCambiarContraseña.Visible = true;
                return;
            }
            else 
            {
                labelNvaContraseña.Text = "No se pudo realizar la operacion";
                return;
            }





        }

        private void buttonCambiarImagen_Click(object sender, EventArgs e)
        {
            ofdImagen.ShowDialog();
            pictureBoxPerfil.ImageLocation = ofdImagen.FileName;
            
            Usuario us = new Usuario();
            
            Imagen im = new Imagen();
            try                                       //Validar existencia de imagen
            {
                FileStream sw = new FileStream(pictureBoxPerfil.ImageLocation, FileMode.Open);
                byte[] imagen = im.convertirAbits(sw);
                us.asignarImagen(imagen);
            }
            catch (Exception)
            {
                labelErrorCrear.Text = "Debe seleccionar una imagen";
                return;
            }

            int id = us.getId(textBoxAutAlias.Text);

            if (us.cambiarImagen(id)) 
            {

                MessageBox.Show("Haz cambiado tu imagen");
                return;
            }
            if (us.cambiarImagen(id))
            {

                MessageBox.Show("No se pudo cambiar tu inmagen");
                return;
            }






        }

        private void button4_Click(object sender, EventArgs e)
        {   
            Usuario usu = new Usuario();
            comboBoxAliasMens.Visible = true;
            groupBoxMensajes.Enabled = true;
            comboBoxAliasMens.Enabled = true;
            comboBoxAliasMens.DataSource = usu.getUsers();
            comboBoxAliasMens.DisplayMember = "alias";
            comboBoxAliasMens.ValueMember = "usuario_id";
            richTextBox1.Enabled = true;
            button8.Enabled = true;
            comboBoxNumMensaje.Enabled = false;
          
            button9.Enabled = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            groupBoxMensajes.Enabled = true;
            comboBoxNumMensaje.Enabled = true;
            button9.Enabled = true;
            comboBoxAliasMens.Visible = false;
            richTextBox1.Enabled= false;            
            button8.Enabled = false;

            Mensaje men=new Mensaje();
            Usuario us=new Usuario();

            comboBoxNumMensaje.DataSource =  men.getMesaggesNums(us.getId(textBoxAutAlias.Text)).ToList();
            comboBoxNumMensaje.DisplayMember = "mensaje_id";


        }

        private void button8_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            Mensaje men = new Mensaje();
            string cadena=richTextBox1.Text;
            int idRemitente = us.getId(textBoxAutAlias.Text);
            string Remitente = textBoxAutAlias.Text;
            int idDestinatario = us.getId(comboBoxAliasMens.Text);
            string Destinatario = comboBoxAliasMens.Text;

            StringDictionary Descripcion = new StringDictionary();//Validar Contenido del Mensaje
            Descripcion.Add("Descripcion", richTextBox1.Text);
            try
            {
                men.validarMensaje(Descripcion);
            }
            catch (Exception ex)
            {
                labelAvisoMensajes.Text= ex.Message;
                return;
            }


            try
            {
                men.mandarMensaje(idRemitente,idDestinatario, cadena,Remitente,Destinatario);
                MessageBox.Show("mensaje enviado");
                richTextBox1.Text = "";
                labelAvisoMensajes.Text = "";
                groupBoxMensajes.Enabled = false;
                return;

            }
            catch
            {
                MessageBox.Show("Error en el envio");
                richTextBox1.Text = "";
                labelAvisoMensajes.Text = "";
                groupBoxMensajes.Enabled = false;
                return;
            
            }
        }

       

        private void button9_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            Mensaje men = new Mensaje();
            int idMensaje;
            try
            {
                 idMensaje = int.Parse(comboBoxNumMensaje.Text);
            }
            catch 
            {
                
                MessageBox.Show("No se  Elimino el mensaje");
                comboBoxNumMensaje.DataSource = null;
                groupBoxMensajes.Enabled = false;
                return;
            }

            int id = us.getId(textBoxAutAlias.Text);
            
                if (men.eliminar(id,idMensaje))
                {
                    MessageBox.Show("Mensaje Eliminado");
                    comboBoxNumMensaje.DataSource = null;
                    groupBoxMensajes.Enabled = false;

                }
               
             else
            {
                MessageBox.Show("No se  Elimino el mensaje");
                groupBoxMensajes.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            button2.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label13.Text = "";
            Usuario us = new Usuario();
            int perfil = Form1.conversion(comboBoxNuevoPerfil.Text);//Validar el perfil
            try
            {
                us.validarPerfil(perfil); 
            }
            catch (Exception ex)
            {
                label13.Text = ex.Message;
                return;
            }

            int idUsuario = us.getId(textBoxAutAlias.Text);
            if (us.cambiarPerfil(idUsuario))
            {
                MessageBox.Show("haz cambiado tu perfil");
                groupBox4.Visible = false;
                button2.Visible = true;
            }
            else 
            {

                MessageBox.Show("No se pudo realizar la operacion");
                groupBox4.Visible = false;
                button2.Visible = true;
            }
            
        }

        private void VerAlbum_Click(object sender, EventArgs e)
        {
            comboBoxVerAlbum.Enabled = true;
            button1.Enabled = true;
            Album al = new Album();
            al.getAlbum();
            comboBoxVerAlbum.DataSource = al.getAlbum();
            comboBoxVerAlbum.DisplayMember = "album_nombre";
        }

        private void buttonMisMensajes_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            Mensaje men = new Mensaje();
            dataGridView3.DataSource = men.getMesagges(idUsuario);
        }

      

        private void button11_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            Mensaje men = new Mensaje();
            dataGridView3.DataSource = men.getMesaggesWritted(idUsuario);





        }

        private void tabPageMensajes_Click(object sender, EventArgs e)
        {

        }

        private void buttonAtenderSolicitud_Click(object sender, EventArgs e)
        {
            groupBoxSolicitudes.Enabled = true;
            Usuario us = new Usuario();
            Amigo ami = new Amigo();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            comboBoxSolicitudes.DataSource = ami.Solicitudes(idUsuario);
            comboBoxSolicitudes.DisplayMember = "alias_seguidor";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            Amigo ami = new Amigo();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            dataGridViewSolicitudesP.DataSource = ami.Solicitudes(idUsuario);

        }

        private void tabPageAmigos_Click(object sender, EventArgs e)
        {

        }

        private void buttonSolicitarAmigo_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idSeguidor = us.getId(textBoxAutAlias.Text);
            string seguidor=textBoxAutAlias.Text;
            int idSeguido = us.getId(comboBoxHacerAmigos.Text);
            string seguido = comboBoxHacerAmigos.Text;
            Amigo ami = new Amigo();

            if (ami.verfificarSolicitud(idSeguido, idSeguidor))
            {
                MessageBox.Show("ERROR:\n-Ya hiciste esa solicitud antes");
                comboBoxSolicitudes.DataSource = null;
                groupBoxHcerAmigos.Enabled = false;
                return;
            }
            if (ami.solicitarAmigo(idSeguido, idSeguidor, seguido, seguidor))
            {
                MessageBox.Show("Haz enviado una solicitud de amigos a " + seguido);
                comboBoxSolicitudes.DataSource = null;
                groupBoxHcerAmigos.Enabled = false;
            }
            else
            {
                MessageBox.Show("ERRor: \n-No se realizo la operación");
                comboBoxSolicitudes.DataSource = null;
                groupBoxHcerAmigos.Enabled = false;
            }


        }

        private void buttonVerAmigos_Click(object sender, EventArgs e)
        {
            Amigo ami = new Amigo();
            Usuario us = new Usuario();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            dataGridViewAmigos.DataSource = ami.ObtenerAmigos(idUsuario);
        }

        private void buttonEliminarAmigos_Click(object sender, EventArgs e)
        {
            groupBoxEliminarAmigos.Enabled = true;
            Amigo ami = new Amigo();
            Usuario us = new Usuario();
            int idUsuario = us.getId(textBoxAutAlias.Text);
            comboBoxEliminarAmigo.DataSource = ami.ObtenerAmigos(idUsuario);
            comboBoxEliminarAmigo.DisplayMember = "alias_seguidor";
        }

        private void buttonEliminarAmigo_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            int idSeguido = us.getId(textBoxAutAlias.Text);
            int idSeguidor = us.getId(comboBoxEliminarAmigo.Text);
            string friend = comboBoxEliminarAmigo.Text;


            Amigo ami = new Amigo();

            if (ami.eliminar(idSeguido, idSeguidor))
            {
                MessageBox.Show(friend+ " Ya no es tu amigo");
                comboBoxEliminarAmigo.DataSource = null;
                groupBoxEliminarAmigos.Enabled = false;
            }
            else 
            {
                MessageBox.Show("No se realizo la operacion");
                comboBoxEliminarAmigo.DataSource = null;
                groupBoxEliminarAmigos.Enabled = false;
            }
        }

        private void buttonAceptarSolicitud_Click(object sender, EventArgs e)
        {
            groupBoxSolicitudes.Enabled = true;
            Usuario us = new Usuario();
            int idSeguido = us.getId(textBoxAutAlias.Text);
            int idSeguidor = us.getId(comboBoxSolicitudes.Text);
            string friend = comboBoxSolicitudes.Text;

            Amigo ami = new Amigo();

            if (ami.agreagarAmigo(idSeguido, idSeguidor))
            {
                MessageBox.Show(friend + " y tu ya son amigos");
                comboBoxSolicitudes.DataSource = null;
                groupBoxSolicitudes.Enabled = false;
            }
            else
            {
                MessageBox.Show("No se realizo la operacion");
                comboBoxSolicitudes.DataSource = null;
                groupBoxSolicitudes.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelAlbum.Text = "";
            pictureBox2.Image = null;
            Album alb = new Album();
            int idAlbum = alb.getAlbumId(comboBoxVerAlbum.Text);
            Imagen im = new Imagen();
            if (im.mostrarImagenAlbum(idAlbum) == null)
            {
                labelAlbum.Text = "No se puede mostrar album";
                return;

            }
            else
            {
                pictureBox2.Image = im.mostrarImagenAlbum(idAlbum);
            }
        }

       
      

      

       
        
        
    }
}
