using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Pays
    {
        public Pays()
        {
            Ville = new HashSet<Ville>();
        }

        public int Idpays { get; set; }
        public string Nom { get; set; } = null!;
        public int ReferencePrix { get; set; }

        public virtual ICollection<Ville> Ville { get; set; }
    }
}
