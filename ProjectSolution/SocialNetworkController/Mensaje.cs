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

namespace Red_Social_Controller
{
    public class Mensaje
    {
       


        public void mandarMensaje(int remitente, int destinatario, string descripcion, string alias_remitente, string alis_destinatario)
        {
            MensajeLINQ men = new MensajeLINQ();
            if (!men.insertaMensaje(remitente, destinatario, descripcion,alias_remitente,alis_destinatario))
            {
                throw new ApplicationException("NO se envio el mensaje");
            }
        }

        public bool eliminar(int idUser,int mensaje) 
        {
            MensajeLINQ men = new MensajeLINQ();
            if (men.borraMensaje(idUser, mensaje))
            {
                return true;
            }
            else 
            {
                return false;
            
            }
        }

        public IQueryable getMesagges(int Iduser)
        {
            MensajeLINQ m = new MensajeLINQ();
            return m.misMensajes(Iduser);
        }

        public IQueryable getMesaggesWritted(int Iduser)
        {
            MensajeLINQ m = new MensajeLINQ();
            return m.misMensajesEscritos(Iduser);
        }

        public IEnumerable<Red_Social_Data.Mensaje> getMesaggesNums(int Iduser)
        {
            MensajeLINQ m = new MensajeLINQ();
            return m.misMensajesNums(Iduser);
        }

        public void validarMensaje(StringDictionary descripcion)
        {
            if (descripcion["descripcion"] == null || descripcion["descripcion"] == "")
            {
                throw new ApplicationException("El mensaje no puede estar vació");
            }
            else if (descripcion["descripcion"].Length > 150)
            {
                throw new ApplicationException("El Mensaje debe ser menor a 150 caracteres");
            }
           
        }







    }
}
