using Dades.Context;
using Dades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Gestor
{
    public class GestorBD
    {
       private PersonaContext db = new PersonaContext();



        public bool validar(string Email, string Password)
        {
            if(db.Usuaris.Select(a => a).Where(a => a is Administrador && a.email.Equals(Email) && a.password.Equals(Password)).Count() > 0)
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
