using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Pays
    {
        public Pays()
        {
            Prix = new HashSet<Prix>();
            Ville = new HashSet<Ville>();
        }

        public int Idpays { get; set; }
        public string Nom { get; set; } = null!;

        public virtual ICollection<Prix> Prix { get; set; }
        public virtual ICollection<Ville> Ville { get; set; }
    }
}
