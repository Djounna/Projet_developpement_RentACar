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
<<<<<<< HEAD

        public int Idpays { get; set; }
=======
        public int IDPays { get; set; }
      
        public int ReferencePrix { get; set; }
       
>>>>>>> Antoine
        public string Nom { get; set; } = null!;
        public int ReferencePrix { get; set; }

        public virtual ICollection<Ville> Ville { get; set; }

    }
}
