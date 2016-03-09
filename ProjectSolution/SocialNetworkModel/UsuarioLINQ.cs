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
    public class UsuarioLINQ 
    {   
        
        public bool agregarUsuario (string alias, string password, byte[] image,int tipoPerfil)
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = new Usuario();
            us.alias = alias;
            tw.Usuarios.InsertOnSubmit(us);
            us.password = password;
            tw.Usuarios.InsertOnSubmit(us);
            us.foto_usuario = image;
            tw.Usuarios.InsertOnSubmit(us);
            us.status = tipoPerfil;
            tw.Usuarios.InsertOnSubmit(us);
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


        public bool buscarUsuario(string Alias)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            bool result = false;

            try
            {
                us = tw.Usuarios.Single(p => p.alias == Alias);
                if (us != null)
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


        public  IEnumerable<Usuario> getAllUsers()
        {
            TwitterDataContext tw=new TwitterDataContext();
            var consulta = from p in tw.Usuarios
                           select p;
            return consulta.ToList();
        }
   

        public bool verificarDatos(string Alias, string Password) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            bool result = false;
            try
            {
                us = tw.Usuarios.Single(p => p.alias == Alias);
                if (us != null)
                {
                    if (us.password == Password)
                    {
                        result = true;//Datos correctos
                    }
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

        public int GetUsuarioId(string Alias) 
        {
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            int id=0;
            try
            {
                us = tw.Usuarios.Single(p => p.alias == Alias);
                if (us != null)
                {
                    id = us.usuario_id;
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


        public string getUsuarioAlias(int Id)
        {
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            string Alias="";
            try
            {
                us = tw.Usuarios.Single(p => p.usuario_id == Id);
                if (us != null)
                {
                    Alias = us.alias;
                }
            }
            finally
            {
                tw.Dispose();
            }
            return Alias; 
        }


        public bool cambiarContraseña(int usuario, string password) 
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = new Usuario();
            us = tw.Usuarios.Single(p => p.usuario_id == usuario);
            if (us != null)
            {
                us.password = password;
                result = true;
            }
            
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


        public bool cambiarImagen(int usuario, byte[]imagen) 
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = new Usuario();
            us = tw.Usuarios.Single(p => p.usuario_id == usuario);
            if (us != null)
            {
                us.foto_usuario = imagen;
                result = true;
            }

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


        public bool actualizarPerfil(int idUser, int perfil) 
        {
            bool result = false;
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = new Usuario();
            us = tw.Usuarios.Single(p => p.usuario_id == idUser);
            if (us != null)
            {
                us.status = perfil;
                result = true;
            }

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

        public bool revisarStatus( int idUser) 
        {
            bool disponible = false;
            TwitterDataContext tw = new TwitterDataContext();
            Usuario us = null;
            us = tw.Usuarios.Single(p => p.usuario_id == idUser);
            if (us != null)
            {
                if (us.status == 1) 
                {
                    disponible = true;
                }    
            }
            else
            {
               
                disponible = false;
            }
            return disponible;   
        }











    }
}
