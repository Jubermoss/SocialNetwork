using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;
using Red_Social_Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;


namespace Red_Social_Controller
{
    public class Amigo:Usuario
    {

        public IQueryable Solicitudes(int idUser) 
        {
            AmigoLINQ am = new AmigoLINQ();
            return am.getSolicitudes(idUser);
        }

        public IQueryable ObtenerAmigos(int idUser)
        {
            AmigoLINQ am = new AmigoLINQ();
            return am.getAllFriends(idUser);
        }


        public bool eliminar(int seguido, int seguidor) 
        {
            AmigoLINQ ami = new AmigoLINQ();
            if (ami.eliminarAmigo(seguido, seguidor))
            {

                return true;
            }
            else 
            {
                return false;
            }
        }

        public bool agreagarAmigo(int seguido, int seguidor) 
        {
            AmigoLINQ ami = new AmigoLINQ();
            if (ami.cambiarEstado(seguido, seguidor))
            {

                return true;
            }
            else
            {
                return false;
            }
        
        
        }

        public bool solicitarAmigo(int usSeguido, int usSeguidor, string alias_segido, string alias_seguidor)
        {
            AmigoLINQ ami = new AmigoLINQ();
            if (ami.insetarSolicitud(usSeguido, usSeguidor, alias_segido, alias_seguidor))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }


        public bool verfificarSolicitud(int usSeguido, int usSeguidor)
        {
            AmigoLINQ ami = new AmigoLINQ();
            if (ami.vereficarExistenciaSolicitud(usSeguido, usSeguidor))
            {
                return true;
            }
            {
                return false;
            }

        }

    }
}
