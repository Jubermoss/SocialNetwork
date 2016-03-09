using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Linq;



namespace Red_Social_Data
{
    public class AlbumLINQ:ImagenLINQ
    {


        public bool agregarAlbum(string Nombre, int Propietario, int Tipo) 
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Album al = new Album();
            al.album_nombre= Nombre;
            tw.Albums.InsertOnSubmit(al);
            al.usuario_id= Propietario;
            tw.Albums.InsertOnSubmit(al);
            al.tipo = Tipo;
            tw.Albums.InsertOnSubmit(al);
            
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

        public bool buscarAlbum(string Nombre)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Album al = null;
            bool result = false;

            try
            {
                al = tw.Albums.Single(p => p.album_nombre== Nombre);
                if (al != null)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                tw.Dispose();
            }

            return result;
        }


        public IEnumerable<Album> getAlbum()
        {
            TwitterDataContext tw = new TwitterDataContext();
            var consulta = from p in tw.Albums
                           select p;
            return consulta.ToList();
        }


        public int GetAlbumId(string Nombre)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Album us = null;
            int id = 0;
            try
            {
                us = tw.Albums.Single(p => p.album_nombre == Nombre);
                if (us != null)
                {
                    id = us.album_id;
                }
            }
            finally
            {
                tw.Dispose();
            }

            return id;
        }

   



       



    }
}
