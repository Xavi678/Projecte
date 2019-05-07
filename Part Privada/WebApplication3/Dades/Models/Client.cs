using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dades.Models
{
    public class Client : Usuari
    {

        public string Cognoms { get; set; }
        public Client()
        {

        }
        


        public Client(Adreça Adreça,string nif, string nom, int edat, string email, string password,int telefon,DateTime dataNaixement,string cognoms) : base(Adreça,nif,nom,edat,email,password,telefon,dataNaixement)
        {
            /* NIF = nif;
             this.nom = nom;
             this.edat = edat;
             this.email = email;
             this.password = password;*/
            Cognoms = cognoms;
        }

    }
}