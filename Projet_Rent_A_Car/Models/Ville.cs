using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public partial class Ville
    {
        public Ville()
        {
            Depot = new HashSet<Depot>();
        }

        public int Idville { get; set; }
        public int IDPays { get; set; }
        public string Nom { get; set; } = null!;

        public virtual Pays IdpaysNavigation { get; set; } = null!;
        public virtual ICollection<Depot> Depot { get; set; }


    }
}
