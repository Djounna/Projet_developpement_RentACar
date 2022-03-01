﻿using System;
using System.Collections.Generic;


namespace Models
{
    public partial class Pays
    {
        public Pays()
        {
            Ville = new HashSet<Ville>();
        }
        public int IDPays { get; set; }
      
        public int ReferencePrix { get; set; }
       
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ville> Ville { get; set; }

    }
}
