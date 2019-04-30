using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
   public class Director : Persona
    {
        
        public Director()
        {

        }

        public Director(Adreça Adreça,string nif, string nom,int edat) : base(Adreça,nif,nom,edat)
        {

        }

        
    }
}
