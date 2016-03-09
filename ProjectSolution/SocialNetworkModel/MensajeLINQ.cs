using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Red_Social_Data
{
      public  class MensajeLINQ
    {

          public bool insertaMensaje(int remitente, int destinatario, string descripcion, string alias_remitente, string alis_destinatario) 
          {
              TwitterDataContext tw = new TwitterDataContext();
              Mensaje men = new Mensaje();
              bool result = false;
              men.usuario_remitente_id = remitente;
              tw.Mensajes.InsertOnSubmit(men);
              men.usuario_destinatario_id = destinatario;
              tw.Mensajes.InsertOnSubmit(men);
              men.descripcion = descripcion;
              tw.Mensajes.InsertOnSubmit(men);
              men.alias_remitente = alias_remitente;
              tw.Mensajes.InsertOnSubmit(men);
              men.alias_destinatario = alis_destinatario;
              tw.Mensajes.InsertOnSubmit(men);
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


          public bool borraMensaje(int idUser,int idMensaje ) 
          {
              TwitterDataContext tw = new TwitterDataContext();
              Mensaje men = null;
              bool result = false;
              
              try
              {
                  men = tw.Mensajes.Single(p => p.usuario_destinatario_id == idUser && p.mensaje_id == idMensaje);
                  if (men != null)
                  {
                      tw.Mensajes.DeleteOnSubmit(men);
                      tw.SubmitChanges();
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



          public IQueryable misMensajes(int IdUser)
          {
              TwitterDataContext tw = new TwitterDataContext();
              var consulta = from m in tw.Mensajes
                             where m.usuario_destinatario_id == IdUser
                             select new { Id_Men = m.mensaje_id, Remitente = m.alias_remitente, Mensaje = m.descripcion };
              return consulta;
          }


          public IEnumerable<Mensaje> misMensajesNums(int IdUser)
          {
              TwitterDataContext tw = new TwitterDataContext();
              var consulta = from m in tw.Mensajes
                             where m.usuario_destinatario_id == IdUser
                             select m;
              return consulta.ToArray() ;
          }



          public IQueryable misMensajesEscritos(int IdUser)
          {
              TwitterDataContext tw = new TwitterDataContext();
              var consulta = from m in tw.Mensajes
                             where m.usuario_remitente_id == IdUser
                             select new { Id_Men = m.mensaje_id, Destinatario = m.alias_destinatario, Mensaje = m.descripcion };
              return consulta;
          }
    }

      
}
