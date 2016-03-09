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
    public class Usuario 
    {

        private string alias;
        private string password;
        private byte[] imagen;
        private int status;

        
        public Usuario() { }
        
        
        public void validarAlias(StringDictionary Alias) 
        {  
            if (Alias["Alias"] == null || Alias["Alias"] == "")
            {
                throw new ApplicationException("El campo Alias es obligatorio");
            }
            else if (Alias["Alias"].Length > 20)
            {
                throw new ApplicationException("El Alias debe ser menor a 20 caracteres");
            }
            else 
            {
                this.alias = Alias["Alias"];
            }             
        }

        public void validarPassword(StringDictionary Password) 
        {
            if (Password["Password"] == null || Password["Password"] == "")
            {
                throw new ApplicationException("El campo Password es obligatorio");
            }
            else if (Password["Password"].Length > 10)
            {
                throw new ApplicationException("Password debe ser menor a 10 caracteres");
            }
            else
            {
                this.password = Password["Password"];
            }            
        }

        public void asignarImagen(byte[]Imagen) 
        {
            this.imagen = Imagen;
        }

        public bool cambiarImagen(int id) 
        {
            UsuarioLINQ us = new UsuarioLINQ();
            if (us.cambiarImagen(id, this.imagen))
            {
                return true;
            }
            else 
            {
                return false;
            }
        
        }

        public void validarPerfil(int Perfil) 
        {
            if (Perfil>1)
            {
                throw new ApplicationException("Perfil Incorrecto");
            }
            else 
            {
                this.status = Perfil;
            }
        }

        public bool CrearUsuario()
        {
            UsuarioLINQ us = new UsuarioLINQ();    
            if(us.agregarUsuario(this.alias, this.password, this.imagen,this.status))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void verificarDisponibilidad(string Alias) 
        {
            UsuarioLINQ usu = new UsuarioLINQ();
            if (usu.buscarUsuario(Alias))
            {
                throw new ApplicationException("Ese alias ya existe");
            } 
        }

        public void Autenticar(string Alias, string Password) 
        {
            UsuarioLINQ us = new UsuarioLINQ();
            if (!us.verificarDatos(Alias, Password))
            {
                throw new ApplicationException("Usuario y/o Password incorrectos");
            }
        }

        public int getId(string Alias) 
        {
            int id;
            UsuarioLINQ usu = new UsuarioLINQ();
            id=usu.GetUsuarioId(Alias);
            return id;
        }

        public string getAlias(int Id) 
        {
            string Alias;
            UsuarioLINQ usu = new UsuarioLINQ();
            Alias = usu.getUsuarioAlias(Id);
            return Alias;
        }

        public bool asignarContraseña(int id) 
        {
            UsuarioLINQ us = new UsuarioLINQ();
            if (us.cambiarContraseña(id, this.password))
            {
                return true;
            }
            else 
            {
                return false;
            }
        
        }

        public bool cambiarPerfil(int id)
        {
            UsuarioLINQ us = new UsuarioLINQ();
            if (us.actualizarPerfil(id,this.status))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public  IEnumerable<Red_Social_Data.Usuario> getUsers() 
        {
            UsuarioLINQ us = new UsuarioLINQ();
            us.getAllUsers();
            return us.getAllUsers();
        }

        public bool revisarPerfil(int Id) 
        {
            UsuarioLINQ us = new UsuarioLINQ();
            if(us.revisarStatus(Id))
            {
                return true;
            }
            else
            {
            return false;
            }
        }
 






    }
}
