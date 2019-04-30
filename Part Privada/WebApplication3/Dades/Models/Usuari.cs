using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    public class Usuari : Persona
    {

        public string email { get; set; }
        public string password { get; set; }

        public Usuari()
        {

        }

        public Usuari(Adreça Adreça,string nif, string nom, int edat, string email, string password)
        {
            this.Adreça = Adreça;
            NIF = nif;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;

        }
    }
}
