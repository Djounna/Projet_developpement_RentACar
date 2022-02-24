using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Depot = new HashSet<Depot>();
        }

        public int Idville { get; set; }
        public int Idpays { get; set; }
        public string Nom { get; set; } = null!;

        public virtual Pays IdpaysNavigation { get; set; } = null!;
        public virtual ICollection<Depot> Depot { get; set; }
    }
}
