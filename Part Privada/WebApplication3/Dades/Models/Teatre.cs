using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    [Table("Teatres")]
    public class Teatre
    {
        public Teatre( Adreça Adreça,string nom, int files, int columnes)
        {
            
            this.Adreça = Adreça;
            Nom = nom;
            Files = files;
            Columnes = columnes;
        }

        public Teatre()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Nom { get; set; }
        public int Files { get; set; }
        public int Columnes { get; set; }

        //[Required
        //[Index("AdreçaID", 1, IsUnique = true)]
        [ForeignKey("Adreça")]
        public int AdreçaID { set; get; }

            
        public virtual Adreça Adreça { set; get; }

      




    }
}
