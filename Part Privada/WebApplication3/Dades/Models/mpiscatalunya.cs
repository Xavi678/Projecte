using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dades.Models
{
    public class mpiscatalunya
    {
        [Key]
         public int Codi { set; get; }
        public string Nom { set; get; }
        public int Codicomarca { set; get; }
        public string Nomcomarca { set; get; }
    }
}
