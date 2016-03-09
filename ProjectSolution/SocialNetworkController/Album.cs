using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;
using Red_Social_Data;
using System.Data.SqlClient;
using System.Data;


namespace Red_Social_Controller
{
    public class Album : Imagen
    {

        private string nombre;
        private int propietario;
        private int tipo;


        public void validarNombre(StringDictionary Nombre)
        {
            if (Nombre["Nombre"] == null || Nombre["Nombre"] == "")
            {
                throw new ApplicationException("El Nombre es obligatorio");
            }
            else if (Nombre["Nombre"].Length > 20)
            {
                throw new ApplicationException("El nombre rebasa los 20 caracteres");
            }
            else
            {
                this.nombre = Nombre["Nombre"];
            }
        }
       
        public void asiganarPropietario(int Propietario) 
        {
            this.propietario = Propietario;
        }

        public void validarTipo(int Tipo)
        {
            if (Tipo > 1)
            {
                throw new ApplicationException("Perfil Incorrecto");
            }
            else
            {
                this.tipo = Tipo;
            }
        }


        public void verificarDisponibilidad(string Nombre)
        {
            AlbumLINQ al = new AlbumLINQ();
            if (al.buscarAlbum(Nombre))
            {
                throw new ApplicationException("Ese Album ya existe");
            }
        }


        public bool crearAlbum() 
        {
            AlbumLINQ al=new AlbumLINQ();
            if (al.agregarAlbum(this.nombre, this.propietario, this.tipo))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }


        public bool agregarImagen(int idAlbum, byte[] imagen) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            ImagenLINQ im = new ImagenLINQ();
            if (im.insertarImagen(idAlbum, imagen))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }


        public IEnumerable<Red_Social_Data.Album> getAlbum()
        {
            AlbumLINQ al = new AlbumLINQ();
            return al.getAlbum();
        }


        public int getAlbumId(string Nombre)
        {
            int id;
            AlbumLINQ usu = new AlbumLINQ();
            id = usu.GetAlbumId(Nombre);
            return id;
        }

    }
}
