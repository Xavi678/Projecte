using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    [Table("Funcions")]
    public class Funcio
    {
        public Funcio(Espectacle Espectacle,Teatre Teatre,DateTime data, TimeSpan horaInici, TimeSpan horaFi)
        {
            this.Teatre = Teatre;
            this.data = data;
            this.horaInici = horaInici;
            this.horaFi = horaFi;
        }

        public Funcio()
        {

        }

        [ForeignKey("Espectacle")]
        public int espectacleID { set; get; }

        [ForeignKey("Teatre")]
        public int teatreID { set; get; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime data { set; get; }
        [DataType(DataType.Time)]
        public TimeSpan horaInici { set; get; }
        [DataType(DataType.Time)]
        public TimeSpan horaFi { set; get; }
        public virtual Espectacle Espectacle {set; get;}
        public virtual Teatre Teatre { set; get; }

    }
}
