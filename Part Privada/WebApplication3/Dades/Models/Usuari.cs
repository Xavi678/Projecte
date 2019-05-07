using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    public class Usuari : Persona
    {
        
       
        public string email { get; set; }
        
        public string password { get; set; }
        public int telefon { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dataNaixement { get; set; }

        public Usuari()
        {

        }

        public Usuari(Adreça Adreça,string nif, string nom, int edat, string email, string password,int telefon, DateTime dataNaixement)
        {
            this.Adreça = Adreça;
            NIF = nif;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;
            this.dataNaixement = dataNaixement;
            this.telefon = telefon;

        }
    }
}
