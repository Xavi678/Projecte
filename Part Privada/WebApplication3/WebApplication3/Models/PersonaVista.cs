using Dades.Gestor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PersonaVista : IValidatableObject
    {
        public PersonaVista()
        {

        }
        public PersonaVista(string nIF, string nom, int edat)
        {
            NIF = nIF;
            this.nom = nom;
            this.edat = edat;
        }

        public PersonaVista(string nIF, string nom, int edat, string email, string password)
        {
            NIF = nIF;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;
        }

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, int codipostal)
        {
            NIF = nIF;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;
        }
        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, int codipostal, TipusPersona tipus)
        {
            NIF = nIF;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;
            this.tipus = tipus;
            
        }
        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, int codipostal, TipusPersona tipus,string cognoms)
        {
            NIF = nIF;
            this.nom = nom;
            this.edat = edat;
            this.email = email;
            this.password = password;
            Comarca = comarca;
            Localitat = localitat;
            Codipostal = codipostal;
            this.tipus = tipus;
            this.Cognoms = cognoms;
        }

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, int codipostal, TipusPersona tipus, int telefon, DateTime dataNaixement) : this(nIF, nom, edat, email, password, comarca, localitat, codipostal, tipus)
        {
            this.telefon = telefon;
            this.dataNaixement = dataNaixement;
        }

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, int codipostal, TipusPersona tipus, int telefon, DateTime dataNaixement, string cognoms) : this(nIF, nom, edat, email, password, comarca, localitat, codipostal, tipus)
        {
            this.telefon = telefon;
            this.dataNaixement = dataNaixement;
            Cognoms = cognoms;
        }
        [Required]
       [RegularExpression("^[0-9]{8}[A-Z]{1}$",ErrorMessage ="Format invàlid")]   
      
        public string NIF { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string nom { get; set; }
        [Range(0,120,ErrorMessage ="Número invalid")]
        [Display(Name = "Edat")]
        public int edat { get; set; }
      
        [EmailAddress]
        [Display(Name = "Correu Electrònic")]
        public string email { get; set; }
        [Display(Name = "Contrasenya")]
        public string password { get; set; }

        public string Comarca { set; get; }
        [Required]
        public string Localitat { set; get; }
        [Required]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Ha de tenir 6 dígits")]
        public int Codipostal { set; get; }
        public TipusPersona tipus { set; get; }
        [Display(Name = "Telèfon")]
        [RegularExpression("[0-9]{9}",ErrorMessage ="El telèfon ha de tenir 9 dígits")]
        public int? telefon { set; get; }
        [Display(Name ="Data Naixement")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime? dataNaixement { set; get; }
        public string Cognoms { set; get; }
        /// <summary>
        /// Determina si el objeto especificado es válido.
        /// </summary>
        /// <param name="validationContext">El contexto de validación.</param>
        /// <returns>
        /// Una colección que contiene información de error de validación.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!cpisvalid(Codipostal))
            {
                yield return new ValidationResult("El codi postal no és vàlid");
            }
            switch (tipus)
            {

                

                case TipusPersona.Client:

                    

                    if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(Cognoms) || !telefon.HasValue || !dataNaixement.HasValue || !IsBetween(dataNaixement.Value) )

                    yield return new ValidationResult("Omple tots els camps correctament");
                    break;
                case TipusPersona.Administrador:

                    if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || !telefon.HasValue || !dataNaixement.HasValue || !IsBetween(dataNaixement.Value))

                        yield return new ValidationResult("Omple tots els camps correctament");
                    break;
            }
        }

        public enum TipusPersona
        {
            Client,Autor, Administrador,Director
        }
        /// <summary>
        /// Determina si la data està entre 1970 i la data actual
        /// </summary>
        /// <param name="input">Datetime</param>
        /// <returns>
        ///   <c>true</c> if the specified input is between; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBetween( DateTime input )
        {
            return (input >= new DateTime(1970,12,12) && input <= DateTime.Now);
        }
        /// <summary>
        /// Valida el codipostal
        /// </summary>
        /// <param name="codipostal">Enter</param>
        /// <returns>booleà</returns>
        private bool cpisvalid(int codipostal)
        {
            GestorBD bd = new GestorBD();

            int cp = bd.getCP(Localitat);
            return int.Parse(cp.ToString().Substring(0, 2)).Equals(int.Parse(codipostal.ToString().Substring(0, 2)));
        }

    }
}