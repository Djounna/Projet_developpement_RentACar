using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Notoriete
    {
        public Notoriete()
        {
            Voiture = new HashSet<Voiture>();
        }

        public int Idnotoriete { get; set; }
        public string? Libelle { get; set; }
        public decimal? CoefficientMultiplicateur { get; set; }
        public bool? Inactif { get; set; }

        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
