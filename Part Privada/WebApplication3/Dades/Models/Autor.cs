using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    public class Autor : Persona
    {
        public Autor()
        {

        }
        public Autor(Adreça Adreça, string nif, string nom, int edat) : base(Adreça, nif, nom, edat)
        {

        }
    }
}
