using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    [Table("Compres")]
   public class Compra
    {

     public Compra()
        {

        }

        [ForeignKey("Funcio")]
        public int funcioID { get; set; }
        [ForeignKey("Client")]
        public string clientID { get; set; }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }
        public virtual Funcio Funcio { get; set; }
        public virtual Client Client { get; set; }


    }
}
