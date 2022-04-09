using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Depots = new HashSet<Depot>();
        }

        public int Idville { get; set; }
        public int Idpays { get; set; }
        public string Nom { get; set; } = null!;

        public virtual Pays IdpaysNavigation { get; set; } = null!;
        public virtual ICollection<Depot> Depots { get; set; }
    }
}
