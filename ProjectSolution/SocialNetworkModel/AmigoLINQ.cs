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
    public class AmigoLINQ:UsuarioLINQ
    {

        public IQueryable getSolicitudes(int Usr) 
        {
           TwitterDataContext tw = new TwitterDataContext();
           var consulta= from a in tw.Amigos
                         where( a.estado==0 && a.usuario_seguido==Usr)
                         select new {a.alias_seguidor};
           return consulta;
        }
        

        public IQueryable getAllFriends(int Usr) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            var consulta = from a in tw.Amigos
                           where (a.estado == 1 && a.usuario_seguido == Usr)
                           select new {Num_Amigo=a.amigo_id,a.alias_seguidor,a.alias_seguido};

            return consulta;
        }
       

        public int getIdAmigo(int usSeguido, int usSeguidor)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Amigo am = null;
            int id = 0;
            try
            {
                am = tw.Amigos.Single(p => p.usuario_seguido == usSeguido && p.usuario_seguidor == usSeguidor);
                if (am != null)
                {
                        id = am.amigo_id;
                }
            }
            catch
            {
                id = 0;
            }
            finally
            {
                tw.Dispose();
            }

            return id;
        }


        public bool eliminarAmigo(int usSeguido, int usSeguidor)
        {
            int friend=getIdAmigo(usSeguido, usSeguidor);
            TwitterDataContext tw = new TwitterDataContext();
            Amigo ami = null;
            bool result = false;
            try
            {
                ami = tw.Amigos.Single(p => p.amigo_id==friend);
                if (ami != null)
                {
                        tw.Amigos.DeleteOnSubmit(ami);
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


        public bool cambiarEstado(int usSeguido, int usSeguidor)
        {
            int friend = getIdAmigo(usSeguido, usSeguidor);
            TwitterDataContext tw = new TwitterDataContext();
            Amigo ami = null;
            bool result = false;
            try
            {
                ami = tw.Amigos.Single(p => p.amigo_id == friend);
                if (ami != null)
                {
                    ami.estado = 1;
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

        public bool insetarSolicitud(int usSeguido, int usSeguidor, string alias_segido, string alias_seguidor)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Amigo am = new Amigo();
            bool result = false;
            
                am.usuario_seguido = usSeguido;
                tw.Amigos.InsertOnSubmit(am);
                am.usuario_seguidor = usSeguidor;
                tw.Amigos.InsertOnSubmit(am);
                am.estado = 0;
                tw.Amigos.InsertOnSubmit(am);
                am.alias_seguido = alias_segido;
                tw.Amigos.InsertOnSubmit(am);
                am.alias_seguidor = alias_seguidor;
                try
                {
                    tw.SubmitChanges();
                    result = true;
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

        public  bool vereficarExistenciaSolicitud(int usSeguido, int usSeguidor) 
        {
            bool existe = false;
            TwitterDataContext tw = new TwitterDataContext();
            
            var consulta= from a in tw.Amigos
                          where(a.usuario_seguido==usSeguido && a.usuario_seguidor==usSeguidor)
                          select a;
            if (consulta.ToList().Count>=1)
            {
                existe = true;
            }
            else 
            {
                existe = false;
            }
            return existe;

        }
        
        






    }
}
