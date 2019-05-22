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

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, string codipostal)
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
        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, string codipostal, TipusPersona tipus)
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
        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, string codipostal, TipusPersona tipus,string cognoms)
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

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, string codipostal, TipusPersona tipus, int telefon, DateTime dataNaixement) : this(nIF, nom, edat, email, password, comarca, localitat, codipostal, tipus)
        {
            this.telefon = telefon;
            this.dataNaixement = dataNaixement;
        }

        public PersonaVista(string nIF, string nom, int edat, string email, string password, string comarca, string localitat, string codipostal, TipusPersona tipus, int telefon, DateTime dataNaixement, string cognoms) : this(nIF, nom, edat, email, password, comarca, localitat, codipostal, tipus)
        {
            this.telefon = telefon;
            this.dataNaixement = dataNaixement;
            Cognoms = cognoms;
        }
        [Required]
       [RegularExpression("[0-9]{8}[A-Z]{1}",ErrorMessage ="Format invàlid")]   
      
        public string NIF { get; set; }
        [Required]
        public string nom { get; set; }
        [Range(0,120,ErrorMessage ="Número invalid")]
        public int edat { get; set; }
      
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }

        public string Comarca { set; get; }
        [Required]
        public string Localitat { set; get; }

        public string Codipostal { set; get; }
        public TipusPersona tipus { set; get; }
       
        [RegularExpression("[0-9]{9}",ErrorMessage ="El telèfon ha de tenir 9 dígits")]
        public int? telefon { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dataNaixement { set; get; }
        public string Cognoms { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            switch (tipus)
            {
                case TipusPersona.Client:

                    

                    if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(Cognoms) || !telefon.HasValue || !dataNaixement.HasValue || !IsBetween(dataNaixement.Value) )

                    yield return new ValidationResult("Omple tots els camps correctament");
                    break;
            }
        }

        public enum TipusPersona
        {
            Client,Autor, Administrador,Director
        }
        public  bool IsBetween( DateTime input )
        {
            return (input >= new DateTime(1970,12,12) && input <= DateTime.Now);
        }

    }
}