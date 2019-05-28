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
    public class Funcio : IValidatableObject
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
        [Required]
        [Display(Name ="Espectacle")]
        public int espectacleID { set; get; }

        [ForeignKey("Teatre")]
        [Required]
        [Display(Name = "Teatre")]
        public int teatreID { set; get; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime data { set; get; }
        [DataType(DataType.Time)]
        [Required]
        [Display(Name = "Hora Inici")]
        public TimeSpan horaInici { set; get; }
        [DataType(DataType.Time)]
        [Required]
        [Display(Name = "Hora Fi")]
        public TimeSpan horaFi { set; get; }
        [Display(Name = "Espectacle")]
        public virtual Espectacle Espectacle {set; get;}
        [Display(Name = "Teatre")]
        public virtual Teatre Teatre { set; get; }

        /// <summary>
        /// Determina si el objeto especificado es válido.
        /// </summary>
        /// <param name="validationContext">El contexto de validación.</param>
        /// <returns>
        /// Una colección que contiene información de error de validación.
        /// </returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            //checkHores(horaInici,horaFi) ??  yield return new ValidationResult("l'hora d'inici ha de ser més petita que la de fi");
            if (checkHores(this.horaInici, this.horaFi))
            {
                yield return new ValidationResult("l'hora d'inici ha de ser més petita que la de fi");
            }
        }

        /// <summary>
        /// Comprova si la data d'inici és més petita que la final
        /// </summary>
        /// <param name="horaInici">The hora inici.</param>
        /// <param name="horaFi">The hora fi.</param>
        /// <returns>Booleà</returns>
        private bool checkHores(TimeSpan horaInici, TimeSpan horaFi)
        {
            //throw new NotImplementedException();

            return horaInici.CompareTo(horaFi) >= 0 ? true : false;

            
        }
    }
}
