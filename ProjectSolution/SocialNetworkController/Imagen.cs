using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO; 
using System.Drawing;
using Red_Social_Data;

namespace Red_Social_Controller
{
    public class Imagen
    {
        private byte[] imagen;
       

        
        public byte[] convertirAbits(FileStream im)
        {
                byte[] filebyte = new byte[im.Length];
                im.Read(filebyte, 0, (int)im.Length);
                im.Close();
                this.imagen = filebyte;
                return this.imagen;
        }

      
        public Bitmap mostrarImagen(int idUsuario)
        {
            ImagenLINQ ima = new ImagenLINQ();
            byte[] picture = ima.obtenerImagenUsuario(idUsuario);
            MemoryStream ms= new MemoryStream();
            ms.Write(picture, 0, picture.Length);
            Bitmap foto = new Bitmap(ms);
            return foto;      
        }


        public Bitmap mostrarColeccion(int Imagen)
        {
            ImagenLINQ ima = new ImagenLINQ();
            byte[] picture = ima.getImagen(Imagen);
            MemoryStream ms = new MemoryStream();
            ms.Write(picture, 0, picture.Length);
            Bitmap foto = new Bitmap(ms);
            return foto;
        }

       
        public Bitmap mostrarImagenAlbum(int idAlbum)
        {
            ImagenLINQ ima = new ImagenLINQ();
            byte[] picture = ima.getImagensAlbum(idAlbum);
            MemoryStream ms = new MemoryStream();
            try
            {
                ms.Write(picture, 0, picture.Length);
                Bitmap foto = new Bitmap(ms);
                return foto;
            }
            catch 
            {
                Bitmap vacio=null;
                return vacio;
            }
        }



        public IEnumerable obtenerNumImagens(int idAlbum) 
        {

            ImagenLINQ im = new ImagenLINQ();
            return im.getNumImagensAlbum(idAlbum);
        }



    }
}
