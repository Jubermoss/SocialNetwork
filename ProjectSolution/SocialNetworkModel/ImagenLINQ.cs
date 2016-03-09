using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq;
using System.Collections.Specialized;
using System.Collections;


namespace Red_Social_Data
{
    public class ImagenLINQ
    {

        public byte[] obtenerImagenUsuario(int idUsuario) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            Binary bits=null;
            byte[] picture=null;
            try
            {
                us = tw.Usuarios.Single(p => p.usuario_id == idUsuario);
                if (us != null)
                {
                    bits = us.foto_usuario;
                    picture = bits.ToArray();
                }
            }
            finally
            {
                tw.Dispose();
            }
            return picture;
        
        }

        public bool insertarImagen(int idAlbum, byte[] imagen) 
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Imagen im = new Imagen();
            im.album_id = idAlbum;
            tw.Imagens.InsertOnSubmit(im);
            im.imagen1 = imagen;
            try
            {
                tw.SubmitChanges();
                result = true;
            }
            finally
            {
                tw.Dispose();
            }
            return result;
        }


        public byte[] getId_IMagens(int idAlbum) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            Imagen im = null;
            Binary bits = null;
            byte[] picture = null;
           
            try
            {
                im = tw.Imagens.Single(p => p.album_id == idAlbum);
                
                if (im != null)
                {
                    //im = tw.Imagens.Single(p => p.imagen_id == idImagen);
                    bits = im.imagen1;
                    picture = bits.ToArray();
                }
            }
            finally
            {
                tw.Dispose();
            }
            return picture;
        }


        public byte[] getImagensAlbum(int idAlbum) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            Imagen im = null;
            Binary bits = null;
            byte[] picture = null;
            try
            {
                im = tw.Imagens.Single(p => p.album_id == idAlbum && p.imagen_id == 1);
                if (im != null)
                {
                    bits = im.imagen1;
                    picture = bits.ToArray();
                }
            }
            catch 
            {
                tw.Dispose();
            }
            finally
            {
                tw.Dispose();
            }
            return picture;
        
        
        }



        public byte[] getImagen(int idImgen)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Imagen im = null;
            Binary bits = null;
            byte[] picture = null;
            try
            {
                im = tw.Imagens.Single(p => p.imagen_id== idImgen);
                while (im != null)
                {
                    bits = im.imagen1;
                    picture = bits.ToArray();


                }
            }
            finally
            {
                tw.Dispose();
            }
            return picture;


        }



        public  IEnumerable getNumImagensAlbum(int idAlbum)
        {
            TwitterDataContext tw = new TwitterDataContext();

            var consulta = from im in tw.Imagens
                           where (im.album_id == idAlbum)
                           select new {im.imagen1};
            return consulta.ToArray();


        }





    }
}
