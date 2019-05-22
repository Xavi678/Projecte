using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [RegularExpression("^[A-Za-z]*$",ErrorMessage ="El nom del teatre només pot contenir lletres")]
        public string Nom { get; set; }
        [Required]
        [MaxLength(2,ErrorMessage ="Número massa gran")]
        public int Files { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage = "Número massa gran")]
        public int Columnes { get; set; }
        public string Comarca { set; get; }
        [Required]
        public string Localitat { set; get; }

        public int Codipostal { set; get; }
    }
}