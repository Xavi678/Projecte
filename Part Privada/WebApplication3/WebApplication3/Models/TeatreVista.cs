using Dades.Gestor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class TeatreVista : IValidatableObject
    {
        public TeatreVista()
        {

        }
        public TeatreVista(string nom, int files, int columnes, string comarca, string localitat, int codipostal)
        {
            Nom = nom;
            Files = files;
            Columnes = columnes;
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;
        }

        public TeatreVista(int ID,string nom, int files, int columnes, string comarca, string localitat, int codipostal)
        {
            this.ID = ID;
            Nom = nom;
            Files = files;
            Columnes = columnes;
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;
        }
        public int ID { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage ="El nom del teatre només pot contenir lletres")]
        public string Nom { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{2}$", ErrorMessage = "Només pot tenir 2 xifres")]
        [Range(1,9,ErrorMessage = "Només pot tenir 2 xifres")]
        public int Files { get; set; }
        [Required]
        //[RegularExpression("^[0-9]{2}$", ErrorMessage = "Només pot tenir 2 xifres")]
        [Range(1, 9, ErrorMessage = "Només pot tenir 2 xifres")]
        public int Columnes { get; set; }
        public string Comarca { set; get; }
        [Required]
        public string Localitat { set; get; }
      [RegularExpression("^[0-9]{6}$",ErrorMessage ="Ha de tenir 6 dígits")]
        [Required]
        public int Codipostal { set; get; }
        /// <summary>
        /// Determina si el objeto especificado es válido.
        /// </summary>
        /// <param name="validationContext">El contexto de validación.</param>
        /// <returns>
        /// Una colección que contiene información de error de validación.
        /// </returns>
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (!cpisvalid(Codipostal))
            {
               yield return new ValidationResult("El codi postal no és vàlid");
            }
        }

        /// <summary>
        /// Valida el codipostal
        /// </summary>
        /// <param name="codipostal">Enter</param>
        /// <returns>booleà</returns>
        private bool cpisvalid(int codipostal)
        {
            GestorBD bd = new GestorBD();

          int cp=  bd.getCP(Localitat);
         return   int.Parse(cp.ToString().Substring(0, 2)).Equals(int.Parse(codipostal.ToString().Substring(0, 2)));
        }
    }
}