using System;
using System.Collections.Generic;
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

        public string NIF { get; set; }
        public string nom { get; set; }
        public int edat { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public string Comarca { set; get; }

        public string Localitat { set; get; }

        public string Codipostal { set; get; }
        public TipusPersona tipus { set; get; }
        public enum TipusPersona
        {
            Client,Autor, Administrador,Director
        }
    }
}