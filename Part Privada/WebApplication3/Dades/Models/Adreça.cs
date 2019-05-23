using Dades.Gestor;
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
        public Adreça(string comarca, string localitat, int codipostal)
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

        public int Codipostal { set; get; }

        

       /* public virtual Persona Persona {set; get;}
        public virtual Teatre Teatre { set; get; }*/

        [NotMapped]
        public string AdreçaCompleta => Comarca + " " + Localitat+ " " +Codipostal;


        /// <summary>
        /// Edita els camps d'adreça
        /// </summary>
        /// <param name="municipi">Municipi</param>
        public void editarAdreça( mpiscatalunya municipi, int codipostal)
        {
             //GestorBD bd = new GestorBD();
         
            Comarca = municipi.Nomcomarca;
            Codipostal = codipostal;
            Localitat = municipi.Nom;
        }

        public static implicit operator Adreça(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
