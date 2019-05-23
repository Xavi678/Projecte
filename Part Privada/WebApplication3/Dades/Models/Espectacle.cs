using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
   public class Espectacle
    {
        public Espectacle()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EspectacleID { get; set; }
        [Required]
        public string titol { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string sinopsi { get; set; }
        [DataType(DataType.Time)]
        [Required]
        public TimeSpan durada { get; set; }
        [Required]
        public string cartell { get; set; }
       [ForeignKey("Director")]
       [Display(Name ="Director")]
        public string nifDirector { get; set; }

        [ForeignKey("Autor")]
        [Display(Name = "Autor")]
        public string nifAutor { get; set; }
        
        public virtual Director Director { get; set; }
       
        public virtual Autor Autor { get; set; }

        public Espectacle(string titol, string sinopsi, TimeSpan durada, string cartell, Director director, Autor autor)
        {
            this.titol = titol;
            this.sinopsi = sinopsi;
            this.durada = durada;
            this.cartell = cartell;
            Director = director;
            Autor = autor;

        }

        
    }
}
