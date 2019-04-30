using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    [Table("Adreces")]
    public class Adreça
    {
        public Adreça(string comarca, string localitat, string codipostal)
        {
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;

        }

        public Adreça()
        {

        }

        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        public string Comarca { set; get; }

        public string Localitat { set; get; }

        public string Codipostal { set; get; }

        

       /* public virtual Persona Persona {set; get;}
        public virtual Teatre Teatre { set; get; }*/

        [NotMapped]
        public string AdreçaCompleta => Comarca + " " + Localitat+ " " +Codipostal;



    }
}
