using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Notoriete
    {
        public Notoriete()
        {
            Voitures = new HashSet<Voiture>();
        }

        public int Idnotoriete { get; set; }
        public string? Libelle { get; set; }
        public decimal? CoefficientMultiplicateur { get; set; }
        public bool? Inactif { get; set; }

        public virtual ICollection<Voiture> Voitures { get; set; }
    }
}
