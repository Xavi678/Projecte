using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dades.Models
{
    [Table("Persones")]
    public class Persona
    {

        [Key]
        public string NIF { get; set; }
        public string nom { get; set; }
        public int edat { get; set; }
        




       [ForeignKey("Adreça")]
        public  int AdreçaID { set; get; }
        //[ForeignKey("NIF")]
       
        public virtual Adreça Adreça {set; get;}


        public Persona()
        {

        }
        public Persona(Adreça Adreça, string NIF, string nom, int edat)
        {
            this.Adreça = Adreça;
            this.NIF = NIF;
            this.nom = nom;
           this.edat = edat;
        }

        

    }
}