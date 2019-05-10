using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class TeatreVista
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
        public string Nom { get; set; }
        public int Files { get; set; }
        public int Columnes { get; set; }
        public string Comarca { set; get; }

        public string Localitat { set; get; }

        public int Codipostal { set; get; }
    }
}