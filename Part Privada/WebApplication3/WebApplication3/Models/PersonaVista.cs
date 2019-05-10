using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class PersonaVista
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

        public string NIF { get; set; }
        public string nom { get; set; }
        public int edat { get; set; }
      
        public string email { get; set; }
        public string password { get; set; }

        public string Comarca { set; get; }

        public string Localitat { set; get; }

        public string Codipostal { set; get; }
        public TipusPersona tipus { set; get; }
        public int telefon { set; get; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dataNaixement { set; get; }
        public string Cognoms { set; get; }
        public enum TipusPersona
        {
            Client,Autor, Administrador,Director
        }

        
    }
}