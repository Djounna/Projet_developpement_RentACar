using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{ 
    public partial class Pays
    {
        public Pays()
        {
            Ville = new HashSet<Ville>();
        }

        public int Idpays { get; set; }
        public int ReferencePrix { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ville> Ville { get; set; }
    }
}
