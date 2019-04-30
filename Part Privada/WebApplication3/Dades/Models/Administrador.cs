using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
   public class Administrador : Usuari
    {
        public Guid token;
        public Administrador()
        {

        }
        

        public Administrador(Adreça Adreça,string nif, string nom, int edat, string email, string password) : base(Adreça,nif,nom,edat,email,password)
        {
           /* NIF = nif;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;*/

        }
    }
}
